using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.Authentication
{
    public sealed class SignInModel
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

        [StringLength(500)]
        [HiddenInput]
        public string Redirect { get; set; }
    }
}
