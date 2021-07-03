using IVS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace IVS.Data.DataContext
{
    public class ServiceDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Flow> Flows { get; set; }

        public ServiceDbContext(DbContextOptions<ServiceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsersToVideos>()
                .HasKey(t => new { t.UserId, t.VideoId });

            modelBuilder.Entity<UsersToVideos>()
                .HasOne(uv => uv.User)
                .WithMany(u => u.UsersToVideos)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UsersToVideos>()
                .HasOne(uv => uv.Video)
                .WithMany(u => u.UsersToVideos)
                .HasForeignKey(u => u.VideoId);

            modelBuilder.Entity<UsersToGroups>()
                .HasKey(t => new { t.UserId, t.GroupId });

            modelBuilder.Entity<UsersToGroups>()
                .HasOne(uv => uv.User)
                .WithMany(u => u.UsersToGroups)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UsersToGroups>()
                .HasOne(uv => uv.Group)
                .WithMany(u => u.UsersToGroups)
                .HasForeignKey(u => u.GroupId);

            modelBuilder.Entity<UsersToFlows>()
                .HasKey(t => new { t.UserId, t.FlowId });

            modelBuilder.Entity<UsersToFlows>()
                .HasOne(uv => uv.User)
                .WithMany(u => u.UsersToFlows)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UsersToFlows>()
                .HasOne(uv => uv.Flow)
                .WithMany(u => u.UsersToFlows)
                .HasForeignKey(u => u.FlowId);

            modelBuilder.Entity<GroupsToVideos>()
                .HasKey(t => new { t.VideoId, t.GroupId });

            modelBuilder.Entity<GroupsToVideos>()
                .HasOne(uv => uv.Group)
                .WithMany(u => u.GroupsToVideos)
                .HasForeignKey(u => u.GroupId);

            modelBuilder.Entity<GroupsToVideos>()
                .HasOne(uv => uv.Video)
                .WithMany(u => u.GroupsToVideos)
                .HasForeignKey(u => u.VideoId);

            modelBuilder.Entity<GroupsToFlows>()
                .HasKey(t => new { t.FlowId, t.GroupId });

            modelBuilder.Entity<GroupsToFlows>()
                .HasOne(uv => uv.Flow)
                .WithMany(u => u.GroupsToFlows)
                .HasForeignKey(u => u.FlowId);

            modelBuilder.Entity<GroupsToFlows>()
                .HasOne(uv => uv.Group)
                .WithMany(u => u.GroupsToFlows)
                .HasForeignKey(u => u.GroupId);

            modelBuilder.Entity<FlowsToVideos>()
                .HasKey(t => new { t.FlowId, t.VideoId });

            modelBuilder.Entity<FlowsToVideos>()
                .HasOne(uv => uv.Flow)
                .WithMany(u => u.FlowsToVideos)
                .HasForeignKey(u => u.FlowId);

            modelBuilder.Entity<FlowsToVideos>()
                .HasOne(uv => uv.Video)
                .WithMany(u => u.FlowsToVideos)
                .HasForeignKey(u => u.VideoId);
        }
    }
}
