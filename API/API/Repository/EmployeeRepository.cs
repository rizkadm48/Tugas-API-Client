using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class EmployeeRepository : IEmployeeRepository // ini implementasi
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext context) // constructor
        {
            this.context = context;
        }
        public int Delete(string Nik) // ini kebawah auto kalau udah implementasi
        {
            var entity = context.Employees.Find(Nik); //find berarti mencari Nik yang akan dihapus
            if(entity == null)
            {
                return 0;
            }

            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string Nik)
        {
            var result = context.Employees.Find(Nik);
            return result;
        }

        //ini insert
        public int Insert(Employee employee)
        {
            var EmailExist = IsEmailExist(employee);    //ini dari void dibawah
            var PhoneExist = IsPhoneExist(employee);    //ini juga
            if (EmailExist == false)    //cek email gak ada
            {
                if (PhoneExist == false) //cek nomor hp gak ada
                {
                    var NIK = GetLastNIK() + 1; //ngambil nik terakhir, ini dijalankan
                    var Year = DateTime.Now.Year;
                    employee.Nik = Year + "00" + NIK.ToString();

                    context.Employees.Add(employee);
                    var result = context.SaveChanges();
                    return result;
                }
                else
                {
                    return 3; //nomor telepon sudah ada
                }
            }
            else if (EmailExist == true && PhoneExist == true) 
            {
                return 4; //email dan nomor telepon sudah ada
            }
            else
            {
                return 2; //email sudah ada
            }
        }

        public bool IsEmailExist(Employee employee)
        {
            var CekEmail = context.Employees.Where(emp => emp.Email == employee.Email).FirstOrDefault(); //0 atau 1 diambil pertama
            if (CekEmail != null)
            {
                return true; //kalau menemukan email sama
            }
            else
            {
                return false; //tidak menemukan email sama
            }
        }
        public bool IsPhoneExist(Employee employee)
        {
            var CekPhone = context.Employees.Where(emp => emp.Phone == employee.Phone).FirstOrDefault(); //null atau 1 diambil pertama
            if (CekPhone != null)
            {
                return true; //kalau menemukan nomor hp sama
            }
            else
            {
                return false; //kalau menemukan email sama
            }
        }
        public int GetLastNIK()
        {
            var lastEmp = context.Employees.OrderByDescending(emp => emp.Nik).FirstOrDefault(); //diurutkan dari nik terakhir
            if (lastEmp == null)
            {
                return 0; //tidak ditemukan atau belum ada nik
            }
            else
            {
                var lastNIK = lastEmp.Nik.Remove(0, 5); //kalau ada nik yang terakhir 
                return int.Parse(lastNIK);
            }
        }

        //ini update
        public int Update(Employee employee)
        {
            var entity = context.Employees.Find(employee.Nik); //find berarti mencari Nik yang akan diedit
            if (entity == null)
            {
                return 0; //kalau nik nya gak ada
            }
            else {
                context.Remove(entity); //dihapus dulu
                context.Entry(employee).State = EntityState.Modified;
                var result = context.SaveChanges();
                return result;
            }
        }
    }

}
