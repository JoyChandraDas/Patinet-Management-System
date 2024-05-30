using PatinetManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Repositories
{
    public interface IAllergiesRepository : IGenericRepository<Allergies>
    {
        void Update(Allergies allergies);
        void UpdateRange(IEnumerable<Allergies> allergies);
    }
}
