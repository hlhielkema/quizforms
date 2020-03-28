using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models
{
    public class ContactFormModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; }
    }
}
