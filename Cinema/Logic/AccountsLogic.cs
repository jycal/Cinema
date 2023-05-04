using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic
{
    public List<AccountModel>? _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }
    static public AccountModel? CurrentPassword { get; private set; }

    public AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts!.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);

    }

    public AccountModel GetById(int id)
    {
        return _accounts!.Find(i => i.Id == id)!;
    }

    public AccountModel GetByMail(string email)
    {
        return _accounts!.Find(i => i.EmailAddress == email)!;
    }

    public AccountModel CheckLogin(int type, string email, string password)
    {
        if (email == null || password == null)
        {
            return null!;
        }
        CurrentAccount = _accounts!.Find(i => i.EmailAddress == email && i.Password == password && i.Type == type);
        return CurrentAccount!;
    }

    public bool PasswordFormatCheck(string password)
    {
        string specialchars = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-";
        bool result = false;
            foreach (char charspecial in specialchars)
            {
                foreach (char charpass in password)
                {
                    if (password.Length > 6 && password.Contains(charspecial))
                    {
                        if (char.IsDigit(charpass) && password.Any(char.IsUpper) && password.Any(char.IsLower))
                        {
                            result = true;
                        }
                    }
                }
            }
        return result;
    }

    public bool EmailFormatCheck(string email)
    {
        string specialchars = "@";
        bool result = false;
            foreach (char charspecial in specialchars)
            {
                    if (email.Contains(charspecial))
                    {
                        result = true;
                    }
            }
        return result;
    }
    public SecureString HashedPass()
        {
            Console.Write("Password: ");
            SecureString pass = new SecureString();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    pass.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if(keyInfo.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.RemoveAt(pass.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            {
                return pass;
            }
}

}
