//using Microsoft.AspNetCore.Mvc;
//using System.Configuration;
using System.Net;
using System.Web.Http;
using WebApiBackend.Models;
using ConfigurationManager = System.Configuration.ConfigurationManager;
//using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WebApiBackend.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PatientsController : ApiController
    {
        private readonly PatientContext _context = new PatientContext(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        //private readonly PatientContext _db = new PatientContext(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        //private readonly IConfiguration _configuration;

        //public PatientsController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        //public PatientsController(PatientContext context)
        //{
        //    _context = new PatientContext(_configuration.GetConnectionString("DefaultConnection"));
        //}

        // GET: api/Patients

        // GET: api/patients
        public IEnumerable<Patient> GetPatients(string searchString = "")
        {
            var patients = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p => p.Name.Contains(searchString));
            }

            return patients.ToList();
        }

        // GET: api/patients/5
        public async Task<IHttpActionResult> GetPatient(int id)
        {
            Patient patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // POST: api/patients
        public async Task<IHttpActionResult> PostPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = patient.Id }, patient);
        }

        // PUT: api/patients/5
        public async Task<IHttpActionResult> PutPatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // DELETE: api/patients/5
        public async Task<IHttpActionResult> DeletePatient(int id)
        {
            Patient patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Count(e => e.Id == id) > 0;
        }

        // GET: api/patients?name={searchString} filter
        public IHttpActionResult GetPatientsByName(string name)
        {
            var filteredPatients = _context.Patients.Where(p => p.Name.Contains(name)).ToList();
            return Ok(filteredPatients);
        }

        // POST: api/patients add
        public IHttpActionResult PostPatients(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Patients.Add(patient);
            _context.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patient.Id }, patient);
        }

        // PUT: api/patients/5 edit
        public IHttpActionResult EditPatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        //[HttpGet]
        //public ActionResult<IEnumerable<Patient>> GetPatients()
        //{
        //    return _context.Patients.ToList();
        //}

        //// GET: api/Patients/5
        //[HttpGet("{id}")]
        //public ActionResult<Patient> GetPatient(int id)
        //{
        //    var patient = _context.Patients.Find(id);

        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    return patient;
        //}

        //// POST: api/Patients
        //[HttpPost]
        //public ActionResult<Patient> PostPatient(Patient patient)
        //{
        //    _context.Patients.Add(patient);
        //    _context.SaveChanges();

        //    return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        //}

        //// PUT: api/Patients/5
        //[HttpPut("{id}")]
        //public IActionResult PutPatient(int id, Patient patient)
        //{
        //    if (id != patient.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(patient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        if (!PatientExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/Patients/5
        //[HttpDelete("{id}")]
        //public IActionResult DeletePatient(int id)
        //{
        //    var patient = _context.Patients.Find(id);
        //    if (patient == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Patients.Remove(patient);
        //    _context.SaveChanges();

        //    return NoContent();
        //}

        //private bool PatientExists(int id)
        //{
        //    return _context.Patients.Any(e => e.Id == id);
        //}
    }
}
