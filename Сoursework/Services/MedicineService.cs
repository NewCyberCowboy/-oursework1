using System.Collections.Generic;
using System.Linq;
using Coursework.Models;
using Coursework.Data;
using Microsoft.AspNetCore.Mvc;


namespace Coursework.Services
{
    public class MedicineService : IMedicineService
    {


        private readonly ApplicationDbContext _context;


        public MedicineService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Medicine> GetAllCustomers()
        {
            return _context.Medicines.ToList();
        }

        public IEnumerable<Medicine> GetAll()
        {
            return _context.Medicines.ToList();
        }

        public void Add(Medicine Medicine)
        {
            _context.Medicines.Add(Medicine);
            _context.SaveChanges();
        }

        public void Update(Medicine Medicine)
        {
            if (Medicine == null)
            {
                throw new ArgumentNullException(nameof(Medicine), "Medicine cannot be null.");
            }

            var existingMedicine = GetById(Medicine.MedicineId);
            if (existingMedicine == null)
            {
                throw new KeyNotFoundException($"Patient with ID {Medicine.MedicineId} not found.");
            }

            existingMedicine.Name = Medicine.Name;
            existingMedicine.UsageMethod = Medicine.UsageMethod;
            existingMedicine.Dosage = Medicine.Dosage;
            existingMedicine.Quantity = Medicine.Quantity;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Medicine = GetById(id);
            if (Medicine != null)
            {
                _context.Medicines.Remove(Medicine);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Medicine with ID {id} not found for deletion.");
            }
        }

        public Medicine GetById(int id)
        {
            return _context.Medicines.FirstOrDefault(c => c.MedicineId == id);
        }


    }

}

