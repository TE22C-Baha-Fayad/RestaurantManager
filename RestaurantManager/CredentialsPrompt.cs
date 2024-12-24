using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

class CredentialPrompts{
    

    public static string HandledReadUserName()
    {
        while (true)
        {
            Console.Write("Please Enter Username: ");
            string username = Console.ReadLine();
            if (username.Length < 3)
            {
                Console.WriteLine("\nThe username must contain 3 or more characters.");
            }
            else
            {
                return username;
            }
        }
    }
    public static string HandledReadPassword(string instruction = "Please Enter password: ")
    {
        while (true)
        {
            Console.Write(instruction);
            string password = Console.ReadLine();
            if (password.Length < 8)
            {
                Console.WriteLine("\nThe password must contain 8 or more characters.");
            }
            else
            {
                
                return password;
            }
        }
    }
}