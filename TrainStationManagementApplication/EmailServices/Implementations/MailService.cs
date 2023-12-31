﻿using TrainStationManagementApplication.Dto;
using TrainStationManagementApplication.EmailServices.Interfaces;
using System.Diagnostics;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using Newtonsoft.Json.Linq;

namespace TrainStationManagementApplication.EmailServices.Implementations
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public string _mailApiKey;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mailApiKey = _configuration.GetSection("MailConfig")["mailApikey"]; 
        }

        public async Task<bool> SendEMailAsync(CreateMailRequestModel email)
        {
            if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
            {
                Configuration.Default.ApiKey.Add("api-key", _mailApiKey);
            }
            var apiInstance = new TransactionalEmailsApi();
            string SenderName = "Anthony Train Station";
            string SenderEmail = "ebukaanthony9@gmail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(email.ToEmail, email.ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>
            {
                smtpEmailTo
            };
            string BccName = "Janice Doe";
            string BccEmail = "example2@example2.com";
            SendSmtpEmailBcc BccData = new SendSmtpEmailBcc(BccEmail, BccName);
            List<SendSmtpEmailBcc> Bcc = new List<SendSmtpEmailBcc>
            {
                BccData
            };
            string CcName = "John Doe";
            string CcEmail = "example3@example2.com";
            SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>
            {
                CcData
            };
            string TextContent = null;
            string ReplyToName = "Anthony Train Station";
            string ReplyToEmail = "ebukaanthony9@gmail.com";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);
            string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            string AttachmentUrl = null;
            string AttachmentName = email.AttachmentName ?? "Welcome.txt";
            byte[] Content = System.Convert.FromBase64String(stringInBase64);
            SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>
            {
                AttachmentContent
            };
            JObject Headers = new JObject
            {
                { "Some-Custom-Name", "unique-id-1234" }
            };
            long? TemplateId = null;
            JObject Params = new JObject
            {
                { "parameter", "My param value" },
                { "subject", "Dansnom" }
            };
            List<string> Tags = new List<string>
            {
                "mytag"
            };
            SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(email.ToEmail, email.ToName);
            List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>
            {
                smtpEmailTo1
            };
            Dictionary<string, object> _parmas = new Dictionary<string, object>
            {
                { "params", Params }
            };
            SendSmtpEmailReplyTo1 ReplyTo1 = new SendSmtpEmailReplyTo1(ReplyToEmail, ReplyToName);
            SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, _parmas, Bcc, Cc, ReplyTo1, email.Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>
            {
                messageVersion
            };
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, Bcc, Cc, email.HtmlContent, TextContent, email.Subject, ReplyTo, Attachment, Headers, TemplateId, Params, messageVersiopns, Tags);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Debug.WriteLine(result.ToJson());
                return true;
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }

        }

    }
}
