using AutoMapper;
using FluentAssertions;
using Moq;
using Tockify.Application.Services.UseCases.ClientUser;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Tests.Helpers;
using Xunit;

namespace Tockify.Tests.UseCases.ClientUser
{
    public class GetAllClientUsersUseCaseTests
    {
        private readonly IMapper _mapper;

        public GetAllClientUsersUseCaseTests()
        {
            _mapper = AutoMapperFixture.MapperInstance;
        }

        [Fact(DisplayName = "GetAllClientUsersUseCase deve retornar lista mapeada corretamente")]
        public async Task ExecuteAsync_WhenCalled_ReturnsDtoList()
        {
            // Arrange
            var mockRepo = new Mock<IClientUserRepository>();
            var fakeEntities = new List<ClientUserModel>
            {
                new ClientUserModel
                {
                    Id        = System.Guid.NewGuid(),
                    Name      = "Usuário A",
                    Email     = "a@teste.com",
                    Password  = "senhaA",
                    Profile   = UserProfile.Client,
                    IsActive  = true,
                    CreatedAt = System.DateTime.UtcNow
                },
                new ClientUserModel
                {
                    Id        = System.Guid.NewGuid(),
                    Name      = "Usuário B",
                    Email     = "b@teste.com",
                    Password  = "senhaB",
                    Profile   = UserProfile.Client,
                    IsActive  = false,
                    CreatedAt = System.DateTime.UtcNow.AddDays(-1)
                }
            };

            mockRepo.Setup(r => r.GetAllClientUsers(UserProfile.Client))
            .ReturnsAsync(fakeEntities);

            var useCase = new GetAllClientUsers(mockRepo.Object, _mapper);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Usuário A");
            result.Last().Name.Should().Be("Usuário B");

            mockRepo.Verify(r => r.GetAllClientUsers(UserProfile.Client), Times.Once);
        }
    }
}
