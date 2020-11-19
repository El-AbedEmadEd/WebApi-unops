using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_unops.Model;

namespace WebApi_unops.Migrations
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.BookItems.Any())
                {
                    return;
                }

                context.BookItems.AddRange(
                    new BookItem
                    {
                        Id = 1,
                        Title = "Test",
                        Publisher = "Test",
                        Author = 1,
                        Description = "Test",
                        Authors = new Authors { 
                        Id = 1,
                        Name = "Autor",
                        Phone = "0599562831"
                        }
                    });

                
                context.SaveChanges();
            }
        }

    }
}
