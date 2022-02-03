using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeerepository;

        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeerepository = employeeRepository;

        }

        [HttpPost("{Register}")]
        public ActionResult<RegisterVM> Post(RegisterVM registerVM)
        {
            var result = employeerepository.Register(registerVM);
            if (result != 0)
            {
                if (result == 4)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email sudah digunakan!" });
                }
                else if (result == 5)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Nomor Telepon sudah digunakan!" });
                }
                else if (result == 6)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email dan Nomor Telepon sudah digunakan!" });
                }
                else 
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil ditambahkan" });
                }
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Gagal ditambahkan" });
            }
        }

        [Authorize(Roles = "Director,Manager")]
        [HttpGet("RegisteredData")]

        public ActionResult<RegisterVM> GetRegisteredData()
        {
            var result = employeerepository.GetRegisteredData();

            return StatusCode(200, new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditampilkan" });

        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            return Ok("Test CORS Berhasil");
        }
    }
}
