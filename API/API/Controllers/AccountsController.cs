using API.Base;
using API.Context;
using API.Models;
using API.Repository;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountrepository;

        public AccountsController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountrepository = accountRepository;
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
                    //var GetProfile = accountrepository.GetProfile(loginVM.EmailPhone);
                    //return Ok(GetProfile);
                    //return RedirectToAction("GetProfile", "Accounts");
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Berhasil login" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Gagal login" });
            }
        }

        /*[Route("GetProfile")]
        [HttpGet]

        public ActionResult<LoginVM> GetProfile(LoginVM loginVM)
        {
            var result = accountrepository.GetProfile(loginVM.EmailPhone);
            //if(LoginVM.ReferenceEquals(result, true) == ProfileVM.ReferenceEquals(result, true)) { }
                return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditampilkan" });
            
        }*/

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
