/// <summary>
/// A basic class that handles prompts regarding credentials and other related information-
/// </summary>
class CredentialPrompts
{
    /// <summary>
    /// a function that Reads a string and converts to float with Error handling capablility.
    /// </summary>
    /// <param name="context"></param>
    /// <returns>the converted number as float</returns>
    public static float HandledReadFloat(string context)
    {
        while (true)
        {
            //print context question
            Console.Write(context);
            //read string
            string ammountString = Console.ReadLine();
            float ammount;
            //tryparse the string into float
            if (float.TryParse(ammountString, out ammount))
            {
                //if succeed return cost
                return ammount;
            }
            else
            {
                //inform the format is wrong and try again.
                Console.WriteLine("Wrong format! please enter a number with no letters. Ex: 125.99");
            }
        }
    }
    /// <summary>
    /// a function that Reads a username with ceratin rules with Error handling capablility.
    /// </summary>
    /// <returns>string username</returns>
    public static string HandledReadUserName()
    {
        while (true)
        {
            //ask for username
            Console.Write("Please Enter Username: ");
            //read username
            string username = Console.ReadLine();
            //if username is less than 3 characters 
            if (username.Length < 3)
            {
                //display error and try again
                Console.WriteLine("\nThe username must contain 3 or more characters.");
            }
            else
            {
                return username;
            }
        }
    }
    /// <summary>
    /// a function that Reads a password with ceratin rules with Error handling capablility.
    /// </summary>
    /// <returns>password txt</returns>
    public static string HandledReadPassword(string instruction = "Please Enter password: ")
    {
        while (true)
        {
            //ask for password
            Console.Write(instruction);
            //read password
            string password = Console.ReadLine();
            //if password length is less than 8 characters
            if (password.Length < 8)
            {
                //display error and try again
                Console.WriteLine("\nThe password must contain 8 or more characters.");
            }
            else
            {

                return password;
            }
        }
    }
}