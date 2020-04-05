using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.Account
{
    public sealed class CreateAccountModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Gebruikersnaam", Prompt = "Gebruikersnaam")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord", Prompt = "Wachtwoord")]
        public string Password { get; set; }
    }
}
