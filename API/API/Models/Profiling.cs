using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Models
{
    [Table("TB_M_Profiling")]
    public class Profiling
    {
        [Key]
        public string Nik { get; set; }
        [ForeignKey("Education")]
        public int Education_Id { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual Education Education { get; set; }
        
    }
}
