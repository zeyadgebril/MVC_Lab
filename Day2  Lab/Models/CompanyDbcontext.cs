using Microsoft.EntityFrameworkCore;

namespace Day2__Lab.Models
{
    public class CompanyDbcontext:DbContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<instructor> instructor { get; set; }
        public DbSet<Trainee> Trainee { get; set; }
        public DbSet<Course> Course { get; set; }

        public DbSet<crsResult> crsResult { get; set; }
        public CompanyDbcontext(DbContextOptions<CompanyDbcontext> option) : base(option)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ITI_MVC_Days;Integrated Security=True;TrustServerCertificate=True");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(d=>
            {
                d.HasKey(d => d.ID);
                d.Property(d => d.Name).HasMaxLength(30).IsRequired();
                d.Property(d=>d.Manager).HasMaxLength(30).IsRequired(false);
            });

            modelBuilder.Entity<instructor>(i => {
                i.HasKey(i => i.ID);
                i.Property(i=>i.name).HasMaxLength(30).IsRequired();
                i.Property(i => i.Image).IsRequired(false);
                i.Property(i=>i.Address).HasMaxLength(70).IsRequired(false);
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}
