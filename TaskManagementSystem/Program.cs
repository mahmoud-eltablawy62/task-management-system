using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Talabat.Repository.Data;
using TaskManagementSystem.api.Extensions;
using TaskManagementSystem.api.MiddleWares;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.Core;
using TaskManagementSystem.Core.Entities.Identity;
using TaskManagementSystem.Core.Services.Contract;
using TaskManagementSystem.Repository;
using TaskManagementSystem.Repository.Data;
using TaskManagementSystem.Repository.Data.Config;
using TaskManagementSystem.Repository.Identity;
using TaskManagementSystem.Repository.Repo.Contract;
using TaskManagementSystem.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(typeof(IGenaricRepo<>), typeof(GenaricRepo<>));
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(ITaskService), typeof(TaskServices));


            builder.Services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });

            builder.Services.AddDbContext<TasksContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddDbContext<UserDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Connection_Identity_User"));
            });

            // builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
          

            builder.Services.AddIdentityService(builder.Configuration);

            var app = builder.Build();
            
            app.UseMiddleware<ExceptionMiddleWares>();

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<TasksContext>();

            var _IdentityContext = services.GetRequiredService<UserDbContext>();


            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                
                await _IdentityContext.Database.MigrateAsync();
                var _user_manager = services.GetRequiredService<UserManager<User>>();
                await UserContextSeed.UserSeedAsync(_user_manager); ;

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration ");
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();
            app.MapControllers();

           

            app.Run();
        }
    }
}




