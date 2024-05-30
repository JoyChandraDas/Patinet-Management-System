using PatinetManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Repositories
{
    public interface IDiseaseInformationRepository : IGenericRepository<DiseaseInformation>
    {
        void Update(DiseaseInformation disease);
        void UpdateRange(IEnumerable<DiseaseInformation> diseases);
    }
}
