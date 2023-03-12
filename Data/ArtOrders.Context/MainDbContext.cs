using ArtOrders.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtOrders.Context
{
    public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid> //Заменяем DbContext на IdentityDbContext
    {
        // TODO: Настроить тут связи
        //public DbSet<Message> Messages { get; set; }
        public DbSet<Image> Images { get; set; }
        //public DbSet<PriceListItem> PriceListItems { get; set; }
        //public DbSet<WorkExampleItem> WorkExampleItems { get; set; }
        //public DbSet<Chat> Chats { get; set; }
        //public DbSet<Order> Orders { get; set; }

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

        }
    }
}
