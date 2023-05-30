using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public static class MailAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/mail.html"));

    static string path2 = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/registrationmail.html"));

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
