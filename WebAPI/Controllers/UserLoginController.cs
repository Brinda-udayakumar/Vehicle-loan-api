using WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly AppDBContext _context;

        public UserLoginController(AppDBContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("UserLogin")]
        public async Task<ActionResult<UserLogin>> Post(UserLogin _UserLogin)
        {
            if (_UserLogin != null && _UserLogin.EmailId != null && _UserLogin.Password != null)
            {
                UserLogin UserLogin = await GetAccount(_UserLogin.EmailId, _UserLogin.Password);

                if (UserLogin != null)
                {
                    
                    return new JsonResult(UserLogin);
                }
                else
                {
                    return new JsonResult("Invalid credentials");
                }
            }
            else
            {
                return new JsonResult("Not Working");
            }
        }

        private async Task<UserLogin> GetAccount(string MailID, string password)
        {
            return await _context.UserLogin.FirstOrDefaultAsync(u => u.EmailId == MailID && u.Password == password);
        }
    }
}
