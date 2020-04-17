using QuizForms.Data.Models.Forms;
using System.Collections.Generic;

namespace QuizForms.Web.Models.Home
{
    public class HomeViewModel
    {
        public List<FormInfo> VisibleForms { get; set; }

        public long LastChangedTimestamp { get; set; }
    }
}
