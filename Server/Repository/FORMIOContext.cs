using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace SPSITECH.Module.FORMIO.Repository
{
    public class FORMIOContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.FORMIO> FORMIO { get; set; }

        public FORMIOContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.FORMIO>().ToTable(ActiveDatabase.RewriteName("SPSITECHFORMIO"));
        }
    }
}