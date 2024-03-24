using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LebUpwor.core.Models
{
    public class UpworkLebContext : DbContext
    {
        public UpworkLebContext()
        {

        }

        public UpworkLebContext(DbContextOptions<UpworkLebContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<AppliedToTask> AppliedToTasks { get; set; } = null!;
        public virtual DbSet<CashOutHistory> CashOutHistories { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<TokenHistory> TokenHistories { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;

        public virtual DbSet<NewJob> NewJobs { get; set; } = null!;
        public virtual DbSet<Notification>Notifications { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{

            //    optionsBuilder.UseLazyLoadingProxies();
            //}
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = LAPTOP-IQGVBR7N\\SQLEXPRESS; Database = LebaneseUpwork; Trusted_Connection = True; Encrypt = False;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                 .HasOne(j => j.User)
                 .WithMany(u => u.JobsPosted)
                 .HasForeignKey(j => j.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Job>()
                .HasOne(j => j.SelectedUser)
                .WithMany(u => u.JobsFinished)
                .HasForeignKey(j => j.SelectedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between User and Message
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between User and TokenHistory
            modelBuilder.Entity<TokenHistory>()
                .HasOne(th => th.Sender)
                .WithMany(u => u.SentTokenHistories)
                .HasForeignKey(th => th.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TokenHistory>()
                .HasOne(th => th.Receiver)
                .WithMany(u => u.ReceivedTokenHistories)
                .HasForeignKey(th => th.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure other relationships as needed
            modelBuilder.Entity<AppliedToTask>()
                .HasOne(u => u.User)
                .WithMany(u => u.AppliedToTasks)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AppliedToTask>()
                .HasOne(u => u.Job)
                .WithMany(u => u.AppliedUsers)
                .HasForeignKey(u => u.JobId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>()
                      .HasOne(t => t.AddedByUser)
                      .WithMany() // Assuming one user can add multiple tags
                      .HasForeignKey(t => t.AddedByUserId)
                      .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Report>()
                 .HasOne(t => t.ReportedBy)
                 .WithMany()
                 .HasForeignKey(t => t.ReportedById)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Report>()
                 .HasOne(t => t.ReportedUser)
                 .WithMany()
                 .HasForeignKey(t => t.ReportedUserId)
                 .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Report>()
                  .HasOne(t => t.ReportedPost)
                  .WithMany()
                  .HasForeignKey(t => t.ReportedPostId)
                  .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Report>()
                 .HasOne(t => t.ReportedMessage)
                 .WithMany()
                 .HasForeignKey(t => t.ReportedMessageId)
                 .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<NewJob>()
              .HasKey(t => new { t.JobId, t.UserId });
            modelBuilder.Entity<NewJob>()
                 .HasOne(t => t.User)
                 .WithMany()
                 .HasForeignKey(t => t.UserId);
            // .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<NewJob>()
             .HasOne(t => t.Job)
             .WithOne()
             .HasForeignKey<NewJob>(t => t.JobId);
             //.OnDelete(DeleteBehavior.NoAction);
        }
    }
}
