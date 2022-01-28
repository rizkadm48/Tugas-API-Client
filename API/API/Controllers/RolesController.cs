using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseController<Role, RoleRepository, int>
    {
        private readonly RoleRepository rolerepository;
        public RolesController(RoleRepository Rolerepository) : base(Rolerepository)
        {
            this.rolerepository = Rolerepository;
        }
    }
}
