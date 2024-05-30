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
    public class AllergiesRepository : GenericRepository<Allergies>, IAllergiesRepository
    {
        protected readonly ApplicationDbContext _allergiesDb;

        public AllergiesRepository(ApplicationDbContext allergiesDb) : base(allergiesDb)
        {
            _allergiesDb = allergiesDb;
        }

        public void Update(Allergies allergies)
        => _allergiesDb.Allergie.Update(allergies);

        public void UpdateRange(IEnumerable<Allergies> allergies)
        => _allergiesDb.Allergie.UpdateRange(allergies);

    }
}
