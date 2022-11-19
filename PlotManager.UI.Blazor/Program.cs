using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using PlotManager.UI.Blazor.ClientServices.Features;
using PlotManager.UI.Blazor.ClientServices.PlotComplexes;
using PlotManager.UI.Blazor.ClientServices.Plots;
using PlotManager.UI.Blazor.ClientServices.Security;
using PlotManager.UI.Blazor.HttpClients.PlotManagerAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddHttpClient<IPlotManagerAPIClient, PlotManagerAPIClient>();
builder.Services.AddHttpClient<IPlotManagerAPIClient, PlotManagerAPISimpleClient>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IBlazorAuthenticationService, BlazorAuthenticationService>();
builder.Services.AddScoped<IAddBearerTokenService, AddBearerTokenService>();

builder.Services.AddScoped<IFeaturesClientService, FeaturesClientService>();
builder.Services.AddScoped<IPlotComplexesClientService, PlotComplexesClientService>();
builder.Services.AddScoped<IPlotsClientService, PlotsClientService>();

builder.Services.AddAuthorizationCore();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();