namespace DefinitionExtractionWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DEDatabaseEntities : DbContext
    {
        public DEDatabaseEntities()
            : base("name=DEDatabaseEntities")
        {
        }

        public virtual DbSet<Ascriptor> Ascriptors { get; set; }
        public virtual DbSet<DefinitionLink> DefinitionLinks { get; set; }
        public virtual DbSet<Definition> Definitions { get; set; }
        public virtual DbSet<Descriptor> Descriptors { get; set; }
        public virtual DbSet<Relation_types> Relation_types { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Definition>()
                .HasMany(e => e.DefinitionLinks)
                .WithRequired(e => e.Definition)
                .HasForeignKey(e => e.Definition_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Descriptor>()
                .HasMany(e => e.Ascriptors)
                .WithRequired(e => e.Descriptor)
                .HasForeignKey(e => e.Descriptor_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Descriptor>()
                .HasMany(e => e.DefinitionLinks)
                .WithRequired(e => e.Descriptor)
                .HasForeignKey(e => e.Descriptor_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Descriptor>()
                .HasMany(e => e.Definitions)
                .WithRequired(e => e.Descriptor)
                .HasForeignKey(e => e.Descriptor_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Descriptor>()
                .HasMany(e => e.Relations)
                .WithRequired(e => e.Descriptor)
                .HasForeignKey(e => e.Descriptor1_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Descriptor>()
                .HasMany(e => e.Relations1)
                .WithRequired(e => e.Descriptor1)
                .HasForeignKey(e => e.Descriptor2_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Relation_types>()
                .HasMany(e => e.Relations)
                .WithRequired(e => e.Relation_types)
                .HasForeignKey(e => e.Relation_type_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Definitions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.User_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
