using Microsoft.EntityFrameworkCore;

namespace Bookflix.Models.Context
{
	public class BookflixDbContext : DbContext
	{
		public BookflixDbContext(DbContextOptions<BookflixDbContext> options) : base(options)
		{}

		public virtual DbSet<Book> Books { get; set; }
		public virtual DbSet<Publisher> Publishers { get; set; }
		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<SoldBook> SoldBooks { get; set; }
		public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SoldBook>().HasKey(x => new { x.BookISBN, x.SellDate });
			modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.ISBN, bc.CategoryID });
			base.OnModelCreating(modelBuilder);
		}
    }
}
