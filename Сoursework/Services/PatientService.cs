using System.Collections.Generic;
using System.Linq;
using Coursework.Models;
using Coursework.Data;
using Microsoft.AspNetCore.Mvc;


namespace Coursework.Services
{
    public class PatientService : IPatientService
    {
        
        
            private readonly ApplicationDbContext _context;


            public PatientService(ApplicationDbContext context)
            {
                _context = context;
            }
            public List<Patient> GetAllCustomers()
            {
                return _context.Patients.ToList();
            }

            public IEnumerable<Patient> GetAll()
            {
                return _context.Patients.ToList();
            }

            public void Add(Patient Patient)
            {
                _context.Patients.Add(Patient);
                _context.SaveChanges();
            }

            public void Update(Patient Patient)
            {
                if (Patient == null)
                {
                    throw new ArgumentNullException(nameof(Patient), "Patients cannot be null.");
                }

                var existingPatient = GetById(Patient.PatientId);
                if (existingPatient == null)
                {
                    throw new KeyNotFoundException($"Patients with ID {Patient.PatientId} not found.");
                }

                existingPatient.Name = Patient.Name;
                existingPatient.Disease = Patient.Disease;

                _context.SaveChanges();
            }

            public void Delete(int id)
            {
                var Patient = GetById(id);
                if (Patient != null)
                {
                    _context.Patients.Remove(Patient);
                    _context.SaveChanges();
                }
                else
                {
                    throw new KeyNotFoundException($"Patient with ID {id} not found for deletion.");
                }
            }

            public Patient GetById(int id)
            {
                return _context.Patients.FirstOrDefault(c => c.PatientId == id);
            }
        

    }

}

