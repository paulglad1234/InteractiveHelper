using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InteractiveHelper.Db.Entities.User;
using InteractiveHelper.Db.Entities.Catalog;
using InteractiveHelper.Db.Entities.Quiz;

namespace InteractiveHelper.Db.Context;

public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemCharacteristic> ItemCharacteristics { get; set; }

    public DbSet<Quiz> Quizes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<ResultNode> Nodes { get; set; }

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

        #region Quiz

        builder.Entity<Quiz>().ToTable("quizes");
        builder.Entity<Quiz>().Property(x => x.Url).IsRequired().HasMaxLength(16);
        builder.Entity<Quiz>().HasAlternateKey(x => x.Url);
        builder.Entity<Quiz>().Property(x => x.Title).IsRequired().HasMaxLength(16);
        builder.Entity<Quiz>().Property(x => x.HelloMessage).IsRequired().HasMaxLength(200);

        builder.Entity<Question>().ToTable("questions");
        builder.Entity<Question>().Property(x => x.Text).IsRequired().HasMaxLength(300);

        builder.Entity<Answer>().ToTable("answers");
        builder.Entity<Answer>().Property(x => x.Text).IsRequired().HasMaxLength(200);

        builder.Entity<ResultNode>().ToTable("result_nodes");

        builder.Entity<Quiz>().HasOne(x => x.Head).WithOne(x => x.Quiz)
            .HasForeignKey<Quiz>(x => x.HeadId).IsRequired(false);
        builder.Entity<Quiz>().HasOne(x => x.Root).WithOne(x => x.Quiz)
            .HasForeignKey<Quiz>(x => x.RootId).IsRequired(false);

        builder.Entity<Question>().HasOne(x => x.Next).WithOne(x => x.Previous)
            .HasForeignKey<Question>(x => x.NextId).IsRequired(false);

        builder.Entity<Answer>().HasOne(x => x.Question).WithMany(x => x.Answers)
            .HasForeignKey(x => x.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<ResultNode>().HasOne(x => x.Question).WithMany(x => x.Nodes)
            .HasForeignKey(x => x.QuestionId);
        builder.Entity<ResultNode>().HasOne(x => x.Answer).WithMany(x => x.Nodes)
            .HasForeignKey(x => x.AnswerId);

        builder.Entity<ResultNode>().HasOne(x => x.Parent).WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId).IsRequired(false);
        builder.Entity<ResultNode>().HasMany(x => x.Items).WithMany(x => x.Results)
            .UsingEntity(e => e.ToTable("item_results"));

        #endregion
    }
}
