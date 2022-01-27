﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Models
{
    [Table("TB_M_Account")]
    public class Account
    {
        [Key]
        public string Nik { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        public virtual Profiling Profiling { get; set; }

        public int OTP { get; set; }
        public DateTime ExpiredToken { get; set; }
        public Boolean isUsed { get; set; }
    }
}
