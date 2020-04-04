using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.Account
{
    public sealed class CreateAccountModel
    {
        [Required]
        [StringLength(50)]        
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
