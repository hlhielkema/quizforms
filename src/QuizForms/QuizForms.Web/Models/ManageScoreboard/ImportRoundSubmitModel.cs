using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizForms.Web.Models.ManageScoreboard
{
    public class ImportRoundSubmitModel
    {       
        [Required]
        public List<KeyValuePair<Guid, string>> Mappings { get; set; }
    }
}
