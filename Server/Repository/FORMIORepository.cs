using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace SPSITECH.Module.FORMIO.Repository
{
    public class FORMIORepository : IFORMIORepository, ITransientService
    {
        private readonly IDbContextFactory<FORMIOContext> _factory;

        public FORMIORepository(IDbContextFactory<FORMIOContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.FORMIO> GetFORMIOs(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.FORMIO.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.FORMIO GetFORMIO(int FORMIOId)
        {
            return GetFORMIO(FORMIOId, true);
        }

        public Models.FORMIO GetFORMIO(int FORMIOId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.FORMIO.Find(FORMIOId);
            }
            else
            {
                return db.FORMIO.AsNoTracking().FirstOrDefault(item => item.FORMIOId == FORMIOId);
            }
        }

        public Models.FORMIO AddFORMIO(Models.FORMIO FORMIO)
        {
            using var db = _factory.CreateDbContext();
            db.FORMIO.Add(FORMIO);
            db.SaveChanges();
            return FORMIO;
        }

        public Models.FORMIO UpdateFORMIO(Models.FORMIO FORMIO)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(FORMIO).State = EntityState.Modified;
            db.SaveChanges();
            return FORMIO;
        }

        public void DeleteFORMIO(int FORMIOId)
        {
            using var db = _factory.CreateDbContext();
            Models.FORMIO FORMIO = db.FORMIO.Find(FORMIOId);
            db.FORMIO.Remove(FORMIO);
            db.SaveChanges();
        }
    }
}
