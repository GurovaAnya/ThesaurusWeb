﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DefinitionExtractionWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DEDatabaseEntities : DbContext
    {
        public DEDatabaseEntities()
            : base("name=DEDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ascriptor> Ascriptors { get; set; }
        public virtual DbSet<DefinitionLink> DefinitionLinks { get; set; }
        public virtual DbSet<Definition> Definitions { get; set; }
        public virtual DbSet<Descriptor> Descriptors { get; set; }
        public virtual DbSet<Relation_types> Relation_types { get; set; }
        public virtual DbSet<Relation> Relations { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
