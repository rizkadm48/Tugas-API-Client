using API.Context;
using API.Models;
using API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OldEmployeesController : ControllerBase
    {
        private EmployeeRepository employeesRepository;
        public OldEmployeesController(EmployeeRepository employeesRepository) // constructor
        {
            this.employeesRepository = employeesRepository;
        }

        [HttpPost]
        public ActionResult Post(Employee employee) //create
        {
            var result = employeesRepository.Insert(employee);
            if (result != 0)
            {
                if (result == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Email sudah digunakan!" });
                }
                else if (result == 3)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Nomor Telepon sudah digunakan!" });
                }
                else if (result == 4)
                {
                    return StatusCode(400,new { status= HttpStatusCode.BadRequest, result,message= "Email dan Nomor Telepon sudah digunakan!"});
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
        [HttpGet]
        public ActionResult Get() //read
        {

            var result = employeesRepository.Get();
            if (result.Count() > 0)
            {
                return StatusCode(200, new {status = HttpStatusCode.OK, result, message="Data Berhasil Ditampilkan" });
            }
            else
            {
                return StatusCode(404,new { status = HttpStatusCode.NotFound, result, message = "Data Tidak Ditemukan" });  //ini kalau datanya harus kosong
            }
        }

        [HttpGet("{Nik}")]
        //[Route("getnik")]
        public ActionResult Get(Employee employee) //read
        {
            var result = employeesRepository.Get(employee.Nik);
            if (result != null) 
            {
                return StatusCode(200,new { status = HttpStatusCode.OK, result, message = "Data Berhasil Ditampilkan" });
            }
            else
            {
                return StatusCode(404,new { status = HttpStatusCode.NotFound, result, message = "NIK Tidak Ditemukan" }); 
            }
        }

        [HttpPut]
        public ActionResult Put(Employee employee) //update
        {
            var result = employeesRepository.Update(employee);
            if (result != 0)
            {
                return StatusCode(200,new { status = HttpStatusCode.OK, result, message = "Data Berhasil Diubah" });
            }
            else
            {
                return StatusCode(404,new { status = HttpStatusCode.NotFound, result, message = "NIK Tidak Ditemukan" }); //belum
            }

        }

        [HttpDelete]
        //[HttpDelete]
        public ActionResult Delete(Employee employee) //hapus
        {

            var result = employeesRepository.Delete(employee.Nik);
            if (result != 0)
            {
                return StatusCode(200,new { status = HttpStatusCode.OK, result, message = "Data Berhasil Dihapus" });
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.NotFound, result, message = "NIK Tidak Ditemukan" });
            }
        }


    }


}
