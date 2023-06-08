public static class MailAccess
{
    public static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/mail.html"));

    public static string path2 = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/registrationmail.html"));

    public static string LoadAll()
    {
        string body = File.ReadAllText(path);
        return body;
    }

    public static string LoadAllRegistration()
    {
        string body = File.ReadAllText(path2);
        return body;
    }
}
