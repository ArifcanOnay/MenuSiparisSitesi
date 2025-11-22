
using MenuSiparis.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace MenuSiparis.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddAuthorization();

            // Swagger/OpenAPI yapılandırması
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<MenüSiparisDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(MenüSiparisDbContext)).GetName().Name);
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Menu Siparis API V1");
                    c.RoutePrefix = string.Empty; // Swagger'ı root URL'de aç
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();
        }
    }
}
