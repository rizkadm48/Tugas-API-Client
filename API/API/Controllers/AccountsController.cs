using API.Base;
using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountrepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration, MyContext context) : base(accountRepository)
        {
            this.accountrepository = accountRepository;
            this._configuration = configuration;
            this.context = context;
        }

        [HttpPost("{login}")]
        public ActionResult<LoginVM> Post(LoginVM loginVM)
        {
            var result = accountrepository.Login(loginVM);
            if (result != 0)
            {
                if (result == 5)
                {
                    return StatusCode(400, new { status = HttpStatusCode.NotFound, result, message = "Password salah!" });
                }
                else if (result == 6)
                {
                    return StatusCode(400, new { status = HttpStatusCode.NotFound, result, message = "Akun tidak ditemukan!" });
                }
                else
                {

                    var getUserData = context.Employees.Where(e => e.Email == loginVM.EmailPhone).FirstOrDefault();
                    var getRole = context.Roles.Where(r => r.AccountRoles.Any(ar => ar.Account.Nik == getUserData.Nik)).ToList();

                    var claims = new List<Claim>
                    {
                        new Claim("Email", loginVM.EmailPhone), //payload
                    };

                    foreach (var item in getRole)
                    {
                        claims.Add(new Claim("roles", item.Name));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Header
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10), //Set expired times
                        signingCredentials: signIn);
                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token); //Generate token
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));

                    return StatusCode(200, new { status = HttpStatusCode.OK, idtoken, message = "Berhasil login" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Gagal login" });
            }
        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }


        [Route("ForgotPassword")]
        [HttpPost]
        public ActionResult ForgotPassword(LoginVM loginVM)
        {
            var result = accountrepository.ForgotPassword(loginVM);
            if(result > 0)
            {
                if(result == 5)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Akun tidak ditemukan" });
                }
                else
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "OTP berhasil dikirim via email" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Gagal forgot password" });
            }
        }

        [Route("ChangesPassword")]
        [HttpPost]
        public ActionResult ChangesPassword(ChangeVM changeVM)
        {
            var result = accountrepository.ChangesPassword(changeVM);
            if (result > 0)
            {
                if (result == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "OTP sudah expired" });
                }
                else if (result == 3)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email tidak ditemukan" });
                }
                else if (result == 4)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "OTP sudah pernah digunakan" });
                }
                else if (result == 5)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "OTP tidak terdaftar" });
                }
                else if (result == 6)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Password harus sama" });
                }
                else 
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Change password berhasil" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Gagal forgot password" });
            }
        }

    }
}
