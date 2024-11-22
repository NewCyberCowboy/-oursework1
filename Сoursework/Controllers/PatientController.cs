using Microsoft.AspNetCore.Mvc;
using Coursework.Models;
using System.Collections.Generic;
using Coursework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _PatientService;


        public PatientController(IPatientService PatientService)
        {
            _PatientService = PatientService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetAll()
        {
            var Patient = _PatientService.GetAll();
            return Ok(Patient);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetById(int id)
        {
            var patient = _PatientService.GetById(id);
            if (patient == null)
            {
                return NotFound($"Patients with ID {id} not found.");
            }
            return Ok(patient);
        }

        [HttpPost]
        public ActionResult Add(Patient patient)
        {
            _PatientService.Add(patient);
            return CreatedAtAction(nameof(GetById), new { id = patient.PatientId }, patient);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatient(int id, [FromBody] PatientUpdateDto PatientUpdateDto)
        {
            if (PatientUpdateDto == null)
            {
                return BadRequest("Patient update data is required.");
            }

            var Patient = _PatientService.GetById(id);
            if (Patient == null)
            {
                return NotFound($"Patient with ID {id} not found.");
            }

            
            if (!string.IsNullOrEmpty(PatientUpdateDto.Name))
            {
                Patient.Name = PatientUpdateDto.Name;
            }
            if (!string.IsNullOrEmpty(PatientUpdateDto.Disease))
            {
                Patient.Disease = PatientUpdateDto.Disease;
            }


            _PatientService.Update(Patient); // Delegate the update to the service

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingPatient = _PatientService.GetById(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            _PatientService.Delete(id);
            return NoContent();
        }
    }
}
