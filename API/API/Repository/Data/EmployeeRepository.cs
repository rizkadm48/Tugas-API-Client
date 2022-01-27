using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }
        public int Register(RegisterVM registerVM)
        {
            var EmailExist = IsEmailExist(registerVM);    //ini dari void dibawah
            var PhoneExist = IsPhoneExist(registerVM);    //ini juga
            if (EmailExist == false)    //cek email gak ada
            {
                if (PhoneExist == false) //cek nomor hp gak ada
                {
                    Employee emp = new Employee();

                    var NIK = GetLastNIK() + 1; //ngambil nik terakhir, ini dijalankan
                    var Year = DateTime.Now.Year;
                    emp.Nik = Year + "00" + NIK.ToString();

                    emp.FirstName = registerVM.FirstName;
                    emp.LastName = registerVM.LastName;
                    emp.Phone = registerVM.Phone;
                    emp.BirthDate = registerVM.BirthDate;
                    emp.Salary = registerVM.Salary;
                    emp.Email = registerVM.Email;
                    emp.Gender = (Models.Gender)registerVM.Gender;
                    
                    context.Employees.Add(emp);
                    context.SaveChanges();

                    Account acc = new Account();
                    acc.Nik = emp.Nik;
                    //acc.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                    acc.Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password);
                    context.Accounts.Add(acc);
                    context.SaveChanges();

                    Education ed = new Education();

                    ed.Degree = registerVM.Degree;
                    ed.GPA = registerVM.GPA;
                    ed.University_Id = registerVM.University_Id;
                    context.Educations.Add(ed);
                    context.SaveChanges();

                    Profiling pro = new Profiling();
                    pro.Nik = acc.Nik;
                    pro.Education_Id = ed.Id;
                    context.Profilings.Add(pro);
                    context.SaveChanges();
                    
                    return 1;
                }
                else
                {
                    return 5; //nomor telepon sudah ada
                }
            }
            else if (EmailExist == true && PhoneExist == true)
            {
                return 6; //email dan nomor telepon sudah ada
            }
            else
            {
                return 4; //email sudah ada
            }
            /*var result = context.SaveChanges();
            return result;*/
        }

        public bool IsEmailExist(RegisterVM registerVM)
        {
            var CekEmail = context.Employees.Where(emp => emp.Email == registerVM.Email).FirstOrDefault(); //0 atau 1 diambil pertama
            if (CekEmail != null)
            {
                return true; //kalau menemukan email sama
            }
            else
            {
                return false; //tidak menemukan email sama
            }
        }
        public bool IsPhoneExist(RegisterVM registerVM)
        {
            var CekPhone = context.Employees.Where(emp => emp.Phone == registerVM.Phone).FirstOrDefault(); //null atau 1 diambil pertama
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

        public IEnumerable<UserDataVM> GetRegisteredData()
        {
            var result = context.Employees.
                           Include(a => a.Account).
                           ThenInclude(p => p.Profiling).
                          ThenInclude(e => e.Education).
                           ThenInclude(u => u.University).

                          Select(x => new UserDataVM
                          {
                              FullName = x.FirstName + " " + x.LastName,
                              Phone = x.Phone,
                              BirthDate = x.BirthDate,
                              Salary = x.Salary,
                              Email = x.Email,
                              Degree = x.Account.Profiling.Education.Degree,
                              GPA = x.Account.Profiling.Education.GPA,
                              University_Name = x.Account.Profiling.Education.University.Name
                          }).ToList(); 

            return result;
        }
    }
}
