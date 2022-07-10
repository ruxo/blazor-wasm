using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Wasm.Server.Persistence;
using Wasm.Shared.Features.ManageTrails;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(TrailViewModel).Assembly));
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BlazingTrailsContext>(opts => opts.UseSqlite(builder.Configuration.GetConnectionString(nameof(BlazingTrailsContext))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();