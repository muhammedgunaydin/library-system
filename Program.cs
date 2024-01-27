using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;

string connString = "Host=localhost;Username=postgres;Password=test;Database=libauto";

using (var conn = new NpgsqlConnection(connString))
{
    try
    {
        conn.Open();
        Console.WriteLine("Succesfully connected to Psql");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(connString));



// Add services to the container.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<LibraryDbContext>();
    context.Database.Migrate();
}

app.Run();
