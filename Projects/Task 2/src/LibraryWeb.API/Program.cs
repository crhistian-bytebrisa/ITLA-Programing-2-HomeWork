using LibraryWeb.API.Middleware;
using LibraryWeb.Application.Interfaces;
using LibraryWeb.Application.MappingConfig;
using LibraryWeb.Application.Services;
using LibraryWeb.Domain.Interfaces.Repositories;
using LibraryWeb.Infraestructure.Data.LibraryContext;
using LibraryWeb.Infraestructure.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace LibraryWeb.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMapster();
            MappingConfig.RegisterMappings();
            builder.Services.AddSingleton<TypeAdapterConfig>(TypeAdapterConfig.GlobalSettings);


            // Add services to the container.
            builder.Services.AddScoped<ILanguageService, LanguageService>();
            builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
            builder.Services.AddScoped<IGenreService, GenreService>();
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepostory>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            builder.Services.AddDbContext<DataContext>(
                opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddIdentityCore<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<IdentityUser>>();

            builder.Services.AddScoped<SignInManager<IdentityUser>>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llave"]!)),
                    ClockSkew = TimeSpan.Zero
                };

            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
