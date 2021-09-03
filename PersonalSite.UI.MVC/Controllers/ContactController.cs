using PersonalSite.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PersonalSite.UI.MVC.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //Basic steps for sending an email in .NET
            //Step 1) Create a string for the mail body.
            string body = $"Message received from my personal website. From {cvm.Name} - Subject {cvm.Subject}<br />Message:{cvm.Message}";

            //Step 2) Create the mailmessage object and customize
            MailMessage msg = new MailMessage("noreply@tylerarowe.com", "tyl3rr0we95@gmail.com", "Message from your contact form,", body);

            msg.IsBodyHtml = true;

            //Step 3) Create the smtpClient object -> supply mail server, username, and wp
            SmtpClient client = new SmtpClient("mail.tylerarowe.com");
            client.Credentials = new NetworkCredential("noreply@tylerarowe.com", "100%OrangeJuice");
            client.Port = 8889;

            //Step 4) attempt to send the email

            try
            {
                client.Send(msg);
                return View("EmailConfirmation", cvm);
            }
            catch (System.Exception)
            {
                ViewBag.ErrorMessage = "Something went wrong - please try again.";
                return View(cvm);
            }
        }
    }
}