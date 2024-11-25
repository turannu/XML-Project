using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XML_Project.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<XML_ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("XML_ProjectContext") ?? throw new InvalidOperationException("Connection string 'XML_ProjectContext' not found.")));
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


app.Run();
