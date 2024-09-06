using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookStoreWebapi.DBOperations
{
    public class DataGenerator
    {
        public static void Initilize(IServiceProvider serviceProvider) //bu serviceproveider aslında servisin proram çalıştığında çalışmasını sağlıyor programla birlikte ayağa kalkıyor gibi bir şey oluyor kısacası
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;

                context.Books.AddRange(
                         new Book
                         {
                            //  Id = 1,
                             Title = "Lean Startup",
                             GenreId = 1, //Personal Growth
                             PageCount = 200,
                             PublishDate = new DateTime(2001, 08, 12)
                         },
                        new Book
                        {
                            // Id = 2,
                            Title = "Herhald",
                            GenreId = 2, //Science Fiction
                            PageCount = 250,
                            PublishDate = new DateTime(2010, 05, 23)
                        },
                        new Book
                        {
                            // Id = 3,
                            Title = "Dune",
                            GenreId = 2, //Science Fiction
                            PageCount = 540,
                            PublishDate = new DateTime(2001, 12, 21)
                        }
                );

                context.SaveChanges();

            }

        }
    }
}