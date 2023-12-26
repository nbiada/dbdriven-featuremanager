using DbDrivenFM.App.Data;
using Microsoft.EntityFrameworkCore;

namespace DbDrivenFM.App.Services;

public class CountryDataService : ICountryDataService
{
    private readonly IDbContextFactory<ApplicationDbContext> _factory;

    public CountryDataService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<Country>> Get()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Countries.AsNoTracking().ToListAsync();
    }
}

public interface ICountryDataService
{
    Task<List<Country>> Get();
}