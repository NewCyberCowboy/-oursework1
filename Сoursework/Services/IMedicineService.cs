using System.Collections.Generic;
using Coursework.Models;

namespace Coursework.Services
{
    public interface IMedicineService
    {
        IEnumerable<Medicine> GetAll();
        void Add(Medicine Medicine);
        void Update(Medicine Medicine);
        void Delete(int id);
        Medicine GetById(int id);
    }
}
