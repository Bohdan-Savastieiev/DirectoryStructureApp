using DirectoryStructureApp.BLL.DTOs;
using DirectoryStructureApp.BLL.Interfaces;
using DirectoryStructureApp.BLL.Mappers;
using DirectoryStructureApp.BLL.Services;
using DirectoryStructureApp.BLL.Validators;
using DirectoryStructureApp.DAL;
using DirectoryStructureApp.DAL.Interfaces;
using DirectoryStructureApp.DAL.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IDirectoryRepository, DirectoryRepository>();
builder.Services.AddScoped<IDirectoryService, DirectoryService>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddTransient<IValidator<DirectoryDto>, DirectoryDtoValidator>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Directories}/{action=Index}/{id?}");

app.Run();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
DbInitializer.Seed(context);