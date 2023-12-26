using DbDrivenFM.App.Components;
using DbDrivenFM.App.Data;
using DbDrivenFM.App.Services;
using DbDrivenFM.App.Startup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddStartupIdentity();
builder.Services.AddStartupDatabase(builder.Configuration);


builder.Services.AddScoped<IFetchDataService, FetchDataService>();

// Register Feature Management and the custom feature filter
//   - this is the original implementation with a custom DbFeatureFilter
// builder.Services.AddFeatureManagement()
//     .AddFeatureFilter<DbFeatureFilter>();

// Register the custom Feature Manager
//  - this is the Db driven implementations of the Feature Manager 
builder.Services.AddScoped<IFeatureManager, DbFeatureManager>();

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

app.MapGet("/feature/{featureName}", async (
        [FromKeyedServices(key: DbFeatureManager.Name)] IFeatureManager? fm, 
        [FromRoute] string featureName) => 
    {
        if (fm is null)
        {
            return Results.NotFound();
        }
        var result = string.Format("Feature \"{0}\" is {1}", featureName,
            await fm.IsEnabledAsync(featureName) ? "enabled" : "disabled");
        return Results.Ok(new ResultResponse(true, result));
    });
app.Run();
