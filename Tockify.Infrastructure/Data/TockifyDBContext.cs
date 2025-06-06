using Microsoft.EntityFrameworkCore;
using Tockify.Domain.Models;

namespace Tockify.Infrastructure.Data
{
    public class TockifyDBContext : DbContext
    {

        public TockifyDBContext(DbContextOptions<TockifyDBContext> options)
            : base(options)
        {
        }
        public DbSet<ClientUserModel> ClientUsers { get; set; }
        public DbSet<CardModel> Cards { get; set; }
        public DbSet<TaskItemModel> TaskItem { get; set; }
        public DbSet<AdmUserModel> AdmUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
