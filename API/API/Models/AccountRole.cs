using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("TB_M_AccountRole")]
    public class AccountRole
    {
        [ForeignKey("Account")]
        public string Account_Nik { get; set; }
        [ForeignKey("Role")]
        public int Role_Id { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
