using CMSAutomationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CMSAutomationAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Delta Table
        public DbSet<CptCodesetDelta> CptCodesetDelta { get; set; }
        public DbSet<RipeCodesets> RipeCodesets { get; set; }
        public DbSet<CPT_CodeSetMapping> CPT_CodeSetMappings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Table mapping
            // modelBuilder.Entity<CptCodesetDelta>().ToTable("cpt_codeset_delta");

            modelBuilder.Entity<CptCodesetDelta>(entity =>
            {
                entity.ToTable("cpt_codeset_delta");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
         .HasColumnName("id");

               
            });

        }
    }
}
