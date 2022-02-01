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

        public int ForgotPassword(LoginVM loginVM)
        {
            var CekEmail = context.Employees.Where(e => e.Email == loginVM.EmailPhone).FirstOrDefault();
            if(CekEmail != null) {
                var CekAkun = context.Accounts.Where(e => e.Nik == CekEmail.Nik).FirstOrDefault();
                Random random = new Random();
                var Otp = random.Next(0, 1000000).ToString("D6"); //ini string random
                CekAkun.OTP = int.Parse(Otp); //biar jadi int

                var Time = DateTime.Now.AddMinutes(5);
                CekAkun.ExpiredToken = Time; //cek expired dr waktu
                CekAkun.isUsed = false; //belum dipake jadi false, nanti dichange jadi true

                context.Entry(CekAkun).State = EntityState.Modified; //buat update data di account nya
                context.SaveChanges();

                var FromAddress = "rdm4898@gmail.com";
                var ToAddress = loginVM.EmailPhone;
                var FromPassword = "Rizkadm4898";
                var SubjectTime = DateTime.Now;
                var Subject = "OTP "  + SubjectTime;
                var Body = "Your OTP is " + Otp;

                var Smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(FromAddress, FromPassword),
                    Timeout = 20000
                };
                MailMessage Message = new MailMessage(FromAddress, ToAddress, Subject, Body);
                Smtp.Send(Message);
                return 1;
            }
            else
            {
                return 5;
            }
        }

        public int ChangesPassword(ChangeVM changeVM)
        {
            var CekEmail = context.Employees.Where(e => e.Email == changeVM.Email).FirstOrDefault();
            if(CekEmail != null)
            {
                var CekAkun = context.Accounts.Where(e => e.Nik == CekEmail.Nik).FirstOrDefault();
                var CekOTP = context.Accounts.Where(e => e.OTP == CekAkun.OTP).FirstOrDefault();
                if (changeVM.OTP == CekOTP.OTP)
                {
                    if(CekAkun.isUsed == false)
                    {
                        if (CekAkun.ExpiredToken > DateTime.Now)
                        {
                            var a = changeVM.NewPass;
                            var b = changeVM.ConfirmPass;
                            if(a == b)
                            {
                                Account acc = new Account();
                                changeVM.NewPass = acc.Password;

                                CekAkun.isUsed = true;
                                context.Entry(CekAkun).State = EntityState.Modified; //buat update data di account nya
                                context.SaveChanges();
                            }
                            else
                            {
                                return 6; //Password harus sama
                            }

                        }
                        else
                        {
                            return 2; // udah expired tokennya
                        }
                        
                    }
                    else if(CekOTP.isUsed == true)
                    {
                        return 4; // OTP sudah terpakai
                    }

                }
                else
                {
                    return 5; //OTP tidak sama
                }

            }
            else
            {
                return 3; //Email tidak ada
            }
            
            return 1;
        }


    }
}
