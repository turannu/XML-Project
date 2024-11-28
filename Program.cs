using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using XML_Project.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Brewery API",
        Version = "v1",
        Description = "Details of breweries"
    });
});

// Database Context
builder.Services.AddDbContext<XML_ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("XML_ProjectContext")));

var app = builder.Build();

// Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brewery API V1");
        c.RoutePrefix = "swagger"; // Ensures swagger is accessible at /swagger
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();