using AutoMapper;
using FluentAssertions;
using Moq;
using Tockify.Application.Services.UseCases.Implementations;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Tests.Helpers;
using Xunit;

namespace Tockify.Tests.UseCases.TaskList
{
    public class GetTaskListsByUserUseCaseTests
    {
        private readonly IMapper _mapper;

        public GetTaskListsByUserUseCaseTests()
        {
            _mapper = AutoMapperFixture.MapperInstance;
        }

        [Fact(DisplayName = "GetTaskListsByUserUseCase deve retornar lista mapeada corretamente")]
        public async Task ExecuteAsyncWhenCalledReturnsTaskListDtoList(List<CardModel> fakeEntities)
        {
            // Arrange
            var mockRepo = new Mock<IToDoListRepository>();

            var fakeEntities = new List<CardModel>
            {
                new CardModel {
                    Id          = Guid.NewGuid().ToString(),
                    UserId      = new ClientUserModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "User 1",
                        Email = "user1@example.com",
                        Password = "password1",
                        IsActive = true
                    },
                    Name        = "Lista 1",
                    Description = "Descrição 1",
                    CreatedAt   = DateTime.UtcNow.AddDays(-1),
                    DueDate     = DateTime.UtcNow.AddDays(1)
                },
                new CardModel
                {
                    Id          = Guid.NewGuid().ToString(),
                    UserId      = new ClientUserModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "User 2",
                        Email = "user2@example.com",
                        Password = "password2",
                        IsActive = true
                    },
                    Name        = "Lista 2",
                    Description = "Descrição 2",
                    CreatedAt   = DateTime.UtcNow.AddDays(-2),
                    DueDate     = DateTime.UtcNow.AddDays(2)
                }
            };

            var userIdGuid = fakeEntities[0].UserId.Id;

            // Simula que o repositório vai retornar a lista acima
            mockRepo.Setup(r => r.GetTasksByUserIdAsync(userIdGuid))
                    .ReturnsAsync(fakeEntities);

            var useCase = new GetToDoListsByUserUseCase(mockRepo.Object, _mapper);

            // Act
            var result = await useCase.ExecuteAsync(userIdGuid);

            // Assert
            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Lista 1");
            result.Last().Name.Should().Be("Lista 2");

            mockRepo.Verify(r => r.GetTasksByUserIdAsync(userIdGuid), Times.Once);
        }

        [Fact(DisplayName = "Quando userId inválido, GetTaskListsByUserUseCase lança ArgumentException")]
        public async Task ExecuteAsync_InvalidUserId_ThrowsArgumentException()
        {
            // Arrange
            var mockRepo = new Mock<IToDoListRepository>();
            var useCase = new GetToDoListsByUserUseCase(mockRepo.Object, _mapper);

            // Act / Assert
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(Guid.Empty));
            mockRepo.Verify(r => r.GetTasksByUserIdAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
