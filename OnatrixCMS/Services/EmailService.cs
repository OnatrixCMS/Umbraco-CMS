using Azure;
using Azure.Communication.Email;
using OnatrixCMS.Models;

namespace OnatrixCMS.Services
{
    public class EmailService()
    {
        public async Task<bool> SendConfirmationEmailAsync(ContactForm form)
        {
            var connectionString = "endpoint=https://emailconfirmation.europe.communication.azure.com/;accesskey=DjTHzrEDjC8KiFjJkzgAhAgha4C60zjmuPa4tapq6rfdevQ09mjUJQQJ99AIACULyCpq7IbPAAAAAZCSHZqJ";
            var emailClient = new EmailClient(connectionString);

            var emailContent = new EmailContent("Tack för att du kontaktade oss!")
            {
                PlainText = "Hej " + form.Name + ",\n\nTack för att du kontaktade oss. Vi återkommer snart!",
                Html = "<p>Hej " + form.Name + ",</p><p>Tack för att du kontaktade oss. Vi återkommer snart!</p>"
            };

            var emailMessage = new EmailMessage(
                senderAddress: "donotreply@eb07405f-2664-4a76-96ff-e67ee0f2ac0b.azurecomm.net",
                content: emailContent,
                recipients: new EmailRecipients(new List<EmailAddress>
                {
            new EmailAddress(form.Email)
                })
            );

            try
            {
                EmailSendOperation emailSendOperation = await emailClient.SendAsync(WaitUntil.Completed, emailMessage);
                return emailSendOperation.HasCompleted;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
