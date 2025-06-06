using AutoMapper;
using FluentAssertions;
using Moq;
using Tockify.Application.DTOs;
using Tockify.Application.Services.UseCases.ToDo;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Tests.Helpers;
using Xunit;

namespace Tockify.Tests.UseCases.TaskList
{
    public class CreateToDoListUseCaseTests
    {
        private readonly IMapper _mapper;
        private object entity;

        public CreateToDoListUseCaseTests()
        {
            _mapper = AutoMapperFixture.MapperInstance;
        }

        [Fact(DisplayName = "Quando dados válidos, CreateTaskListUseCase retorna TaskListDto com valores corretos")]
        public async Task ExecuteAsync_ValidCommand_ReturnsTaskListDto()
        {
            // Arrange
            var mockRepo = new Mock<IToDoListRepository>();

            // Simula que AddTaskAsync devolve exatamente a entidade que recebe (com CreatedAt etc.)
            mockRepo.Setup(r => r.AddTaskAsync(It.IsAny<CardModel>()))
                    .ReturnsAsync((CardModel t) =>
                    {
                        t.Id = Guid.NewGuid().ToString();
                        t.CreatedAt = DateTime.UtcNow;
                        return t;
                    });

            var dto = _mapper.Map<ToDoDto>(entity);
            var useCase = new CreateToDoList(mockRepo.Object, _mapper);

            var command = new CreateToDoCommand
            {
                UserId = Guid.NewGuid(),
                Name = "Minha Lista",
                Description = "Descrição de teste",
                DueDate = DateTime.UtcNow.AddDays(3)
            };

            // Act
            var result = await useCase.ExecuteAsync(command);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.UserId.Should().Be(command.UserId);
            result.Name.Should().Be("Minha Lista");
            result.Description.Should().Be("Descrição de teste");
            result.DueDate.Should().BeCloseTo(command.DueDate, TimeSpan.FromSeconds(1));
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

            mockRepo.Verify(r => r.AddTaskAsync(It.IsAny<CardModel>()), Times.Once);
        }

        [Fact(DisplayName = "Quando UserId ou Name inválidos, CreateTaskListUseCase lança ArgumentException")]
        public async Task ExecuteAsync_InvalidFields_ThrowsArgumentException()
        {
            // Arrange
            var mockRepo = new Mock<IToDoListRepository>();
            var useCase = new CreateToDoList(mockRepo.Object, _mapper);

            // Cenário 1: userId vazio
            var cmd1 = new CreateToDoCommand
            {
                UserId = Guid.Empty,
                Name = "Teste",
                Description = "Desc",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            // Cenário 2: nome vazio
            var cmd2 = new CreateToDoCommand
            {
                UserId = Guid.NewGuid(),
                Name = "",
                Description = "Desc",
                DueDate = DateTime.UtcNow.AddDays(1)
            };

            // Act / Assert
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(cmd1));
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(cmd2));

            mockRepo.Verify(r => r.AddTaskAsync(It.IsAny<CardModel>()), Times.Never);
        }
    }
}
