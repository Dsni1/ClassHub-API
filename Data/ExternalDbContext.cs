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

            // ===== USER =====
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.User_name)
                .HasColumnName("user_name")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

            // ===== ROLE =====
            modelBuilder.Entity<Role>()
                .ToTable("Roles")
                .HasKey(r => r.Id);

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .HasColumnName("name")
                .IsRequired();

            // ===== ORGANISATION =====
            modelBuilder.Entity<Organisation>()
                .ToTable("Organisations")
                .HasKey(o => o.Id);

            modelBuilder.Entity<Organisation>()
                .Property(o => o.Name)
                .HasColumnName("name")
                .IsRequired();

            // ===== USERROLE (COMPOSITE KEY) =====
            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles");

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.RoleId, ur.UserId, ur.OrganisationId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Organisation)
                .WithMany(o => o.UserRoles)
                .HasForeignKey(ur => ur.OrganisationId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===== GROUP =====
            modelBuilder.Entity<Group>()
                .ToTable("Groups")
                .HasKey(g => g.Id);

            modelBuilder.Entity<Group>()
                .Property(g => g.Name)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Organisation)
                .WithMany(o => o.Groups)
                .HasForeignKey(g => g.OrganisationId);

            // ===== GROUPUSER (COMPOSITE KEY) =====
            modelBuilder.Entity<GroupUser>()
                .ToTable("GroupUsers");

            modelBuilder.Entity<GroupUser>()
                .HasKey(gu => new { gu.GroupId, gu.UserId });

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.Group)
                .WithMany(g => g.GroupUsers)
                .HasForeignKey(gu => gu.GroupId);

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.GroupUsers)
                .HasForeignKey(gu => gu.UserId);

            // ===== CHATROOM =====
            modelBuilder.Entity<ChatRoom>()
                .ToTable("ChatRooms")
                .HasKey(cr => cr.Id);

            modelBuilder.Entity<ChatRoom>()
                .Property(cr => cr.Type)
                .HasColumnName("type")
                .IsRequired();

            modelBuilder.Entity<ChatRoom>()
                .Property(cr => cr.Created_At)
                .HasColumnName("created_at")
                .IsRequired();

            modelBuilder.Entity<ChatRoom>()
                .HasOne(cr => cr.Group)
                .WithMany(g => g.ChatRooms)
                .HasForeignKey(cr => cr.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===== CHATROOMUSER (COMPOSITE KEY) =====
            modelBuilder.Entity<ChatRoomUser>()
                .ToTable("ChatRoomUsers");

            modelBuilder.Entity<ChatRoomUser>()
                .HasKey(cu => new { cu.ChatRoomId, cu.UserId });

            modelBuilder.Entity<ChatRoomUser>()
                .HasOne(cu => cu.ChatRoom)
                .WithMany(cr => cr.ChatRoomUsers)
                .HasForeignKey(cu => cu.ChatRoomId);

            modelBuilder.Entity<ChatRoomUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.ChatRoomUsers)
                .HasForeignKey(cu => cu.UserId);

            // ===== MESSAGE =====
            modelBuilder.Entity<Message>()
                .ToTable("Messages")
                .HasKey(m => m.Id);

            modelBuilder.Entity<Message>()
                .Property(m => m.Text)
                .HasColumnName("text")
                .IsRequired();

            modelBuilder.Entity<Message>()
                .Property(m => m.Created_At)
                .HasColumnName("created_at")
                .IsRequired();

            modelBuilder.Entity<Message>()
                .HasOne(m => m.ChatRoom)
                .WithMany(cr => cr.Messages)
                .HasForeignKey(m => m.ChatRoomId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);
        }
    }
}
