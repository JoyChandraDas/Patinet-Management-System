using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.DataAccess.Domain
{
    public class Allergies
    {
        [Key]
        public int Id { get; set; }
        public string AllergiesName { get; set; }
    }
}
