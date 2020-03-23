using QuizForms.Data.Models.Forms;
using System.Collections.Generic;

namespace QuizForms.Web.Models
{
    public class HomeViewModel
    {
        public List<FormInfo> AvailableForms { get; set; }
    }
}
