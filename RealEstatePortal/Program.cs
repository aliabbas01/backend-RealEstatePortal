using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RealEstatePortal.Data;
using RealEstatePortal.Interfaces;
using RealEstatePortal.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Your React app's URL
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// Add services to the container
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IMapperService, MapperService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();
 
var tokenKey = builder.Configuration.GetSection("AppSettings:Token").Value;
if (string.IsNullOrEmpty(tokenKey))
{
    throw new ApplicationException("JWT secret key is not configured");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    // Apply pending migrations automatically
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataContext>();

            // Check if database exists and apply migrations
            if (context.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Applying pending migrations...");
                context.Database.Migrate();
                Console.WriteLine("Migrations applied successfully!");
            }
            else
            {
                Console.WriteLine("Database is up to date - no pending migrations.");
            }

            // Optional: Seed initial data
            // await SeedData.Initialize(services);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or seeding the database.");
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowReactFrontend");
app.Run();