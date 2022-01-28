using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly AccountRoleRepository accountrolerepository;
        public AccountRolesController(AccountRoleRepository AccountRolerepository) : base(AccountRolerepository)
        {
            this.accountrolerepository = AccountRolerepository;
        }
    }
}
