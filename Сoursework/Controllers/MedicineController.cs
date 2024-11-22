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
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _MedicineService;


        public MedicineController(IMedicineService MedicineService)
        {
            _MedicineService = MedicineService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Medicine>> GetAll()
        {
            var Medicine = _MedicineService.GetAll();
            return Ok(Medicine);
        }

        [HttpGet("{id}")]
        public ActionResult<Medicine> GetById(int id)
        {
            var Medicine = _MedicineService.GetById(id);
            if (Medicine == null)
            {
                return NotFound($"Medicines with ID {id} not found.");
            }
            return Ok(Medicine);
        }

        [HttpPost]
        public ActionResult Add(Medicine Medicine)
        {
            _MedicineService.Add(Medicine);
            return CreatedAtAction(nameof(GetAll), new { id = Medicine.MedicineId }, Medicine);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateMedicine(int id, [FromBody] MedicineUpdateDto MedicineUpdateDto)
        {
            if (MedicineUpdateDto == null)
            {
                return BadRequest("Medicine update data is required.");
            }

            var Medicine = _MedicineService.GetById(id);
            if (Medicine == null)
            {
                return NotFound($"Medicine with ID {id} not found.");
            }


            if (!string.IsNullOrEmpty(MedicineUpdateDto.Name))
            {
                Medicine.Name = MedicineUpdateDto.Name;
            }
            if (!string.IsNullOrEmpty(MedicineUpdateDto.UsageMethod))
            {
                Medicine.UsageMethod = MedicineUpdateDto.UsageMethod;
            }
            if (!string.IsNullOrEmpty(MedicineUpdateDto.Dosage))
            {
                Medicine.Dosage = MedicineUpdateDto.Dosage;
            }
            // Проверяем и присваиваем значение Quantity
            if (MedicineUpdateDto.Quantity.HasValue)
            {
                Medicine.Quantity = MedicineUpdateDto.Quantity.Value;
            }

            _MedicineService.Update(Medicine); // Delegate the update to the service

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingMedicine = _MedicineService.GetById(id);
            if (existingMedicine == null)
            {
                return NotFound();
            }

            _MedicineService.Delete(id);
            return NoContent();
        }
    }
}

