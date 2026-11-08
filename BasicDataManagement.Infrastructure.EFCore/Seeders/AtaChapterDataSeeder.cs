using System.Threading.Tasks;
using BasicDataManagement.Domain.ATAChapterAgg;
using Microsoft.EntityFrameworkCore;

namespace BasicDataManagement.Infrastructure.EFCore.Seeders
{
    public static class AtaChapterDataSeeder
    {
        public static async Task SeedAsync(BasicDataContext context)
        {
            // فصل‌های ATA
            if (!await context.AtaChapters.AnyAsync())
            {
                var chapters = AtaChapterSeedData.GetAll();
                await context.AtaChapters.AddRangeAsync(chapters);
            }

            // Page Blockها
            if (!await context.MaintenancePageBlocks.AnyAsync())
            {
                var blocks = PageBlockSeedData.GetAll();
                await context.MaintenancePageBlocks.AddRangeAsync(blocks);
            }

            await context.SaveChangesAsync();
        }
    }
}