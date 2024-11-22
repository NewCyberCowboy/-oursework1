using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using Coursework.Data;
using Coursework.Models;
using Coursework.Services;

namespace Coursework.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Account> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountService(UserManager<Account> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public void Add(Account account)
        {
            var result = _userManager.CreateAsync(account, account.Password).Result;
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create account.");
            }
        }

        public List<Account> GetAllCustomers()
        {
            return _context.Accounts.ToList();
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public void Update(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account), "Accounts cannot be null.");
            }

            var existingAccount = GetById(account.Id);
            if (existingAccount == null)
            {
                throw new KeyNotFoundException($"Accounts with ID {account.Id} not found.");
            }

            existingAccount.Name = account.Name;
            existingAccount.Password = account.Password;
            existingAccount.Email = account.Email;
            existingAccount.ConfirmPassword = account.ConfirmPassword;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var account = GetById(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Account with ID {id} not found for deletion.");
            }
        }

        public Account GetById(int id)
        {
            return _context.Accounts.FirstOrDefault(c => c.Id == id);
        }

        // Реализация метода Login
        public void Login(string email, string password)
        {
            // Логика аутентификации
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                throw new Exception("User  not found.");
            }

            var result = _userManager.CheckPasswordAsync(user, password).Result;
            if (!result)
            {
                throw new Exception("Invalid password.");
            }

            // Здесь можно добавить логику для успешного входа
        }
    }
}