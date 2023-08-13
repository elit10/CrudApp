using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CrudApp.Models
{
    public class Accreditation
    {
        [DisplayName("MIP ID:")]
        [Required]
        public int MIPID { get; set; }




        [Key]
        public int accreditationID{ get; set; }




        [DisplayName("Engineer ID:")]
        [Required]
        public int engineerID { get; set; }


        [DisplayName("is accredited:")]
        [Required]
        public bool isAccredited { get; set; }
    }
}
