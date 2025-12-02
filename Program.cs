using Microsoft.EntityFrameworkCore;
using ScamWarning.Data;
using ScamWarning.Interfaces;
using ScamWarning.Repositories;
using ScamWarning.Services;
using ScamWarning.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure DbContext with InMemory Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ScamWarningDB"));

// Register Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IWarningRepository, WarningRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Register Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IWarningService, WarningService>();
builder.Services.AddScoped<ICommentService, CommentService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ScamWarning API",
        Version = "v1",
        Description = "API for managing scam warnings, user authentication, and comments"
    });
});

var app = builder.Build();

// Seed the database with initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Seed Categories
    if (!context.Categories.Any())
    {
        context.Categories.AddRange(
            new Category { Id = 1, Name = "Phishing", Emoji = "🎣", Description = "Email and website scams that steal personal information" },
            new Category { Id = 2, Name = "Phone Scam", Emoji = "📞", Description = "Fraudulent phone calls and SMS messages" },
            new Category { Id = 3, Name = "Investment Scam", Emoji = "💰", Description = "Fake investment opportunities and Ponzi schemes" },
            new Category { Id = 4, Name = "Romance Scam", Emoji = "💔", Description = "Online dating and romance fraud" },
            new Category { Id = 5, Name = "Other", Emoji = "⚠️", Description = "Other types of scams" }
        );
    }
    
    // Seed users
    if (!context.Users.Any())
    {
        context.Users.AddRange(
            new User 
            { 
                Id = 1, 
                Username = "demo", 
                Email = "demo@test.com", 
                Password = "demo123",
                IsAdmin = false,
                CreatedAt = DateTime.UtcNow
            },
            new User 
            { 
                Id = 2, 
                Username = "root", 
                Email = "root@admin.com", 
                Password = "dadababa",
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow
            }
        );
    }
    
    // Seed sample warnings
    if (!context.Warnings.Any())
    {
        context.Warnings.AddRange(
            new Warning 
            { 
                Id = 1, 
                Title = "Fake Bank Email", 
                Description = "Received email claiming to be from bank asking for credentials",
                WarningSigns = "Suspicious sender email, urgency tactics, asking for personal information",
                ImageUrl = "",
                CategoryId = 1,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow
            },
            new Warning 
            { 
                Id = 2, 
                Title = "IRS Phone Call Scam", 
                Description = "Call claiming to be IRS threatening arrest if not paid immediately",
                WarningSigns = "Threatening language, demands immediate payment, asks for gift cards",
                ImageUrl = "",
                CategoryId = 2,
                AuthorId = 1,
                Status = "Approved",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
    
    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection disabled for development (frontend uses HTTP)
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();