namespace Kwikemark.Entities
{
    using System;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class DbKwikemartContext : IdentityDbContext<User>
    {
        public DbKwikemartContext(DbContextOptions<DbKwikemartContext> options)
            : base(options)
        {

        }

        public DbKwikemartContext()
        : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    => optionsBuilder.UseMySQL(connectionString: ConnectionString);

        private static string ConnectionString
        {
            get
            {
                const string databaseName = "dbkwikemartnetcore";
                const string databaseUser = "root";
                const string databasePass = "FerroIntegral*V1";

                return $"Server=localhost;" +
                       $"database={databaseName};" +
                       $"uid={databaseUser};" +
                       $"pwd={databasePass};" +
                       $"pooling=true;";
            }
        }
        public new DbSet<User> Users { get; set; }
        public new DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TypeProduct> TypeProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        modelBuilder.Entity<Product>()
            .HasOne(p => p.TypeProduct)
            .WithMany(b => b.Products)
            .HasForeignKey(f=> f.IdTypeProduct);
        }
    }
}
