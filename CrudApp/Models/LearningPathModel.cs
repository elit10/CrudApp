using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CrudApp.Models
{
    public class LearningPathModel
    {
        [Key]
        public int LPID { get; set; }


        [DisplayName("MIP Key:")]
        [Required]
        public int MIPID { get; set; }


        [DisplayName("Name:")]
        [Required]
        public string LPName { get; set; }

    }
}
