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
    public class PatientRepository : GenericRepository<Patientinformation>, IPatientRepository
    {
        protected readonly ApplicationDbContext _patientDb;

        public PatientRepository(ApplicationDbContext patientDb) : base(patientDb)
        {
            _patientDb = patientDb;
        }

        public void Update(Patientinformation patientinformation)
        => _patientDb.Patientinformation.Update(patientinformation);

        public void UpdateRange(IEnumerable<Patientinformation> patientinformations)
        => _patientDb.Patientinformation.UpdateRange(patientinformations);
    }
}
