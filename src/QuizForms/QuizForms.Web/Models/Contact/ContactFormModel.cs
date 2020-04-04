using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.Contact
{
    public class ContactFormModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
