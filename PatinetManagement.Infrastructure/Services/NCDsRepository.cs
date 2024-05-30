using PatinetManagement.DataAccess.Domain;
using PatinetManagement.Infrastructure.Context;
using PatinetManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Services
{
    public class NCDsRepository : GenericRepository<NCD>, INCDsRepository
    {
        protected readonly ApplicationDbContext _ncdsDb;

        public NCDsRepository(ApplicationDbContext ncdsDb) : base(ncdsDb)
        {
            _ncdsDb = ncdsDb;
        }

        public void Update(NCD ncd)
        => _ncdsDb.NCD.Update(ncd);
        public void UpdateRange(IEnumerable<NCD> ncds)
        => _ncdsDb.NCD.UpdateRange(ncds);
    }
}
