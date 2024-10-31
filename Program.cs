using LMSOnline.Models;
using LMSOnline.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

namespace LMSOnline
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create the builder for the application.
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bear Authorization: `Bearer  Genreated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id=JwtBearerDefaults.AuthenticationScheme
                            }
                        },new String[]{}
                    }
                });
            });

            // Get connection string from configuration.
            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnect"];
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionString));

            // Register services
            builder.Services.AddScoped<AccountService, AccountServiceImpl>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Configure authorization policies
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "User"));
                });
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"));
                });
                options.AddPolicy("LeaderShip", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "LeaderShip"));
                });
                options.AddPolicy("AdminOrSuperAdmin", policy =>
                {
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                            (c.Value == "Admin" || c.Value == "SuperAdmin")));
                });
            });

            // Build the application
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
