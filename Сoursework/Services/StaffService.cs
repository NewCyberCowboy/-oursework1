using System.Collections.Generic;
using System.Linq;
using Coursework.Models;
using Coursework.Data;
using Microsoft.AspNetCore.Mvc;

namespace Coursework.Services
{
    public class StaffService : IStaffService
    {

        private readonly ApplicationDbContext _context;


        public StaffService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Staff> GetAllCustomers()
        {
            return _context.Staffs.ToList();
        }

        public IEnumerable<Staff> GetAll()
        {
            return _context.Staffs.ToList();
        }

        public void Add(Staff Staff)
        {
            _context.Staffs.Add(Staff);
            _context.SaveChanges();
        }

        public void Update(Staff Staff)
        {
            if (Staff == null)
            {
                throw new ArgumentNullException(nameof(Staff), "Staff cannot be null.");
            }

            var existingStaff = GetById(Staff.StaffId);
            if (existingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with ID {Staff.StaffId} not found.");
            }

            existingStaff.Name = Staff.Name;
            existingStaff.Position = Staff.Position;


            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Staff = GetById(id);
            if (Staff != null)
            {
                _context.Staffs.Remove(Staff);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Staff with ID {id} not found for deletion.");
            }
        }

        public Staff GetById(int id)
        {
            return _context.Staffs.FirstOrDefault(c => c.StaffId == id);
        }


    }
}
