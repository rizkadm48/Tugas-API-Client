using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
        private readonly MyContext context;
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
    }
}
