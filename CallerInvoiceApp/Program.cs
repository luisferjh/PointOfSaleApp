using Application.Interfaces;
using Application.Services;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Implementations;
using Infrastructure.Interfaces;
using Mapster;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CallerInvoiceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddMapster();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUnitOfWorkRepositories, UnitOfWorkRepositories>();
            builder.Services.AddScoped<IUnitOfWorkAdapter, UnitOfWorkAdapter>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();      
            builder.Services.AddScoped<IDbConnection>(provider => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDbTransaction>(s =>
            {
                SqlConnection conn = (SqlConnection)s.GetService<IDbConnection>();
                conn.Open();
                return conn.BeginTransaction();
            });
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IRateService, RateService>();
            builder.Services.AddTransient<IInvoiceService, InvoiceService>();
            builder.Services.AddTransient<IProductCategoryCollection, ProductCategoryCollection>();
            builder.Services.AddTransient<IUserCollection, UserCollection>();
            builder.Services.AddTransient<IProductCollection, ProductCollection>();
            builder.Services.AddTransient<IInvoiceCollection, InvoiceCollection>();
            builder.Services.AddTransient<IRateCollection, RateCollection>();                            
            builder.Services.RegisterMapsterConfiguration();    

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            var summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.MapGet("/weatherforecast", (HttpContext httpContext) =>
            {
                var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    {
                        Date = DateTime.Now.AddDays(index),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = summaries[Random.Shared.Next(summaries.Length)]
                    })
                    .ToArray();
                return forecast;
            })
            .WithName("GetWeatherForecast");

           

            app.Run();
        }
    }
}