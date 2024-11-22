using System.Collections.Generic;
using Coursework.Models;

namespace Coursework.Services
{
    public interface IPrescriptionService
    {
        IEnumerable<Prescription> GetAll();
        void Add(Prescription Prescription);
        void Update(Prescription Prescription);
        void Delete(int id);
        Prescription GetById(int id);
    }
}
