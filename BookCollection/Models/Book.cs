using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace BookCollection.Models
{

    public class Book
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Author { get; set; }

        public required int Year { get; set; }

        public string? Publisher { get; set; }

        public string? Description { get; set; }


    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
