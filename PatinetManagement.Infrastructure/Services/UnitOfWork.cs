using PatinetManagement.Infrastructure.Context;
using PatinetManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IDiseaseInformationRepository DiseaseInformation { get; private set; }
        public IPatientRepository Patient { get; private set; }
        public IAllergiesRepository Allergies { get; private set; }
        public IAllergiesDetailsRepository AllergiesDetails { get; private set; }

        public INCDsRepository NCDs { get; private set; }
        public INCDsDetailsRepository NCDsDetails { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Patient = new PatientRepository(_dbContext);
            DiseaseInformation = new DiseaseInformationRepository(_dbContext);
            Allergies = new AllergiesRepository(_dbContext);
            AllergiesDetails = new AllergiesDetailsRepository(_dbContext);
            NCDs = new NCDsRepository(_dbContext);
            NCDsDetails = new NCDsDetailsRepository(_dbContext);
        }
        public async Task CommitAsync()
       => await _dbContext.SaveChangesAsync();

        public async Task RollbackAsync()
        => await _dbContext.DisposeAsync();
    }
}
