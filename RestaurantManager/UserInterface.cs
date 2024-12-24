class UserInterface
{
    private Database _database;

    public UserInterface(Database database)
    {
        _database = database;
    }
    public static void DisplayMainMenu(Database database)
    {

        while (true)
        {

            string header = "Welcome to the krusty krab Restaurant!";
            List<string> options = new List<string> { "Login", "Register new Account", "Exit" };
            int option = Navigation.DisplayNavigation(header, options);

            switch (option)
            {

                case 0:
                    DisplayLoginScreen(database);
                    break;
                case 1:
                    DisplayRegisterScreen(database);
                    break;
                case 2:
                    DisplayMessage("See you next Time!");
                    return;

            }
        }

    }

    private static void DisplayLoginScreen(Database database)
    {
         string header = "What type of account would you like to Login as?";
        List<string> options = new List<string>() { "User Account - Recommended for users.", "Admin Account - For Restaurant staff only.", "Return - Returns back to main menu." };
        int option = Navigation.DisplayNavigation(header, options);
        bool isAdmin = false;
        switch (option)
        {
            case 0:
                isAdmin = false;
                break;
            case 1:
                isAdmin = true;
                break;
            case 2:
                break;
        }
        string username = CredentialPrompts.HandledReadUserName();
        string password = CredentialPrompts.HandledReadPassword();
        User user = new User(username,password,isAdmin);
        if(user.Login(database))
        {
            Console.WriteLine("user logged in!");
            Console.ReadKey();
        }
        else{
            Console.WriteLine("user not found!");
            Console.ReadKey();
        }
       
    }
    private static void DisplayRegisterScreen(Database database)
    {
        string header = "What type of account would you like to Register as?";
        List<string> options = new List<string>() { "User Account - Recommended for users.", "Admin Account - For Restaurant staff only.", "Return - Returns back to main menu." };
        int option = Navigation.DisplayNavigation(header, options);
        bool isAdmin = false;
        switch (option)
        {
            case 0:
                isAdmin = false;
                break;
            case 1:
                //Admin secret and hash in code for debug purpose only
                // @AdminSecret1423                                                   
                // 428d66aaea017668a44a8fcfe0bb685d7b4b2321a19d82e4e05b6eabdd0d13b6
                const string adminSecrectHash = "428d66aaea017668a44a8fcfe0bb685d7b4b2321a19d82e4e05b6eabdd0d13b6";
                Console.Write("This Gate is for Admins Only, please Enter the secret to register an AdminAccount: ");
                string inputStringSecret = Console.ReadLine();
                if (!Authenticator.VerifyPassword(inputStringSecret, adminSecrectHash))
                {
                    Console.WriteLine("Wrong Secret! You are not allowed to register an admin account.");
                    Console.WriteLine("Press Any key to continue...");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Correct Secret! Please Continue Registering.");
                isAdmin = true;
                break;
            case 2:
                break;
        }
        string username;
        string password;
        while (true)
        {
            username = CredentialPrompts.HandledReadUserName();
            if (database.GetUser(username) == null)
            {
                break;
            }
            else
            {
                Console.WriteLine("Username is in use, please try a different username.");
            }
        }
        while (true)
        {
            password = CredentialPrompts.HandledReadPassword();
            string reEnteredPassword = CredentialPrompts.HandledReadPassword("Please ReEnter the password: ");
            if (password == reEnteredPassword)
            {
                break;
            }
            else
            {
                Console.WriteLine("passwords do not match! Please try again.");
            }
        }
        string hashedPassword = Authenticator.HashPassword(password);
        User.Register(username, hashedPassword, isAdmin, database);
        DisplayMessage("User Account is now Registered please press any key to return...");
        Console.ReadKey();

    }
    private static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }



}