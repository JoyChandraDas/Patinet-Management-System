using PatinetManagement.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.DataAccess.Domain
{
    public class Patientinformation
    {
        [Key]
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int DiseaseInformationId { get; set; }
        public Epilepsy EpilepsyId { get; set; }
        public string? NCDDetailsId { get; set; }
        public string? AllergiesDetailsId { get; set; }
    }
}
