using Microsoft.OpenApi.Models;
using Tockify.Application.Command.ToDo;
using Tockify.Application.Mappings;
using Tockify.Application.Services.Interfaces.ClientUser;
using Tockify.Application.Services.Interfaces.ToDo;
using Tockify.Application.Services.UseCases.ClientUser;
using Tockify.Application.Services.UseCases.Implementations;
using Tockify.Application.Services.UseCases.ToDo;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Context;
using Tockify.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- Persistência / MongoContext e Repositórios ---
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<IClientUserRepository, ClientUserRepository>();
builder.Services.AddScoped<IToDoListRepository, ToDoRepository>();

// --- AutoMapper ---
builder.Services.AddAutoMapper(typeof(ClientUserProfile));
builder.Services.AddAutoMapper(typeof(ToDoProfile));

// --- Casos de Uso ClientUser ---
builder.Services.AddScoped<ICreateClientUserCase, CreateClientUserUseCase>();
builder.Services.AddScoped<IGetAllClientUsersCase, GetAllClientUsersCase>();
builder.Services.AddScoped<IGetClientUserByIdCase, GetClientUserByIdCase>();
builder.Services.AddScoped<IUpdateClientUseCase, UpdateClientUserUseCase>();
builder.Services.AddScoped<IDeleteClientUserCase, DeleteClientUserUseCase>();

// --- Casos de Uso ToDo ---
builder.Services.AddScoped<ICreateToDoCase, CreateToDoCase>();
builder.Services.AddScoped<IGetUserToDosCase, GetUserToDosCase>();
builder.Services.AddScoped<IUpdateToDoCase, UpdateToDoCase>();
builder.Services.AddScoped<IDeleteToDoCase, DeleteToDoCase>();

// ---  TaskItem ---


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
