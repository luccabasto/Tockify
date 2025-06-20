using AutoMapper;
using Microsoft.OpenApi.Models;
using Tockify.Application.Mappings;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Application.Services.Interfaces.TaskItem;
using Tockify.Application.Services.Interfaces.ToDo;
using Tockify.Application.Services.UseCases.ClientUser;
using Tockify.Application.Services.UseCases.Implementations;
using Tockify.Application.Services.UseCases.TaskItem;
/*using Tockify.Application.Services.UseCases.ToDo;*/
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Context;
using Tockify.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- Persistência / MongoContext e Repositórios ---
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<IClientUserRepository, ClientUserRepository>();
/*builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();*/
/*builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();*/

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(ClientUserProfile));

// --- Casos de Uso ClientUser ---
builder.Services.AddScoped<ICreateClientUserCase, CreateClientUserUseCase>();
builder.Services.AddScoped<IGetAllClientUsersCase, GetAllClientUsersCase>();
builder.Services.AddScoped<IGetClientUserByIdCase, GetClientUserByIdCase>();
builder.Services.AddScoped<IUpdateClientUseCase, UpdateClientUserUseCase>();
builder.Services.AddScoped<IDeleteClientUserCase, DeleteClientUserUseCase>();

// --- Casos de Uso ToDo e TaskItem (separados) ---
/*builder.Services.AddScoped<ICreateToDoListUseCase, CreateToDoListUseCase>();*/
builder.Services.AddScoped<IGetToDoByUserUseCase, GetToDoListsByUserUseCase>();
builder.Services.AddScoped<ICreateTaskItemUseCase, CreateTaskItemUseCase>();
builder.Services.AddScoped<IGetTaskItemsByTaskListUseCase, GetTaskItemsByTaskListUseCase>();

// --- MVC / Swagger ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tockify API V1"));

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
