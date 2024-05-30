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
    public class AllergiesDetailsRepository : GenericRepository<Allergies_Details>, IAllergiesDetailsRepository
    {
        protected readonly ApplicationDbContext _allergiesDetailsDb;

        public AllergiesDetailsRepository(ApplicationDbContext allergiesDetailsDb) : base(allergiesDetailsDb)
        {
            _allergiesDetailsDb = allergiesDetailsDb;
        }

        public void Update(Allergies_Details allergies_Details)
        => _allergiesDetailsDb.Allergies_Detail.Update(allergies_Details);

        public void UpdateRange(IEnumerable<Allergies_Details> allergies_Detailses)
        => _allergiesDetailsDb.Allergies_Detail.UpdateRange(allergies_Detailses);

    }
}
