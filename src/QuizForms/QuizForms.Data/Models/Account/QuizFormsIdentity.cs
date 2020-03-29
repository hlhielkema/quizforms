using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace QuizForms.Data.Models.Account
{
    public sealed class QuizFormsIdentity : IIdentity
    {
        public string Name { get; private set; }

        public QuizFormsIdentity(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException(nameof(username));

            Name = username;
        }

        public string AuthenticationType => "QuizFormsIdentityAuth";

        public bool IsAuthenticated => true;
    }
}
