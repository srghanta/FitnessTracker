using FitnessTracker.Data;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// -------------------- DbContext --------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

// -------------------- Identity --------------------
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// -------------------- JWT Authentication --------------------
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// -------------------- AutoMapper --------------------
builder.Services.AddAutoMapper(typeof(Program));

// -------------------- Controllers --------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// -------------------- Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// -------------------- Seed Default Data --------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Seed default user
    if (!context.UserProfiles.Any())
    {
        context.UserProfiles.Add(new UserProfile { UserName = "DefaultUser" });
        context.SaveChanges();
        Console.WriteLine("Default user created with ID 1.");
    }

    var defaultUser = context.UserProfiles.First();

    // Seed default workout
    if (!context.Workouts.Any())
    {
        context.Workouts.Add(new Workout
        {
            Name = "Seed Workout",
            DurationMinutes = 30,
            Date = DateTime.UtcNow,
            UserProfileId = defaultUser.Id
        });
        context.SaveChanges();
        Console.WriteLine("Default workout created.");
    }

    var defaultWorkout = context.Workouts.First();

    // Seed default workout log
    if (!context.WorkoutLogs.Any())
    {
        context.WorkoutLogs.Add(new WorkoutLog
        {
            WorkoutId = defaultWorkout.Id,
            Notes = "First workout log",
            Date = DateTime.UtcNow
        });
        context.SaveChanges();
        Console.WriteLine("Default workout log created.");
    }
}

// -------------------- Middleware --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
