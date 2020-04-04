using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.Account
{
    public sealed class UpdatePasswordModel
    {
        [Required]
        [StringLength(50)]        
        [ReadOnly(true)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
