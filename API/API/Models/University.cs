using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Models
{
    [Table("TB_M_University")]
    public class University
    {
        [Key]
        public  int Id { get; set; }
        public string Name { get; set; }
        //[ForeignKey("Educations")]
        //public int Education_Id { get; set; }
        [JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }
    }
}
