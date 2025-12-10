using Microsoft.EntityFrameworkCore;
using ClassHub.Models;

namespace ClassHub.Data
{
    public class ExternalDbContext : DbContext
    {
        public ExternalDbContext(DbContextOptions<ExternalDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Organisation>().ToTable("Organisations");
            modelBuilder.Entity<Group>().ToTable("Groups");
            modelBuilder.Entity<ChatRoom>().ToTable("ChatRooms");
            modelBuilder.Entity<Message>().ToTable("Messages");

            // ===== USERROLE =====
            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles")
                .HasKey(ur => new { ur.RoleId, ur.UserId, ur.OrganisationId });

            // ===== GROUPUSER =====
            modelBuilder.Entity<GroupUser>()
                .ToTable("GroupUsers")
                .HasKey(gu => new { gu.GroupId, gu.UserId });

            // ===== CHATROOMUSER =====
            modelBuilder.Entity<ChatRoomUser>()
                .ToTable("ChatRoomUsers")
                .HasKey(cu => new { cu.ChatRoomId, cu.UserId });

            // ===== MESSAGE =====
            modelBuilder.Entity<Message>()
                .Property(m => m.CreatedAt)
                .HasColumnName("created_at");

            modelBuilder.Entity<ChatRoom>()
                .Property(cr => cr.CreatedAt)
                .HasColumnName("created_at");
        }

    }
}
