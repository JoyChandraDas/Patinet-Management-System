using Microsoft.EntityFrameworkCore;
using PatinetManagement.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatinetManagement.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Patientinformation> Patientinformation { get; set; }
        public DbSet<DiseaseInformation> DiseaseInformation { get; set; }
        public DbSet<Allergies> Allergie { get; set; }
        public DbSet<NCD> NCD { get; set; }
        public DbSet<Allergies_Details> Allergies_Detail { get; set; }
        public DbSet<NCD_Details> NCD_Detail { get; set; }
    }
}
