using BookingsWebApi.Configuration;
using BookingsWebApi.Models;

using Microsoft.EntityFrameworkCore;

namespace BookingsWebApi.Context;

public class BookingsDbContext : DbContext, IBookingsDbContext
{
    private readonly IConfiguration? _configuration;

    public BookingsDbContext(
        DbContextOptions<BookingsDbContext> options,
        IConfiguration configuration
    )
        : base(options)
    {
        _configuration = configuration;
    }

    public BookingsDbContext() { }

    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Hotel> Hotels { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_configuration == null)
        {
            return;
        }

        string connectionString = _configuration["ConnectionStrings:SqlServer"] ?? string.Empty;
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // PKs
        modelBuilder.Entity<Booking>().HasKey(b => b.Id);
        modelBuilder.Entity<Room>().HasKey(r => r.Id);
        modelBuilder.Entity<Hotel>().HasKey(h => h.Id);
        modelBuilder.Entity<City>().HasKey(c => c.Id);
        modelBuilder.Entity<User>().HasKey(u => u.Id);

        // Relations

        modelBuilder
            .Entity<Booking>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId);

        modelBuilder
            .Entity<Booking>()
            .HasOne(b => b.Room)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RoomId);

        modelBuilder
            .Entity<Room>()
            .HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId);

        modelBuilder
            .Entity<Hotel>()
            .HasOne(h => h.City)
            .WithMany(c => c.Hotels)
            .HasForeignKey(h => h.CityId);

        // Seeder
        modelBuilder.SeedData();
    }
}