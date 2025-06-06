using Microsoft.OpenApi.Models;
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

            /// Aplicando inje��o de depend�ncia para o reposit�rio
            /// 
            // Registra o MongoContext 
            builder.Services.AddSingleton<MongoContext>();

            // Resgistrando o reposit�rio de usu�rios do cliente
            builder.Services.AddScoped<IClientUserRepository, ClientRepository>();
            

            // Registrando os servi�os
            

            // Controllers e Swagger
            builder.Services.AddControllers();
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

            // autentica��o
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
