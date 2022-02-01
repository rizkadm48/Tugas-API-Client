using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository accountrolerepository;
        private readonly MyContext context;
        public AccountRolesController(AccountRoleRepository AccountRolerepository, MyContext context) : base(AccountRolerepository)
        {
            this.accountrolerepository = AccountRolerepository;
            this.context = context;
        }

        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]
        public ActionResult<SignVM> SignManager(SignVM signVM)
        {
            var result = accountrolerepository.SignManager(signVM);
            return Ok(new { status = HttpStatusCode.OK, result, message = "Berhasil menjadi Manager" });
          
        }

    }
}
