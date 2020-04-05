using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.Contact
{
    public class ContactFormModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Emailadres", Prompt = "Emailadres")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Bericht", Prompt = "Bericht")]
        public string Message { get; set; }
    }
}
