using PatinetManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Repositories
{
    public interface IAllergiesDetailsRepository : IGenericRepository<Allergies_Details>
    {
        void Update(Allergies_Details allergies_Details);
        void UpdateRange(IEnumerable<Allergies_Details> allergies_Detailses);
    }
}
