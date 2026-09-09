using backend_development_assessment.api.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddControllers();
//database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("NeonDb"))
);
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers(); // ← activate their routes
// API Documentation
if (app.Environment.IsDevelopment())
{
    // Generate the OpenAPI document
    app.MapOpenApi();
    // Add the Scalar UI endpoint
    app.MapScalarApiReference();
}


app.MapGet("/", () => "Server Running");
app.MapGet("/test", () => "test endpoint");

app.Run();
