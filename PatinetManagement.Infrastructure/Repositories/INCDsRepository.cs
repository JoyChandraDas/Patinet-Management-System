using PatinetManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Repositories
{
    public interface INCDsRepository : IGenericRepository<NCD>
    {
        void Update(NCD ncd);
        void UpdateRange(IEnumerable<NCD> ncds);
    }
}
