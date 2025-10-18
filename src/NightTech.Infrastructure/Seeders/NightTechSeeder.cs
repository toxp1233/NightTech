using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NightTech.Domain.Constants;
using NightTech.Infrastructure.Persistance;


namespace NightTech.Infrastructure.Seeders;

internal class NightTechSeeder(NightTechDbContext dbContext) : INightTechSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}
