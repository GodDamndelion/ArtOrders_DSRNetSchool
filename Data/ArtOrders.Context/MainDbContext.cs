using ArtOrders.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ArtOrders.Context
{
    public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> //Заменяем DbContext на IdentityDbContext
    {
        //public DbSet<User> Users { get; set; } Создано автоматически
        public DbSet<Message> Messages { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<PriceListItem> PriceListItems { get; set; }
        public DbSet<WorkExampleItem> WorkExampleItems { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Order> Orders { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Вот эти таблички создаются автоматически из IdentityDbContext
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("user_identity_roles");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("user_role_owners");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("user_role_claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");

            modelBuilder.Entity<User>().Property(u => u.Role).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Nickname).IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.Nickname).IsUnique();
            modelBuilder.Entity<User>().HasOne(u => u.Avatar).WithOne(i => i.User).HasPrincipalKey<User>(u => u.AvatarId);
            
            modelBuilder.Entity<Message>().ToTable("messages");
            modelBuilder.Entity<Message>().Property(m => m.ChatId).IsRequired();
            modelBuilder.Entity<Message>().HasOne(m => m.Chat).WithMany(c => c.Messages).HasForeignKey(m => m.ChatId);
            modelBuilder.Entity<Message>().HasOne(m => m.User).WithMany(u => u.Messages).HasForeignKey(m => m.UserId);
            modelBuilder.Entity<Message>().HasOne(m => m.Image).WithMany(i => i.Messages).HasForeignKey(m =>m.ImageId);

            modelBuilder.Entity<Image>().ToTable("images");
            modelBuilder.Entity<Image>().Property(i => i.Link).IsRequired();
            modelBuilder.Entity<Image>().HasIndex(i => i.Link).IsUnique();

            modelBuilder.Entity<PriceListItem>().ToTable("price_list_items");
            modelBuilder.Entity<PriceListItem>().Property(p => p.UserId).IsRequired();
            modelBuilder.Entity<PriceListItem>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<PriceListItem>().Property(p => p.Price).IsRequired();
            modelBuilder.Entity<PriceListItem>().HasOne(p => p.User).WithMany(u => u.PriceList).HasForeignKey(p => p.UserId);

            modelBuilder.Entity<WorkExampleItem>().ToTable("work_example_items");
            modelBuilder.Entity<WorkExampleItem>().Property(w => w.UserId).IsRequired();
            modelBuilder.Entity<WorkExampleItem>().Property(w => w.ImageId).IsRequired();
            modelBuilder.Entity<WorkExampleItem>().HasOne(w => w.User).WithMany(u => u.WorkExamples).HasForeignKey(w => w.UserId);
            modelBuilder.Entity<WorkExampleItem>().HasOne(w => w.Image).WithOne(i => i.WorkExampleItem).HasPrincipalKey<WorkExampleItem>(w => w.ImageId);

            modelBuilder.Entity<Chat>().ToTable("chats");
            modelBuilder.Entity<Chat>().Property(w => w.CustomerId).IsRequired();
            modelBuilder.Entity<Chat>().Property(w => w.ArtistId).IsRequired();
            modelBuilder.Entity<Chat>().HasOne(c => c.Order).WithOne(o => o.Chat).HasPrincipalKey<Chat>(c => c.OrderId);
            modelBuilder.Entity<Chat>().HasOne(c => c.Customer).WithMany(u => u.Chats).HasForeignKey(c => c.CustomerId);
            modelBuilder.Entity<Chat>().HasOne(c => c.Artist).WithMany(u => u.Chats).HasForeignKey(c => c.ArtistId);

            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Order>().Property(o => o.Name).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.Status).IsRequired();
            modelBuilder.Entity<Order>().Property(o => o.ChatLink).IsRequired();
            modelBuilder.Entity<Order>().HasIndex(o => o.ChatLink).IsUnique();
            modelBuilder.Entity<Order>().Property(o => o.EditsNumber).IsRequired();
            modelBuilder.Entity<Order>().HasOne(o => o.Customer).WithMany(u => u.Orders).HasForeignKey(o => o.CustomerId);
            modelBuilder.Entity<Order>().HasOne(o => o.Artist).WithMany(u => u.Orders).HasForeignKey(o => o.ArtistId);
            modelBuilder.Entity<Order>().HasOne(o => o.CurrentResultImage).WithOne(i => i.Order).HasPrincipalKey<Order>(o => o.CurrentResultId);
        }
    }
}
