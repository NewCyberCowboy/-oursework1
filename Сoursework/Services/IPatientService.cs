using System.Collections.Generic;
using Coursework.Models;

namespace Coursework.Services
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetAll();
        void Add(Patient patient);
        void Update(Patient Patient);
        void Delete(int id);
        Patient GetById(int id);
    }
}
