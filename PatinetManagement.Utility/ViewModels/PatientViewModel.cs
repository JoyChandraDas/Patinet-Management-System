using PatinetManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Utility.ViewModels
{
    public class PatientViewModel
    {
        public Patientinformation? Patient { get; set; }
        public List<Allergies_Details>? AllergiesDetails { get; set; }
        public List<NCD_Details>? NCD_Details { get; set; }
        public int? Id { get; set; }
        public string? PatientName { get; set; }
        public string? DiseaseName { get; set; }
        public string? EpilepsyName { get; set; }
        public string? NCDDetails { get; set; }
        public string? AllergiesDetailses { get; set; }

        public List<Allergies>? AllergiList { get; set; }
        public List<NCD>? NCDList { get; set; }

        public List<NCDDetailViewModel>? NCDDetailList { get; set; }
        public List<AllergyDetailViewModel>? AllergyDetailList { get; set; }
    }
}
