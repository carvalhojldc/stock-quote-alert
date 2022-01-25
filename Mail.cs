using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
namespace MailInterface
{
    internal class Mail
    {
        private string smtpServer, fromEmail, fromPassword, toEmail;
        private int smtpPort;
        public Mail()
        {
            try
            {
                string text = File.ReadAllText(@"mail.config");
                var data = JsonConvert.DeserializeObject<dynamic>(text);
                string smtpPort = null;
                if (data != null)
                {
                    this.smtpServer = data.SelectToken("smtpServer");
                    smtpPort = data.SelectToken("smtpPort");
                    this.fromEmail = data.SelectToken("fromEmail");
                    this.fromPassword = data.SelectToken("fromPassword");
                    this.toEmail = data.SelectToken("toEmail");
                }

                if (this.smtpServer == null || smtpPort == null || this.fromEmail == null ||
                     this.fromPassword == null || this.toEmail == null
                    )
                {
                    Console.WriteLine("\tERROR Mail config not set");
                    Environment.Exit(1);
                }
                this.smtpPort = Convert.ToInt16(smtpPort);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tERROR " + ex.Message);
                Environment.Exit(1);
            }
        }

        public void Send(string subject, string body)
        {
            var fromAddress = new MailAddress(this.fromEmail, "Stock Quote Systems");
            var toAddress = new MailAddress(this.toEmail, "");

            var smtp = new SmtpClient
            {
                Host = this.smtpServer,
                Port = this.smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, this.fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tERROR " + ex.Message);
            }

        }
    }
}
