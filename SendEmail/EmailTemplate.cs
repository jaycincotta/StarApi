using SendGrid.Helpers.Mail;

namespace StarApi.SendEmail
{
    public abstract class EmailTemplate
    {
        public abstract string TemplateName { get; }
        public abstract string ToName { get; }
        public abstract string ToEmail{ get; }
        public abstract string Subject { get; }
        public abstract string Text { get; }
        public abstract string Html { get; }
        public EmailAddress To => new EmailAddress(ToEmail, ToName);
        public EmailAddress From => new EmailAddress("support@equal.vote", "STAR Vote Elections");
    }
}
