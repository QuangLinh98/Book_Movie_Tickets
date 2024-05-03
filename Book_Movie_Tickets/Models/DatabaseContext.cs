using Microsoft.EntityFrameworkCore;

namespace EDN.Models
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Screenings>()
               .HasMany(s => s.bookings)
               .WithOne(b => b.Screening)
               .HasForeignKey(b => b.screening_id);

            modelBuilder.Entity<Movies>()
                .HasMany(m => m.screenings)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.movie_id);

            modelBuilder.Entity<Theaters>()
                .HasMany(t => t.screenings)
                .WithOne(s => s.Theater)
                .HasForeignKey(s => s.theater_id);

            modelBuilder.Entity<Customers>()
                .HasMany(c => c.bookings)
                .WithOne(b => b.Customer)
                .HasForeignKey(b => b.customer_id);

			modelBuilder.Entity<Bookings>()
				.HasOne(b => b.Screening)
				.WithMany(s => s.bookings)
				.HasForeignKey(b => b.screening_id);
		}
        public DbSet<Bookings> bookings { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Screenings> Screenings { get; set; }
        public DbSet<Theaters> Theaters { get; set; }

    }
}
