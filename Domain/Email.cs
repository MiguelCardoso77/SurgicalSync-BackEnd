namespace DDDNetCore.Domain
{
    public class Email
    {
        public string EmailContent { get; private set; }
        public string Destination { get; private set; }
        public string Subject { get; private set; }
        
        public Email(string emailContent, string destination, string subject)
        {
            if (emailContent == null || destination == null || subject == null)
            {
                throw new System.ArgumentNullException();
            }
            
            this.EmailContent = emailContent;
            this.Destination = destination;
            this.Subject = subject;
        }
    }
}