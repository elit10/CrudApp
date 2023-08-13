using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CrudApp.Models
{
    public class MIP
    {
        [Key]
        public int MIPID { get; set; }

        [DisplayName("MIP Name:")]
        [Required]
        public string MIPName { get; set; }

    }
}