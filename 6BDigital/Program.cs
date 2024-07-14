using _6BDigital.Application;
using _6BDigital.Data;
using _6BDigital.Domain.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Blazored.LocalStorage;


var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder();
configBuilder.AddJsonFile("Appsettings.json");

IConfigurationRoot config = configBuilder.Build();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddSingleton<IAppointmentApplication, AppointmentApplication>();
builder.Services.AddSingleton<IUserApplication, UserApplication>();


builder.Services.AddSingleton<IAppointmentData, AppointmentData>(x => new AppointmentData(config.GetValue<string>("ConnectionString")));

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
