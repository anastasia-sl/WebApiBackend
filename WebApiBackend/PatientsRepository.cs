using WebApiBackend.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebApiBackend
{
    public class PatientsRepository
    {
        private readonly PatientContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PatientsRepository(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _baseUrl = configuration["MyApiSettings:BaseUrl"];
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync(string searchString = "")
        {
            IQueryable<Patient> patientsQuery = _context.Patients;

            if (!string.IsNullOrEmpty(searchString))
            {
                patientsQuery = patientsQuery.Where(p => p.Name.Contains(searchString));
            }

            return await patientsQuery.ToListAsync();
        }
    }
}
