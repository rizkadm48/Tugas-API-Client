using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext context;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int SignManager(SignVM signVM)
        {
            var NikEmployee = context.Employees.Where(e => e.Nik == signVM.Nik).FirstOrDefault();

            AccountRole ar = new AccountRole();
            ar.Account_Nik = NikEmployee.Nik;
            ar.Role_Id = 4; //4 karena manager, 5 itu director, 3 itu employee

            context.AccountRoles.Add(ar);
            return context.SaveChanges();

        }

    }
}
