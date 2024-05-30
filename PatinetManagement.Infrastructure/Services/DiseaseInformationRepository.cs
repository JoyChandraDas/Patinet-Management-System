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
    public class DiseaseInformationRepository : GenericRepository<DiseaseInformation>, IDiseaseInformationRepository
    {
        protected readonly ApplicationDbContext _diseaseDb;

        public DiseaseInformationRepository(ApplicationDbContext diseaseDb) : base(diseaseDb)
        {
            _diseaseDb = diseaseDb;
        }

        public void Update(DiseaseInformation disease)
        => _diseaseDb.DiseaseInformation.Update(disease);

        public void UpdateRange(IEnumerable<DiseaseInformation> diseases)
        => _diseaseDb.DiseaseInformation.UpdateRange(diseases);
    }
}
