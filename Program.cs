using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromMilliseconds(10),
            errorCodesToAdd: null))
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

// add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Lars Oostenenk API (TestFase)",
        Version = "v1",
        Description = "A basic API to manage products.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Lars Oostenenk",
            Email = "Le.oostenenk@gmail.com",
            Url = new Uri("https://larsoostenenk.nl"),
        }
    });

    // Add XML documentation file
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // Voeg voorbeelden toe voor POST requests
    options.OperationFilter<SwaggerExamplesOperationFilter>();
});

var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

app.MapGet("/", () => "Welcome to the Lars Oostenenk API! Check /swagger for API documentation.");

// Use CORS
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();
app.Run();
