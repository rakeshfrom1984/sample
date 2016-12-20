using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net.Mime;
using System.Web.Helpers;
using System.Web.Security;
using System.Web.Mvc;


namespace Motormechs.Web.Helper
{
    public class EmailHandler
    {
        #region "Private Variables of this class"
        string _mailTo;
        string _mailFrom;
        string _mailFromName;
        string _mailSubject;
        string _mailBody;
        string _mailAttachmentPath;
        string _mailCC;
        string _mailBcc;
        string _smtpHost = ConfigurationManager.AppSettings["smtpHost"].ToString();
        string _smtpUser = ConfigurationManager.AppSettings["smtpUserName"].ToString();
        string _smtpPassword = ConfigurationManager.AppSettings["smtpPassword"].ToString();
        int _port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpEmailPort"].ToString());
        //string _SupportEmail = ConfigurationManager.AppSettings["SupportEmail"].ToString();
        //string _MailFrom = ConfigurationManager.AppSettings["MailFrom"].ToString();
        //string _ReplyTo = ConfigurationManager.AppSettings["ReplyTo"].ToString();
        #endregion

        #region "Public Properties"
        public string mailTo
        {
            get
            {
                return _mailTo;
            }
            set
            {
                _mailTo = value;
            }
        }
        public string mailFrom
        {
            get
            {
                return _mailFrom;
            }
            set
            {
                _mailFrom = value;
            }
        }
        public string mailFromName
        {
            get
            {
                return _mailFromName;
            }
            set
            {
                _mailFromName = value;
            }
        }
        public string mailSubject
        {
            get
            {
                return _mailSubject;
            }
            set
            {
                _mailSubject = value;
            }
        }
        public string mailBody
        {
            get
            {
                return _mailBody;
            }
            set
            {
                _mailBody = value;
            }
        }
        public string mailAttachmentPath
        {
            get
            {
                return _mailAttachmentPath;
            }
            set
            {
                _mailAttachmentPath = value;
            }
        }
        public string mailCC
        {
            get
            {
                return _mailCC;
            }
            set
            {
                _mailCC = value;
            }
        }
        public string mailBcc
        {
            get
            {
                return _mailBcc;
            }
            set
            {
                _mailBcc = value;
            }
        }

        #endregion

        #region "Public Functions"

        //Public function to send email
        public void SendEmail()
        {
            try
            {
                if (string.IsNullOrEmpty(mailFrom))
                {
                    mailFrom = System.Configuration.ConfigurationManager.AppSettings["MailFrom"].ToString();
                }

                System.Net.Mail.MailMessage objMsg = new MailMessage();
                objMsg.To.Add(mailTo);
                objMsg.From = new MailAddress(mailFrom, "MotorMechs");
                objMsg.IsBodyHtml = true;
                objMsg.Subject = mailSubject;
                objMsg.Body = mailBody;

                objMsg.Priority = System.Net.Mail.MailPriority.Normal;
                System.Net.NetworkCredential objAuthontication = new System.Net.NetworkCredential(_smtpUser, _smtpPassword);
                SmtpClient objSmtp = new SmtpClient();
                //objSmtp.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis;
                objSmtp.Host = _smtpHost;
                objSmtp.Port = _port;
                objSmtp.UseDefaultCredentials = false;
                objSmtp.EnableSsl = true;
                objSmtp.Credentials = objAuthontication;
                objSmtp.Send(objMsg);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
             

        #endregion
    }
}