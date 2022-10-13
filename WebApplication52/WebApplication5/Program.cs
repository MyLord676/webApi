using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication5.Data;
using WebApplication5.Domain.Entities;
using WebApplication5.Service;
using WebApplication5.Domain.Entities;
using WebApplication5.Service;

namespace WebApplication5
{ 
    public class Programm
    {
        private static void Main(string[] args)
        { 
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.Bind("Project", new DbConfig());

            builder.Services.AddControllers();

            // Add services to the container.
            //builder.Services.AddTransient<IUsers, MockUsers>();
            //builder.Services.AddTransient<IPost, MockPosts>();
            //builder.Services.AddTransient<IOffice, MockOffices>();
            //builder.Services.AddTransient<IPublication, MockPublications>();

            //Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DataBaseContext>();

            var app = builder.Build();
            
            // Swagger
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
