using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalSite.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Email address is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "* Subject is required.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "* Message is required.")]
        [UIHint("MultilineText")] //Provide a lareger TextArea in the UI.
        public string Message { get; set; }
    }
}