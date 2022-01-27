using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace API.Repository.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int Login(LoginVM loginVM)
        {
            var EmailPhone = context.Employees.Where(e => e.Email == loginVM.EmailPhone || e.Phone == loginVM.EmailPhone).FirstOrDefault();
            if (EmailPhone != null)
            {
                var Password = context.Accounts.Where(e => e.Nik == EmailPhone.Nik).FirstOrDefault();

                if (BCrypt.Net.BCrypt.Verify(loginVM.Password, Password.Password))
                {
                    return 1;
                }
                else
                {
                    return 5; //password tidak ada
                }
            }
            else
            {
                return 6; //akun tidak ada
            }

        }

        public IEnumerable GetProfile(string EmailPhone)
        {
            var employees = context.Employees;
            var accounts = context.Accounts;
            var profilings = context.Profilings;
            var educations = context.Educations;
            var universities = context.Universitys;

            var result = (from emp in employees
                          join acc in accounts on emp.Nik equals acc.Nik
                          join pro in profilings on acc.Nik equals pro.Nik
                          join edu in educations on pro.Education_Id equals edu.Id
                          join univ in universities on edu.University_Id equals univ.Id
                          where (emp.Email == EmailPhone || emp.Phone == EmailPhone)
                          select new 
                          {
                              Nik = emp.Nik,
                              FullName = emp.FirstName + " " + emp.LastName,
                              Phone = emp.Phone,
                              BirthDate = emp.BirthDate,
                              Salary = emp.Salary,
                              Email = emp.Email,
                              Degree = edu.Degree,
                              GPA = edu.GPA,
                              UnivName = univ.Name
                          }).ToList();

            return result;
        }

        public int ForgotPassword(LoginVM loginVM)
        {
            var CekEmail = context.Employees.Where(e => e.Email == loginVM.EmailPhone).FirstOrDefault();
            if(CekEmail != null) {
                var CekAkun = context.Accounts.Where(e => e.Nik == CekEmail.Nik).FirstOrDefault();
                Random random = new Random();
                var Otp = random.Next(0, 1000000).ToString("D6");
                CekAkun.OTP = int.Parse(Otp);

                var time  = DateTime.Now.AddMinutes(5);
                CekAkun.ExpiredToken = time;
                CekAkun.isUsed = false;

                context.Entry(CekAkun).State = EntityState.Modified;
                context.SaveChanges();

                var fromAddress = "rdm4898@gmail.com";
                var toAddress = loginVM.EmailPhone;
                var fromPassword = "Rizkadm4898";
                var subject = "OTP";
                var body = "Your OTP is " + Otp;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(fromAddress, fromPassword),
                    Timeout = 20000
                };
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
                smtp.Send(message);
                return 1;
            }
            else
            {
                return 5;
            }
        }

        public int ChangePassword(ChangeVM changeVM)
        {
            var CekEmail = context.Employees.Where(e => e.Email == changeVM.Email).FirstOrDefault();
            if(CekEmail != null)
            {
                var CekAkun = context.Accounts.Where(e => e.Nik == CekEmail.Nik).FirstOrDefault();
                CekAkun.isUsed = true;
                

            }
            else
            {
                return 3;
            }
            
            return 1;
        }
    }
}
