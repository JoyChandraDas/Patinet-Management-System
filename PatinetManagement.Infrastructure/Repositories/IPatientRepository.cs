using PatinetManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Repositories
{
    public interface IPatientRepository : IGenericRepository<Patientinformation>
    {
        void Update(Patientinformation patientinformation);
        void UpdateRange(IEnumerable<Patientinformation> patientinformations);
    }
}
