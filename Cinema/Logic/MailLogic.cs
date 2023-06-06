namespace Cinema.Logic
{
    public class MailLogic
    {
        private string _mail;
        public MailLogic()
        {
            _mail = MailAccess.LoadAll();
        }

        // public static string CreateBody()
        // {

        //     string body = MailAccess.LoadAll();
        //     body = body.Replace

        // }
    }
}