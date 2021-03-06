using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using DataAccess.Concrete.Configurations;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Bu context birden fazla provider i�in migration takibi yap�ld��� i�in
    /// varsay�lan olarak Postg db'si �zerinden �al���r. E�er sql ge�mek isterseniz
    /// AddDbContext eklerken bundan t�reyen MsDbContext'i kullan�n�z.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        protected readonly IConfiguration configuration;

        /// <summary>
        /// constructor da IConfiguration al�yoruz ki, birden fazla db ye parallel olarak
        /// migration yaratabiliyoruz.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions options, IConfiguration configuration)
          : base(options)
        {
            this.configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GroupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GroupClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OperationClaimEntityConfiguration());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseNpgsql(configuration.GetConnectionString("OASPgContext")));
            }
        }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupClaim> GroupClaims { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
    }
}