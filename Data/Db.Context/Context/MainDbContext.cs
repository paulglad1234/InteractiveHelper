using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InteractiveHelper.Db.Entities;

namespace InteractiveHelper.Db.Context;

public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<Item> Items { get; set; }
    //public DbSet<ItemCharacteristic> ItemCharacteristics { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("users");
        builder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

        builder.Entity<Brand>().ToTable("brands");
        builder.Entity<Brand>().Property(x => x.Name).IsRequired();
        builder.Entity<Brand>().Property(x => x.Name).HasMaxLength(50);

        builder.Entity<Category>().ToTable("categories");
        builder.Entity<Category>().Property(x => x.Title).IsRequired();
        builder.Entity<Category>().Property(x => x.Title).HasMaxLength(100);
        builder.Entity<Category>().HasIndex(x => x.Title).IsUnique();

        builder.Entity<Characteristic>().ToTable("characteristics");
        builder.Entity<Characteristic>().Property(x => x.Name).IsRequired();
        builder.Entity<Characteristic>().Property(x => x.Name).HasMaxLength(30);
        builder.Entity<Characteristic>().HasOne(x => x.Category).WithMany(x => x.Characteristics)
            .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Item>().ToTable("items");
        builder.Entity<Item>().Property(x => x.Name).IsRequired();
        builder.Entity<Item>().Property(x => x.Name).HasMaxLength(200);

        builder.Entity<Item>().HasOne(x => x.Brand).WithMany(x => x.Items)
            .HasForeignKey(x => x.BrandId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Item>().HasOne(x => x.Category).WithMany(x => x.Items)
            .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Item>().HasMany(x => x.Characteristics).WithMany(x => x.Items)
            .UsingEntity<ItemCharacteristic>().ToTable("item_characteristics");

        //builder.Entity<ItemCharacteristic>().Property(x => x.Value).HasMaxLength(20);
    }
}
