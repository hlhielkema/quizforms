using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.ManageScoreboard
{
    public class CreateScoreboardModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Titel", Prompt = "Titel")]
        public string Title { get; set; }
    }
}
