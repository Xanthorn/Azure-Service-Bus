using System.Net.Mail;

namespace API.Sender.Validators
{
    public static class EmailValidator
    {
        public static bool Validate(string email)
        {
            try
            {
                string emailAddress = new MailAddress(email).Address;
            }

            catch(FormatException)
            {
                return false;
            }

            return true;
        }
    }
}
