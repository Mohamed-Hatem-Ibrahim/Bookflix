using Microsoft.EntityFrameworkCore;

namespace Bookflix.Models.Context
{
	public class BookflixDbContext : DbContext
	{
		public BookflixDbContext(DbContextOptions<BookflixDbContext> options) : base(options)
		{}

		public DbSet<Book> Books { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<SoldBook> SoldBooks { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SoldBook>().HasKey(x => new { x.BookISBN, x.SellDate });
			base.OnModelCreating(modelBuilder);
		}
	}
}
