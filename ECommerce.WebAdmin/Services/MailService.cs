using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ECommerce.WebAdmin.Services
{
    public class MailService : IMailService
    {
        public bool SendEmail(string from, string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage(from, to, subject, body);
                var client = new SmtpClient();
                client.Send(message);
                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
            return true;
        }
    }


    public class MockMailService : IMailService
    {
        public bool SendEmail(string from, string to, string subject, string body)
        {
            Debug.Write("Email Sent!");
            return true;
        }
    }


    public interface IMailService
    {
        bool SendEmail(string from, string to, string subject, string body);
    }
}