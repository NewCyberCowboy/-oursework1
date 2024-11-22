using System.Collections.Generic;
using System.Linq;
using Coursework.Models;
using Coursework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework.Services
{
    public class PrescriptionService : IPrescriptionService
    {

        private readonly ApplicationDbContext _context;


        public PrescriptionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Prescription> GetAllCustomers()
        {
            return _context.Prescriptions.ToList();
        }

        public IEnumerable<Prescription> GetAll()
        {
            return _context.Prescriptions.ToList();
        }

        public void Add(Prescription Prescription)
        {
            _context.Prescriptions.Add(Prescription);
            _context.SaveChanges();
        }

        public void Update(Prescription Prescription)
        {
            if (Prescription == null)
            {
                throw new ArgumentNullException(nameof(Prescription), "Prescription cannot be null.");
            }

            var existingPrescription = GetById(Prescription.PrescriptionId);
            if (existingPrescription == null)
            {
                throw new KeyNotFoundException($"Patient with ID {Prescription.PrescriptionId} not found.");
            }

            existingPrescription.PrescriptionDuration = Prescription.PrescriptionDuration;
            existingPrescription.StaffId = Prescription.StaffId;
            existingPrescription.PatientId = Prescription.PatientId;
            existingPrescription.Quantity = Prescription.Quantity;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var Prescription = GetById(id);
            if (Prescription != null)
            {
                _context.Prescriptions.Remove(Prescription);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Prescription with ID {id} not found for deletion.");
            }
        }

        public Prescription GetById(int id)
        {
            return _context.Prescriptions.FirstOrDefault(c => c.PrescriptionId == id);
        }


    }
}
