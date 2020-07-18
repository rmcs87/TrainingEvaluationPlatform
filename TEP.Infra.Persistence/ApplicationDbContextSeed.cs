using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEP.Domain.Entities;

namespace TEP.Infra.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static Task SeedDefaultUserAsync()
        {
            throw new NotImplementedException();
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(                    
                    new Category("EPI", "C_01")
                    {
                        Created = DateTime.Now
                    },
                    new Category("PANEL", "C_02")
                    {
                        Created = DateTime.Now
                    },
                    new Category("TOOL", "C_03")
                    {
                        Created = DateTime.Now
                    },
                    new Category("CLOTHES", "C_04")
                    {
                        Created = DateTime.Now
                    }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
