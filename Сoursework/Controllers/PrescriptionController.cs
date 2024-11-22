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
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _PrescriptionService;


        public PrescriptionController(IPrescriptionService PrescriptionService)
        {
            _PrescriptionService = PrescriptionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Prescription>> GetAll()
        {
            var Prescription = _PrescriptionService.GetAll();
            return Ok(Prescription);
        }

        [HttpGet("{id}")]
        public ActionResult<Prescription> GetById(int id)
        {
            var Prescription = _PrescriptionService.GetById(id);
            if (Prescription == null)
            {
                return NotFound($"Prescriptions with ID {id} not found.");
            }
            return Ok(Prescription);
        }

        [HttpPost]
        public ActionResult Add(Prescription Prescription)
        {
            _PrescriptionService.Add(Prescription);
            return CreatedAtAction(nameof(GetAll), new { id = Prescription.PrescriptionId }, Prescription);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePrescription(int id, [FromBody] PrescriptionUpdateDto PrescriptionUpdateDto)
        {
            if (PrescriptionUpdateDto == null)
            {
                return BadRequest("Prescription update data is required.");
            }

            var Prescription = _PrescriptionService.GetById(id);
            if (Prescription == null)
            {
                return NotFound($"Prescription with ID {id} not found.");
            }

            if (!string.IsNullOrEmpty(PrescriptionUpdateDto.PrescriptionDuration))
            {
                Prescription.PrescriptionDuration = PrescriptionUpdateDto.PrescriptionDuration;
            }

            if (PrescriptionUpdateDto.Quantity.HasValue)
            {
                Prescription.Quantity = PrescriptionUpdateDto.Quantity.Value;
            }

            if (PrescriptionUpdateDto.StaffId > 0) // Assuming StaffId should be a positive integer
            {
                Prescription.StaffId = PrescriptionUpdateDto.StaffId;
            }

            if (PrescriptionUpdateDto.PatientId > 0) // Assuming PatientId should be a positive integer
            {
                Prescription.PatientId = PrescriptionUpdateDto.PatientId;
            }

            _PrescriptionService.Update(Prescription); // Delegate the update to the service

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingPrescription = _PrescriptionService.GetById(id);
            if (existingPrescription == null)
            {
                return NotFound();
            }

            _PrescriptionService.Delete(id);
            return NoContent();
        }
    }
}

