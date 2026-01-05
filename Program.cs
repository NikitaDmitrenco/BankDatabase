using BankDatabase;
using BankDatabase.Components;
using BankDatabase.Infostructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContextFactory<ApplicationContext>(opt =>
        opt.UseSqlite($"Data Source=bankdatabase.db")
        .EnableSensitiveDataLogging());
}
else
{
    builder.Services.AddDbContextFactory<ApplicationContext>(opt =>
        opt.UseSqlite($"Data Source=bankdatabase.db"));
}

builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<CustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
