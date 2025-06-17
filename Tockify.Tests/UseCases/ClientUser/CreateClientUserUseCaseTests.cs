using AutoMapper;
using FluentAssertions;
using Moq;
using Tockify.Application.DTOs;
using Tockify.Application.Services.UseCases.ClientUser;
using Tockify.Domain.Enums;
using Tockify.Domain.Models;
using Tockify.Domain.Repository.Interface;
using Tockify.Tests.Helpers;
using Xunit;

namespace Tockify.Tests.UseCases.ClientUser
{
    public class CreateClientUserUseCaseTests
    {
        private readonly IMapper _mapper;

        public CreateClientUserUseCaseTests()
        {
            _mapper = AutoMapperFixture.MapperInstance;
        }

        [Fact(DisplayName = "Quando dados válidos, CreateClientUserUseCase deve retornar DTO com valores corretos")]
        public async Task ExecuteAsync_ValidCommand_ReturnsClientUserDto()
        {
            // Arrange
            var mockRepo = new Mock<IClientUserRepository>();
            // Simula que não existe usuário com esse email
            mockRepo.Setup(r => r.ClientUserExistsAsync(It.IsAny<string>()))
                    .ReturnsAsync(false);

            // Ao cadastrar, devolve exatamente a entidade que foi passada
            mockRepo.Setup(r => r.RegisterUserAsync(It.IsAny<ClientUserModel>(), It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync((ClientUserModel u, string e, string p) =>
                    {
                        // Imita “persistência” atribuindo Id e CreatedAt
                        u.Id = new Random().Next(1, 1000);
                        u.Profile = UserProfile.Client;
                        u.IsActive = true;
                        u.CreatedAt = DateTime.UtcNow;
                        return u;
                    });

            var useCase = new CreateClientUser(mockRepo.Object, _mapper);

            var command = new CreateClientUserCommand
            {
                Name = "Teste Usuário",
                Email = "teste@domain.com",
                Password = "senha123",
                Gender = "Male"
            };

            // Act
            var result = await useCase.ExecuteAsync(command);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().NotBeEmpty();
            result.Name.Should().Be("Teste Usuário");
            result.Email.Should().Be("teste@domain.com");
            result.Profile.Should().Be(UserProfile.Client);
            result.IsActive.Should().BeTrue();
            result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

            mockRepo.Verify(r => r.ClientUserExistsAsync("teste@domain.com"), Times.Once);
            mockRepo.Verify(r => r.RegisterUserAsync(It.IsAny<ClientUserModel>(), "teste@domain.com", "senha123"), Times.Once);
        }

        [Fact(DisplayName = "Quando email já existe, CreateClientUserUseCase deve lançar InvalidOperationException")]
        public async Task ExecuteAsync_EmailAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var mockRepo = new Mock<IClientUserRepository>();
            // Simula que já existe usuário com esse email
            mockRepo.Setup(r => r.ClientUserExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(true);

            var useCase = new CreateClientUser(mockRepo.Object, _mapper);

            var command = new CreateClientUserCommand
            {
                Name = "Teste Usuário",
                Email = "exists@domain.com",
                Password = "senha123",
                Gender = "Male"
            };

            // Act / Assert
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => useCase.ExecuteAsync(command));

            mockRepo.Verify(r => r.ClientUserExistsAsync("exists@domain.com"), Times.Once);
            mockRepo.Verify(r => r.RegisterUserAsync(It.IsAny<ClientUserModel>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Theory(DisplayName = "Quando Name, Email ou Password inválidos, CreateClientUserUseCase deve lançar ArgumentException")]
        [InlineData("", "a@b.com", "senha123")]       // name vazio
        [InlineData("Nome", "", "senha123")]          // email vazio
        [InlineData("Nome", "a@b.com", "")]           // senha vazia
        public async Task ExecuteAsync_InvalidFields_ThrowsArgumentException(string name, string email, string password)
        {
            // Arrange
            var mockRepo = new Mock<IClientUserRepository>();
            var useCase = new CreateClientUser(mockRepo.Object, _mapper);

            var command = new CreateClientUserCommand
            {
                Name = name,
                Email = email,
                Password = password,
                Gender = "Male"
            };

            // Act / Assert
            await Assert.ThrowsAsync<ArgumentException>(
                () => useCase.ExecuteAsync(command));

            mockRepo.Verify(r => r.ClientUserExistsAsync(It.IsAny<string>()), Times.Never);
            mockRepo.Verify(r => r.RegisterUserAsync(It.IsAny<ClientUserModel>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
