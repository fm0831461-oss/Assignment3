using Assignment3.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Book> Books { set; get; }
        public DbSet<Borrowing> Borrows { set; get; }
        public DbSet<Member>Members  { set; get; }
        public DbSet<Category>Categories  { set; get; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(e => e.UserName).IsUnique();
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Books)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Member>()
           .HasMany(e => e.Borrows)
           .WithOne(e => e.Member)
           .HasForeignKey(e => e.MemberId);

            modelBuilder.Entity<Book>()
           .HasMany(e => e.Borrows)
           .WithOne(e => e.Book)
           .HasForeignKey(e => e.BookId);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Programming", Description = "Programming and software development books" },
                new Category { CategoryId = 2, Name = "Database", Description = "Database and data management books" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "C++ Basics", Author = "John Smith", ISBN = "ISBN001", Price = 450, IsAvailable = true, CategoryId = 1 },
                new Book { BookId = 2, Title = "SQL Fundamentals", Author = "Sarah Ali", ISBN = "ISBN002", Price = 500, IsAvailable = false, CategoryId = 2 }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member { MemberId = 1, FullName = "Ahmed Hassan", Email = "ahmed@example.com", Phone = "01012345678" },
                new Member { MemberId = 2, FullName = "Sara Mohamed", Email = "sara@example.com", Phone = "01198765432" }
            );

            modelBuilder.Entity<Borrowing>().HasData(
                new Borrowing { BorrowingId = 1, BorrowDate = new DateTime(2026, 9, 20), ReturnDate = new DateTime(2026, 9, 25), MemberId = 1, BookId = 1 },
                new Borrowing { BorrowingId = 2, BorrowDate = new DateTime(2026, 9, 22), ReturnDate = null, MemberId = 2, BookId = 2 }
            );

            modelBuilder.Entity<Category>().HasIndex(e => e.Name).IsUnique();
            modelBuilder.Entity<Book>().HasIndex(e => e.ISBN).IsUnique();
            modelBuilder.Entity<Member>().HasIndex(e => e.Email).IsUnique();
        }
    }
}
