using Day3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Day3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("Company");
            builder.Services.AddDbContext<ComContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services
                   .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;

        options.User.RequireUniqueEmail = true;
    })
                   .AddEntityFrameworkStores<ComContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "DefS";
                options.DefaultChallengeScheme = "DefS";
            })
            .AddJwtBearer("DefS", options =>
            {
                var secretKeyString = builder.Configuration.GetValue<string>("SecretKey");
                var secretyKeyInBytes = Encoding.ASCII.GetBytes(secretKeyString ?? string.Empty);
                var secretKey = new SymmetricSecurityKey(secretyKeyInBytes);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = secretKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminsOnly", policy => policy
                    .RequireClaim(ClaimTypes.Role, "admin")
                    .RequireClaim(ClaimTypes.NameIdentifier));

                options.AddPolicy("UsersOnly", policy => policy
                    .RequireClaim(ClaimTypes.Role, "admin", "user")
                    .RequireClaim(ClaimTypes.NameIdentifier));
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}