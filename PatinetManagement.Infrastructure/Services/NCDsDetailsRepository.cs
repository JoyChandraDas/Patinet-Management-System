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
    public class NCDsDetailsRepository : GenericRepository<NCD_Details>, INCDsDetailsRepository
    {
        protected readonly ApplicationDbContext _ncdsDetailsDb;

        public NCDsDetailsRepository(ApplicationDbContext ncdsDetailsDb) : base(ncdsDetailsDb)
        {
            _ncdsDetailsDb = ncdsDetailsDb;
        }

        public void Update(NCD_Details nCD_Details)
        => _ncdsDetailsDb.NCD_Detail.Update(nCD_Details);
        public void UpdateRange(IEnumerable<NCD_Details> nCD_Detailses)
        => _ncdsDetailsDb.NCD_Detail.UpdateRange(nCD_Detailses);
    }
}
