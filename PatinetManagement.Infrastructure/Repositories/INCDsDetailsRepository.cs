using PatinetManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Repositories
{
    public interface INCDsDetailsRepository : IGenericRepository<NCD_Details>
    {
        void Update(NCD_Details nCD_Details);
        void UpdateRange(IEnumerable<NCD_Details> nCD_Detailses);
    }
}
