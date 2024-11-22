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
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _StaffService;


        public StaffController(IStaffService StaffService)
        {
            _StaffService = StaffService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Staff>> GetAll()
        {
            var Staff = _StaffService.GetAll();
            return Ok(Staff);
        }

        [HttpGet("{id}")]
        public ActionResult<Staff> GetById(int id)
        {
            var order = _StaffService.GetById(id);
            if (order == null)
            {
                return NotFound($"Staffs with ID {id} not found.");
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult Add(Staff Staff)
        {
            _StaffService.Add(Staff);
            return CreatedAtAction(nameof(GetAll), new { id = Staff.StaffId }, Staff);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateStaff(int id, [FromBody] StaffUpdateDto StaffUpdateDto)
        {
            if (StaffUpdateDto == null)
            {
                return BadRequest("Staff update data is required.");
            }

            var Staff = _StaffService.GetById(id);
            if (Staff == null)
            {
                return NotFound($"Staff with ID {id} not found.");
            }


            if (!string.IsNullOrEmpty(StaffUpdateDto.Name))
            {
                Staff.Name = StaffUpdateDto.Name;
            }
            if (!string.IsNullOrEmpty(StaffUpdateDto.Position))
            {
                Staff.Position = StaffUpdateDto.Position;
            }


            _StaffService.Update(Staff); // Delegate the update to the service

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingStaff = _StaffService.GetById(id);
            if (existingStaff == null)
            {
                return NotFound();
            }

            _StaffService.Delete(id);
            return NoContent();
        }
    }
}

