using Microsoft.AspNetCore.Mvc;
using Coursework.Models;
using System.Collections.Generic;
using Coursework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;



namespace Coursework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<Account> _userManager; 
        private readonly SignInManager<Account> _signInManager; 

        public AccountController(IAccountService accountService, UserManager<Account> userManager, SignInManager<Account> signInManager)
        {
            _accountService = accountService;
            _userManager = userManager; 
            _signInManager = signInManager; 
        }

        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAll()
        {
            var accounts = _accountService.GetAll();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public ActionResult<Account> GetById(int id)
        {
            var account = _accountService.GetById(id);
            if (account == null)
            {
                return NotFound($"Account with ID {id} not found.");
            }
            return Ok(account);
        }

        [HttpPost]
        public ActionResult Add(Account account)
        {
            _accountService.Add(account);
            return CreatedAtAction(nameof(GetAll), new { id = account.Id }, account);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateAccount(int id, [FromBody] AccountUpdateDto accountUpdateDto)
        {
            if (accountUpdateDto == null)
            {
                return BadRequest("Account update data is required.");
            }

            var account = _accountService.GetById(id);
            if (account == null)
            {
                return NotFound($"Account with ID {id} not found.");
            }

            if (!string.IsNullOrEmpty(accountUpdateDto.Name))
            {
                account.Name = accountUpdateDto.Name;
            }
            if (!string.IsNullOrEmpty(accountUpdateDto.Password))
            {
                account.Password = accountUpdateDto.Password;
            }
            if (!string.IsNullOrEmpty(accountUpdateDto.Email))
            {
                account.Email = accountUpdateDto.Email;
            }
            if (!string.IsNullOrEmpty(accountUpdateDto.ConfirmPassword))
            {
                account.ConfirmPassword = accountUpdateDto.ConfirmPassword;
            }

            _accountService.Update(account);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingAccount = _accountService.GetById(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            _accountService.Delete(id);
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(Account model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Пароли не совпадают.");
                return BadRequest(ModelState);
            }

            var account = new Account
            {
                Email = model.Email,
                Name = model.Name
            };

            var result = await _userManager.CreateAsync(account, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(account, isPersistent: false);
                return CreatedAtAction(nameof(GetAll), new { id = account.Id }, account);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(Account model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                return Ok();
            }

            return Unauthorized("Неверный логин или пароль.");
        }

        [HttpPost("logout")] 
        public async Task<ActionResult> Logout()
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("Пользователь не аутентифицирован.");
            }

            
            await _signInManager.SignOutAsync();
            return Ok("Вы вышли из системы.");
        }
    }
}