using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PromoReservationWeb.Service
{
    public class SystemEmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string systemEmailAddress = "filterkit@civicmdsg.com.ph";
            string useEmailName = "Order Confirmed – Volvo Filter Kit Promo";
            string systemEmailPassword = "QyM-J_F6,_rs_JQ";
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(systemEmailAddress, useEmailName)
            };

            mail.To.Add(new MailAddress(email));
           // mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

            mail.Subject = "Promo Kit Order Confirmation" + subject;
            mail.Body = htmlMessage;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;



            SmtpClient client = new SmtpClient("box1242.bluehost.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(systemEmailAddress, systemEmailPassword);


            try
            {
                client.SendMailAsync(mail);
            }
            catch (Exception e)
            {
                Console.Write(e.InnerException.Message);
                Console.ReadLine();
            }

           

        }
    }
}
