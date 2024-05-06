using System.Data.Entity;
//using Microsoft.EntityFrameworkCore;

namespace WebApiBackend.Models
{
    public class PatientContext : DbContext
    {
        //public PatientContext(DbContextOptions<PatientContext> options) : base(options)
        //{
        //}
        public PatientContext(string connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
