using Microsoft.EntityFrameworkCore;

using StackOverHead.Auth.Infra.Mapping;
using StackOverHead.Auth.Infra.Models;

namespace StackOverHead.Auth.Infra.Context
{
    public class StackOverHeadAuthDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public StackOverHeadAuthDbContext() : base() { }
        public StackOverHeadAuthDbContext(DbContextOptions<StackOverHeadAuthDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
