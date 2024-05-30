using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Utility.ViewModels
{
    public class DiseasesViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Disease Name is Required")]
        public string DiseaseName { get; set; }
    }
}
