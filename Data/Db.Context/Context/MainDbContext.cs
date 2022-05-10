using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InteractiveHelper.Db.Entities.User;
using InteractiveHelper.Db.Entities.Catalog;
using InteractiveHelper.Db.Entities.TheTest;

namespace InteractiveHelper.Db.Context;

public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemCharacteristic> ItemCharacteristics { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region User

        builder.Entity<User>().ToTable("users");
        builder.Entity<IdentityRole<Guid>>().ToTable("user_roles");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

        #endregion

        #region Catalog

        builder.Entity<Brand>().ToTable("brands");
        builder.Entity<Brand>().Property(x => x.Name).IsRequired();
        builder.Entity<Brand>().Property(x => x.Name).HasMaxLength(50);
        builder.Entity<Brand>().HasIndex(x => x.Name).IsUnique();

        builder.Entity<Category>().ToTable("categories");
        builder.Entity<Category>().Property(x => x.Title).IsRequired();
        builder.Entity<Category>().Property(x => x.Title).HasMaxLength(100);
        builder.Entity<Category>().HasIndex(x => x.Title).IsUnique();

        builder.Entity<Characteristic>().ToTable("characteristics");
        builder.Entity<Characteristic>().Property(x => x.Name).IsRequired();
        builder.Entity<Characteristic>().Property(x => x.Name).HasMaxLength(30);
        builder.Entity<Characteristic>().HasOne(x => x.Category).WithMany(x => x.Characteristics)
            .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<Characteristic>().HasIndex(x => new { x.Name, x.CategoryId }).IsUnique();

        builder.Entity<Item>().ToTable("items");
        builder.Entity<Item>().Property(x => x.Name).IsRequired();
        builder.Entity<Item>().Property(x => x.Name).HasMaxLength(200);
        builder.Entity<Item>().Property(x => x.Image).HasMaxLength(2048);
        //builder.Entity<Item>().Property(x => x.Price).HasPrecision(10,2); something went wrong with it

        builder.Entity<Item>().HasOne(x => x.Brand).WithMany(x => x.Items)
            .HasForeignKey(x => x.BrandId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Item>().HasOne(x => x.Category).WithMany(x => x.Items)
            .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Item>().HasMany(x => x.Characteristics).WithMany(x => x.Items)
            .UsingEntity<ItemCharacteristic>(
                j => j
                     .HasOne(ic => ic.Characteristic)
                     .WithMany(characteristic => characteristic.ItemCharacteristics)
                     .HasForeignKey(ic => ic.CharacteristicId)
                     .OnDelete(DeleteBehavior.Cascade), // will it delete the item then?
                j => j
                     .HasOne(ic => ic.Item)
                     .WithMany(item => item.ItemCharacteristics)
                     .HasForeignKey(ic => ic.ItemId)
                     .OnDelete(DeleteBehavior.Cascade), // will it delete the characteristic then?
                j =>
                {
                    j.Property(ic => ic.Value).HasMaxLength(50);
                    j.Property(ic => ic.Value).HasDefaultValue("-");
                    j.HasKey(t => new { t.ItemId, t.CharacteristicId });
                });

        builder.Entity<ItemCharacteristic>().ToTable("item_characteristics");

        #endregion

        #region TheTest

        builder.Entity<TheTest>().ToTable("the_tests");
        //builder.Entity<TheTest>().HasIndex(x => x.IsActive).HasFilter("[IsActive] = true").IsUnique();

        builder.Entity<Question>().ToTable("questions");
        builder.Entity<Question>().Property(x => x.Text).IsRequired();
        builder.Entity<Question>().Property(x => x.Text).HasMaxLength(500);
        builder.Entity<Question>().HasOne(x => x.Test).WithMany(x => x.Questions)
            .HasForeignKey(x => x.TestId).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Answer>().ToTable("answers");
        builder.Entity<Answer>().Property(x => x.Text).HasMaxLength(200);
        builder.Entity<Answer>().Property(x => x.Text).IsRequired();
        builder.Entity<Answer>().HasOne(x => x.Question).WithMany(x => x.Answers)
            .HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Result>().ToTable("results");
        builder.Entity<Result>().Property(x => x.Conclusion).IsRequired();
        builder.Entity<Result>().Property(x => x.Conclusion).HasMaxLength(1000);
        builder.Entity<Result>().HasOne(x => x.Test).WithMany(x => x.Results)
            .HasForeignKey(x => x.TestId).OnDelete(DeleteBehavior.Cascade);

        // it even made the indexes right, according to THETEST migration.
        // Because searching is supposed to be done excactly by x.Results
        builder.Entity<Result>().HasMany(x => x.Answers).WithMany(x => x.Results)
            .UsingEntity(e => e.ToTable("result_answers"));
        builder.Entity<Result>().HasMany(x => x.Items).WithMany(x => x.Results)
            .UsingEntity(e => e.ToTable("itemset_results"));

        #endregion
    }
}
