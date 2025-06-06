using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Tockify.Domain.Repository;
using Tockify.Domain.Repository.Interface;
using Tockify.Infrastructure.Data;

namespace Tockify.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            /// Aplicando inje��o de depend�ncia para o reposit�rio

            /// Configurando o DbContext para usar SQL Server -- MongoDB eu vejo depois
            builder.Services.AddDbContext<TockifyDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TockifyDBConnection"))); // o nome da conex�o deve ser definido no appsettings.json


            // Resgistrando o reposit�rio de usu�rios do cliente
            builder.Services.AddScoped<IClientUserRepository, ClientRepository>();
            // Conforme for terminando o os reposit�rios, v� registrando aqui


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
