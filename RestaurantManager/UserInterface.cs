using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.Serialization;

/// <summary>
/// The class incharge of managing the UserInterface
/// </summary> <summary>
/// 
/// </summary>
class UserInterface
{
    /// <summary>
    /// a function to display the main menu for a database
    /// </summary>
    /// <param name="database"></param>
    public static void DisplayMainMenu(Database database)
    {
        while (true)
        {
            //print main menu
            string header = "Welcome to the krusty krab Restaurant!";
            List<string> options = new List<string> { "Login", "Register new Account", "Exit" };
            //create navigation menu for Login/Registering account.
            int option = Navigation.DisplayNavigation(header, options);

            switch (option)
            {

                case 0:
                    //login Account
                    DisplayLoginScreen(database);
                    break;
                case 1:
                    //register Account
                    DisplayRegisterScreen(database);
                    break;
                case 2:
                    //exit application
                    DisplayMessage("See you next Time!");
                    return;

            }
        }

    }
   
    /// <summary>
    /// Display a User account page after login
    /// </summary>
    /// <param name="user"></param>
    /// <param name="database"></param>
    private static void DisplayUserPage(User user, Database database)
    {
        while (true)
        {
            //prepare for a navigation menu for user account
            string header = $"Hi {user.Username} (User Account), what are you willing to do?";
            List<string> options = new List<string>() { "Browse Items - allows you to browse items and add to your shopping cart.",
            "View Cart - Shows items inside your shopping cart.",
             "Remove Cart Item - Removes an item from your shopping cart.",
              "CheckOut - removes a published item.",
              "Add/Check Credits - allows you to view or add credits",
               "Logout - Returns back to main menu." };
            //create the user navigation menu for user actions
            int option = Navigation.DisplayNavigation(header, options);

            switch (option)
            {
                case 0:
                    if (Navigation.CheckAndHandleListEmpty<Item>(database.Items, "There are no items in the list"))
                    {
                        //return back to the menu
                        break;
                    }
                    //if itemslist not empty then browse items
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
                    //Log out
                    int Choice = Navigation.ConfirmLogOut();
                    switch (Choice)
                    {
                        case 0:
                            Console.WriteLine("You have been logged out, Press any key to continue...");
                            Console.ReadKey();
                            return;
                        case 1:
                            Console.WriteLine("Logout cancelled, Press any key to continue...");
                            Console.ReadKey();
                            continue;
                    }
                    break;
            }
        }
    }
    /// <summary>
    /// Displays the admin page after login.
    /// </summary>
    /// <param name="admin"></param>
    /// <param name="database"></param> <summary>
    /// 
    /// </summary>
    /// <param name="admin"></param>
    /// <param name="database"></param>
    private static void DisplayAdminPage(Admin admin, Database database)
    {
        while (true)
        {
            //prepare for an admin navigation page
            string header = $"Hi {admin.Username} (Admin Account), what are you willing to do?";
            List<string> options = new List<string>() { "View Items - shows all published items.",
            "List New Item - adds an item for users.",
             "Edit Item - edits properties that belongs to an Item.",
              "Remove an Item - removes a published item.",
              "Remove a User account - removes a user account from the list",
               "Logout - Returns back to main menu." };
            //build the navigation menu for admin actions
            int option = Navigation.DisplayNavigation(header, options);

            switch (option)
            {
                case 0:
                    if (Navigation.CheckAndHandleListEmpty<Item>(database.Items, "There are no items in the list"))
                    {
                        //return back to the menu
                        break;
                    }
                    //else: show items that are publised by admins
                    Admin.ViewItems(database);
                    break;
                case 1:
                    Admin.ListNewItem(database);
                    break;
                case 2:

                    if (Navigation.CheckAndHandleListEmpty<Item>(database.Items, "There are no items in the list"))
                    {
                        //return back to the menu
                        continue;
                    }
                    //else: edit item fucntion is called
                    Admin.EditItem(database);
                    break;
                case 3:
                    if (Navigation.CheckAndHandleListEmpty<Item>(database.Items, "There are no items in the list"))
                    {
                        //return back to the menu
                        continue;
                    }
                    //else remove item function is called
                    Admin.RemoveItem(database);
                    break;
                case 4:
                    List<User> userAccountList = new List<User>();
                    foreach (Account account in database.AccountData)
                    {
                        if (!account.IsAdmin)
                            userAccountList.Add((User)account);
                    }
                    if (Navigation.CheckAndHandleListEmpty<User>(userAccountList, "There are no Users in the list."))
                    {
                        continue;
                    }
                    Admin.RemoveUser(database);
                    break;
                case 5:
                    //log out
                    int Choice = Navigation.ConfirmLogOut();
                    switch (Choice)
                    {
                        case 0:
                            Console.WriteLine("You have been logged out, Press any key to continue...");
                            Console.ReadKey();
                            return;
                        case 1:
                            Console.WriteLine("Logout cancelled, Press any key to continue...");
                            Console.ReadKey();
                            continue;
                    }
                    break;

            }
        }
    }
    /// <summary>
    /// Displays a general login screen for an account.
    /// </summary>
    /// <param name="database"></param>
    private static void DisplayLoginScreen(Database database)
    {

        //read username and password txt
        string username = CredentialPrompts.HandledReadUserName();
        string password = CredentialPrompts.HandledReadPassword();
        //create an account instance to login and find the account
        Account account = Account.Login(username, password, database);
        if (account != null) // if account found
        {
            //check the type of the account
            //if admin
            if (account.IsAdmin)
            {
                //Login as admin
                Admin admin = (Admin)account;
                DisplayMessage("Login Successful for Admin Account! Press any key to continue...");
                Console.ReadKey();
                //display admin page
                DisplayAdminPage(admin, database);
            }//if user
            else
            {
                //login as user
                User user = (User)account;
                DisplayMessage("Login Successful! Press any key to continue...");
                Console.ReadKey();
                //display user page
                DisplayUserPage(user, database);
            }
        }
        else//if account not found
        {
            //display error
            DisplayMessage("account not found, username and/or password is wrong! Press any key to return...");
            Console.ReadKey();
        }
    }
    /// <summary>
    /// Displays the registering screen
    /// </summary>
    /// <param name="database"></param>
    private static void DisplayRegisterScreen(Database database)
    {
        //prepare content for navigation menu.
        string header = "What type of account would you like to Register as?";
        List<string> options = new List<string>() { "User Account - Recommended for users.", "Admin Account - For Restaurant staff only.", "Return - Returns back to main menu." };
        //create navigation menu.
        int option = Navigation.DisplayNavigation(header, options);
        //create a boolean to check if the user want to register as admin or as user
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
                //if admin guesses the correct password then continue login or else show that the password is wrong and quit.
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
                //return to main menu
                return;
        }
        //declare username, password;
        string username, password;
        while (true)
        {
            //try read username and check if the username is already in use.
            username = CredentialPrompts.HandledReadUserName();
            if (database.GetUser(username) == null)
            {
                break;
            }
            else
            {
                //show that username is in use and try again
                Console.WriteLine("Username is in use, please try a different username.");
            }
        }
        while (true)
        {
            //read password
            password = CredentialPrompts.HandledReadPassword();
            //read password again
            string reEnteredPassword = CredentialPrompts.HandledReadPassword("Please ReEnter the password: ");
            //if password isnt the same as the reEntered password then inform the user that they don't match to try again or if they match continue.
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
        //if admin
        if (isAdmin)
        {
            //register admin account and store the hashed password instead of plain text and inform that an admin account is registered
            Admin.Register(username, hashedPassword, database);
            DisplayMessage("Admin Account is now Registered please press any key to return...");
            Console.ReadKey();
        }
        else
        {
            //register user account and store the hashed password instead of plain text and inform that an user account is registered
            User.Register(username, hashedPassword, database);
            DisplayMessage("User Account is now Registered please press any key to return...");
            Console.ReadKey();
        }
        new DataSerializer<Database>(database).SaveData();

    }
    /// <summary>
    /// a simpler way to write Console.Writeline(), to display messages.
    /// </summary>
    /// <param name="message"></param> <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    private static void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }



}