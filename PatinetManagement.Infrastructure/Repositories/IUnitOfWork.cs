using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Repositories
{
    public interface IUnitOfWork
    {
        IDiseaseInformationRepository DiseaseInformation { get;}
        IPatientRepository Patient { get; }
        IAllergiesRepository Allergies { get; }
        IAllergiesDetailsRepository AllergiesDetails { get; }
        INCDsRepository NCDs { get; }
        INCDsDetailsRepository NCDsDetails { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
