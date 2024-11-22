using System.Collections.Generic;
using Coursework.Models;

namespace Coursework.Services
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAll();
        void Add(Account account);
        void Update(Account account);
        void Delete(int id);
        void Login(string email, string password);
        Account GetById(int id);
    }
}
