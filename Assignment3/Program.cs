
using Assignment3.Data;
using Assignment3.Models;
using Assignment3.Repo.IMP;
using Assignment3.Repo.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Assignment3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBookCustomRepository, BookCustomRepository>();
            builder.Services.AddScoped<IBorrowCustomRepo, BorrowCustomRepo>();
            builder.Services.AddScoped<IMemberCustomRepo, MemberCustomRepo>();
            builder.Services.AddScoped<IGenericRepo<Book>, GenericRepo<Book>>();
            builder.Services.AddScoped<IGenericRepo<Member>, GenericRepo<Member>>();
            builder.Services.AddScoped<IGenericRepo<Category>, GenericRepo<Category>>();
            builder.Services.AddScoped<IGenericRepo<Borrowing>, GenericRepo<Borrowing>>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            var key = Encoding.ASCII.GetBytes(
                builder.Configuration["Jwt:Key"]!
            );

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters =new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =new SymmetricSecurityKey(key),

                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer =builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                    };
            });

            builder.Services.AddAuthorization();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
