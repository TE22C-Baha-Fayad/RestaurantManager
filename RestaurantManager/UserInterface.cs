using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Metadata;

class UserInterface
{
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
    static bool CheckAndHandleItemListEmpty(Database database)
    {
        if (database.items.Count == 0)
        {
            DisplayMessage("There are no Items, Press any key to return...");
            Console.ReadKey();
            return true;
        }
        return false;
    }

    private static void DisplayUserPage(User user, Database database)
    {
        while (true)
        {

            string header = $"Hi {user.username}, what are you willing to do?";
            List<string> options = new List<string>() { "Browse Items - allows you to browse items and add to your shopping cart.",
            "View Cart - Shows items inside your shopping cart.",
             "Remove Cart Item - Removes an item from your shopping cart.",
              "CheckOut - removes a published item.",
              "Add/Check Credits - allows you to view or add credits",
               "Logout - Returns back to main menu." };
            int option = Navigation.DisplayNavigation(header, options);

            switch (option)
            {
                case 0:
                    if (CheckAndHandleItemListEmpty(database))
                    {
                        break;
                    }
                    user.BrowseItems(database);
                    break;
                case 1:
                    user.ViewCartItems();
                    break;
                case 2:
                    user.RemoveCartItem();
                    break;
                case 3:
                    user.CheckOut();
                    break;
                case 4:
                    user.SeeOrAddCredits();
                    break;
                case 5:
                    return;
            }
        }
    }
    private static void DisplayAdminPage(Admin admin, Database database)
    {

        while (true)
        {

            string header = $"Hi {admin.username}, what are you willing to do?";
            List<string> options = new List<string>() { "View Items - shows all published items.",
            "List New Item - adds an item for users.",
             "Edit Item - edits properties that belongs to an Item.",
              "Remove an Item - removes a published item.",
               "Logout - Returns back to main menu." };
            int option = Navigation.DisplayNavigation(header, options);

            switch (option)
            {
                case 0:
                    if (CheckAndHandleItemListEmpty(database))
                    {
                        continue;
                    }
                    admin.ViewItems(database);
                    break;
                case 1:
                    admin.ListNewItem(database);
                    break;
                case 2:

                    if (CheckAndHandleItemListEmpty(database))
                    {
                        continue;
                    }
                    admin.EditItem(database);

                    break;
                case 3:
                    if (CheckAndHandleItemListEmpty(database))
                    {
                        continue;
                    }
                    admin.RemoveItem(database);
                    break;
                case 4:
                    return;
            }
        }
    }
    private static void DisplayLoginScreen(Database database)
    {
        string username = CredentialPrompts.HandledReadUserName();
        string password = CredentialPrompts.HandledReadPassword();
        Account account = Account.Login(username, password, database);
        if (account != null)
        {
            if (account.GetType() == typeof(Admin))
            {
                Admin admin = (Admin)account;
                DisplayMessage("Login Successful for Admin Account! Press any key to continue...");
                Console.ReadKey();
                DisplayAdminPage(admin, database);
            }
            else if (account.GetType() == typeof(User))
            {
                User user = (User)account;
                DisplayMessage("Login Successful! Press any key to continue...");
                Console.ReadKey();
                DisplayUserPage(user, database);
            }
        }
        else
        {
            DisplayMessage("account not found, username and/or password is wrong! Press any key to return...");
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
                string inputStringSecret = "@AdminSecret1423"; //debug remove later 
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
                return;
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
        if (isAdmin)
        {
            Admin.Register(username, hashedPassword, database);
            DisplayMessage("User Account is now Registered please press any key to return...");
            Console.ReadKey();
        }
        else
        {

            User.Register(username, hashedPassword, database);
            DisplayMessage("User Account is now Registered please press any key to return...");
            Console.ReadKey();
        }

    }
    private static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }



}