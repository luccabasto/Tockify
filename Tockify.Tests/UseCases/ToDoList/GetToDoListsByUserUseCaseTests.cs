using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // AutoMapperFixture.MapperInstance deve expor um IMapper configurado com MappingProfile
            _mapper = AutoMapperFixture.MapperInstance;
        }

        [Fact(DisplayName = "GetTaskListsByUserUseCase deve retornar lista mapeada corretamente")]
        public async Task ExecuteAsyncWhenCalledReturnsTaskListDtoList()
        {
            // Arrange

            // 1) Cria um Guid de usuário fictício
            var userIdGuid = Guid.NewGuid();

            // 2) Cria lista fake de TaskListModel
            var fakeEntities = new List<CardModel>
            {
                // Construtor de TaskListModel: (string userId, string name, string description, DateTime dueDate)
                new CardModel(
                    id: Guid.NewGuid().ToString(),
                    userId: userIdGuid.ToString(), // Corrigido: userIdGuid() -> userIdGuid.ToString()
                    name: "Lista 1",
                    dueDate: DateTime.UtcNow.AddDays(1)
                ),
                new CardModel(
                    id: Guid.NewGuid().ToString(),
                    userId: userIdGuid.ToString(),
                    name: "Lista 2",
                    dueDate: DateTime.UtcNow.AddDays(2)
                )
            };

            // 3) Configura o Mock<IToDoListRepository> (ou ITaskListRepository)
            var mockRepo = new Mock<IToDoListRepository>();
            mockRepo
                .Setup(r => r.GetTasksByUserIdAsync(userIdGuid))
                .ReturnsAsync(fakeEntities);

            // 4) Instancia o Use Case, injetando o mock e o mapper
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

            // Act / Assert: se Guid.Empty for passado, esperamos ArgumentException
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(Guid.Empty));

            // Verifica que o repositório não foi sequer invocado
            mockRepo.Verify(r => r.GetTasksByUserIdAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
