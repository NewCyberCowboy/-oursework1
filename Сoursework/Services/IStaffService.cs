using System.Collections.Generic;
using Coursework.Models;

namespace Coursework.Services
{
    public interface IStaffService
    {
        IEnumerable<Staff> GetAll();
        void Add(Staff Staff);
        void Update(Staff Staff);
        void Delete(int id);
        Staff GetById(int id);
    }
}
