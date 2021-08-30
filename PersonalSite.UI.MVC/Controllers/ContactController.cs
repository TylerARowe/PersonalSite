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
        //Contact - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //When a class has validation attributes, that validation should be checked before attempting to process any of the data they've provided. If they're going to send us something, before we even try to do anything with it we need to validate that its the correct data. 

            if (!ModelState.IsValid)
            {
                //Send them back to the form, by passing the object to the view,
                //the form returns with the original information they provided

                return View(cvm);
            }

            //Only exectures if the form (object) passes the model validation
            //Build the message -- what we will see when we receive their email.

            string message = $"You have recieved an email from {cvm.Name} with a subject: " +
                $"{cvm.Subject}. Please respond to {cvm.Email} with your response to the " +
                $"following message: <br/> {cvm.Message}";

            //MailMessage(what actually sends the email.)

            MailMessage mm = new MailMessage(
                //From
                ConfigurationManager.AppSettings["EmailUser"].ToString(),
                //To
                //you@yourdomain.ext, could be you@gmail.com, hotmail, etc.
                //Sometimes -- SmarterASP has experienced issues with forwarding, if you find the forwarding sdoes not want to work as it's written here, you can hardcode the email you want to forward to
                ConfigurationManager.AppSettings["EmailTo"].ToString(),
                cvm.Subject,
                message
                );

            //MailMessage properties
            //Allow html formatting in the email (message has HTML in it) (similar to Html.Raw())
            mm.IsBodyHtml = true;
            //If you want to mark these emails with "high priority"
            mm.Priority = MailPriority.High;
            //If you dont set this, the default is normal.

            //Respond to the senders email instead of our own SMTP client (webmail)
            mm.ReplyToList.Add(cvm.Email);

            //SMTP client
            //This is the information from the host, in our case this is SmarterASP
            //This allows the email to actually be sent.

            SmtpClient Client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());
            //Client Credentials (SmarterASP requires your username and password.)
            Client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString());

            //It is possible to have configuration issues, so we can encapsulate our code in a try catch
            try
            {
                Client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry, but your request could not be completed at this time. " +
                    $"Please try again later. Error Message: <br/> {ex.StackTrace}";
                throw;
            }

            return View("EmailConfirmation", cvm);
        }
    }
}