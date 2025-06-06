using AutoMapper;
using Microsoft.OpenApi.Models;
using Tockify.Application.Mappings;
using Tockify.Application.Services.UseCases.ClientUser;
using Tockify.Application.Services.UseCases.Implementations;
using Tockify.Application.Services.UseCases.Interfaces;
using Tockify.Application.Services.UseCases.TaskItem;
using Tockify.Application.Services.UseCases.ToDo;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Data;
using Tockify.Infrastructure.Repositories;


namespace Tockify.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            /// Injetar MongoContext e Repositórios
            builder.Services.AddSingleton<MongoContext>();
            // Resgistrando o repositório de usuários do cliente
            builder.Services.AddScoped<IClientUserRepository, ClientRepository>();
            builder.Services.AddScoped<IClientUserRepository, ClientRepository>();
            builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();
            builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();

            // Registrar AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Registrar Use Cases
            builder.Services.AddScoped<ICreateClientUserUseCase, CreateClientUser>();
            builder.Services.AddScoped<IGetAllClientUsersUseCase, GetAllClientUsers>();
            builder.Services.AddScoped<ICreateToDotUseCase, CreateToDoList>();
            builder.Services.AddScoped<IGetToDoByUserUseCase, GetToDoListsByUserUseCase>();
            builder.Services.AddScoped<ICreateTaskItemUseCase, CreateTaskItemUseCase>();
            builder.Services.AddScoped<IGetTaskItemsByTaskListUseCase, GetTaskItemsByTaskListUseCase>();

            // Controllers e Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tockify API",
                    Version = "v1",
                    Description = "API para administrar suas tarefas."
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tockify API V1");
            });
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            // autenticação
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
