using DbDrivenFM.App.Components;
using DbDrivenFM.App.Data;
using DbDrivenFM.App.Services;
using DbDrivenFM.App.Startup;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddStartupIdentity();
builder.Services.AddStartupDatabase(builder.Configuration);


builder.Services.AddScoped<IFetchDataService, FetchDataService>();

// Register Feature Management and the custom feature filter
//builder.Services.AddFeatureManagement()
//    .AddFeatureFilter<DbFeatureFilter>();

// Register the custom Feature Manager
builder.Services.AddKeyedScoped<IFeatureManager, DbFeatureManager>
    (DbFeatureManager.Name);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapGet("/test", 
    async ([FromKeyedServices(key: DbFeatureManager.Name)] IFeatureManager fm) => 
    {
        if (fm is null)
        {
            return Results.NotFound();
        }
        if (await fm.IsEnabledAsync(FeatureConstants.FetchData))
        {
            return Results.Ok("Feature is enabled");
        }
        else
        {
            return Results.Ok("Feature is disabled");
        }
    });
app.Run();
