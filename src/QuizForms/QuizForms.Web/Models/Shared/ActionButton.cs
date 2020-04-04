using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizForms.Web.Models.Shared
{
    public class ActionButton
    {
        public string Action { get; set; }

        public string Id { get; set; }

        public string Label { get; set; }

        public ActionButton(string action, string id, string label)
        {
            Action = action;
            Id = id;
            Label = label;
        }
    }
}
