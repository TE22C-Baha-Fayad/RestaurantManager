
//Represents a general account with basic login functionality
using System.Collections;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

class Account : IAccount
{
    //declaring properties to store username and passwordhash for an account
    [JsonInclude]
    public string Username { get; protected set; }
    [JsonInclude]
    public string PasswordHash { get; protected set; }
    [JsonInclude]
    public bool IsAdmin { get; protected set; }



    /// <summary>
    /// A function to login an account and validate credentials by a database.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="passwordTxt"></param>
    /// <param name="database"></param>
    /// <returns>Account if the account is found or null if the account isn't found.</returns>
    public static Account Login(string username, string passwordTxt, Database database)
    {
        //validate if account exists in database
        Account account = Authenticator.ValidateUser(database.AccountData, username, passwordTxt);
        //if account exists return account or else return null.
        if (account != null)
        {
            return account;
        }
        return null;
    }
}
//A child class that represents an Admin account
class Admin : Account
{

    /// <summary>
    /// a constructor with the parameters username and password to create an admin account.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public Admin(string username, string password)
    {
        Username = username;
        PasswordHash = password;
        IsAdmin = true;
    }
    /// <summary>
    /// Registers an Admin account into a database.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="database"></param>
    public static void Register(string username, string password, Database database)
    {
        //create an admin account.
        Admin admin = new(username, password) { IsAdmin = true };
        //save to data base
        database.SaveUser(admin);
    }
    /// <summary>
    /// View items list in database.
    /// </summary>
    /// <param name="database"></param> <summary>
    /// 
    /// </summary>
    /// <param name="database"></param>
    public static void ViewItems(Database database)
    {
        //print the information about each Restaurant item in Database.
        for (int i = 0; i < database.Items.Count; i++)
        {
            database.Items[i].DisplayInfo();
        }
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();

    }
    /// <summary>
    /// Lists a new item and adds to items list in database.
    /// </summary>
    /// <param name="database"></param>
    public static void ListNewItem(Database database)
    {
        //read item name
        Console.Write("Item name:");
        string itemName;
        while (true)
        {
            itemName = Console.ReadLine();
            if (itemName == "")
            {
                //if item name is empty display this message
                Console.WriteLine("This field can't be empty");
            }
            else
            {
                //or else break the loop
                break;
            }
        }
        //read item description
        Console.Write("Item Description:");
        string itemDescription = Console.ReadLine();
        //read item cost
        float itemCost = CredentialPrompts.HandledReadFloat("Item Cost:");
        //create the item
        Item item = new Item(itemName, itemDescription, itemCost);
        //save item in items list in a database.
        database.Items.Add(item);
        new DataSerializer<Database>(database).SaveData();
    }
    /// <summary>
    /// edits an item in the items list in a database
    /// </summary>
    /// <param name="database"></param>
    public static void EditItem(Database database)
    {
        while (true)
        {
            //Prepare contents of a navigation menu.
            string header = "";
            List<string> options = new List<string>();
            //adds all items names in the database itemslist as options.
            for (int i = 0; i < database.Items.Count; i++)
            {
                options.Add(database.Items[i].Name);
            }
            //create the navigation and read the picked options index.
            int option = Navigation.DisplayNavigation(header, options);
            //get item by index in the database
            Item item = database.GetItemByIndex(option);
            //display the item.
            Console.WriteLine("The item has the following info:");
            item.DisplayInfo();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            //previous info were shown and now...
            //prepare for a new navigation to edit a specific property.
            header = "";
            options = new List<string>() { "Item Name", "Item Description", "Item Cost", "Return back" };
            //create the navigation
            option = Navigation.DisplayNavigation(header, options);


            switch (option)
            {
                case 0:
                    //if the option is 0 then shows the Edit item name screen
                    Console.Write("Item Name:");
                    item.Name = Console.ReadLine();
                    Console.WriteLine("Item name changed successfully! Press any key to conitnue...");
                    Console.ReadKey();
                    return;
                case 1:
                    //if the option is 1 then shows the Edit item description screen
                    Console.Write("Item Description:");
                    item.Description = Console.ReadLine();
                    Console.WriteLine("Item Description changed successfully! Press any key to conitnue...");
                    Console.ReadKey();
                    return;
                case 2:
                    //if the option is 2 then shows the Edit item price screen
                    item.Price = CredentialPrompts.HandledReadFloat("Item Price:");
                    Console.WriteLine("Item Price changed successfully! Press any key to conitnue...");
                    Console.ReadKey();
                    return;
                case 3:
                    //if the option is 3 then return.
                    return;
            }

        }

    }
    /// <summary>
    /// Removes an item from the items list in the database.
    /// </summary>
    /// <param name="database"></param> <summary>
    /// </summary>
    /// <param name="database"></param>
    public static void RemoveItem(Database database)
    {
        //prepare contents of navigation menu.
        string header = "";
        List<string> options = new List<string>();
        for (int i = 0; i < database.Items.Count; i++)
        {
            options.Add(database.Items[i].Name);
        }
        //create navigation menu to choose an item
        int option = Navigation.DisplayNavigation(header, options);
        //get item by index
        Item item = database.GetItemByIndex(option);


        header = $"Are you sure you want to remove this item:({item.Name})? ";
        options = new List<string>() { "Yes", "No" };
        //create navigation menu to confirm choice
        option = Navigation.DisplayNavigation(header, options);
        switch (option)
        {
            case 0:
                //inform that the item is removed
                Console.WriteLine("Item is now removed! Press any key to conitnue...");
                //remove the item
                database.Items.Remove(item);
                new DataSerializer<Database>(database).SaveData();
                break;
            case 1:
                Console.WriteLine("Cancelled Removing the item! Press any key to continue...");
                break;

        }
        Console.ReadKey();


    }
    /// <summary>
    /// Removes a user in a data base.
    /// </summary> <summary>
    /// 
    /// </summary>
    public static void RemoveUser(Database database)
    {
        //prepare contents of navigation menu.
        string header = "";
        List<string> options = new List<string>();
        //create a userindex list to store the original indexes of the users because the index is not the same after filtering the accounts
        List<int> userAccountIndexes = new List<int>();
        for (int i = 0; i < database.AccountData.Count; i++)
        {
            //get each account
            Account account = database.AccountData[i];
            if (account.IsAdmin)
            {
                //skip if the account is admin
                continue;
            }
            else
            {
                //add the name of the account to the option
                options.Add(account.Username);
                userAccountIndexes.Add(i);
            }
        }
        //create navigation menu to choose a user
        int option = Navigation.DisplayNavigation(header, options);
        //create a dictionary to redefine the new indexes because the options index isn't the same as the account index in the list after filtering.
        Dictionary<int, int> indexDictionary = new Dictionary<int, int>();
        //for every index in the list
        for (int i = 0; i < userAccountIndexes.Count; i++)
        {
            //add the key as option index order and value to be the value of the user account index
            indexDictionary.Add(i, userAccountIndexes[i]);
        }
        //confirm if the user really wants to remove the account
        header = $"Are you sure you want to remove this User:({database.GetUserByIndex(indexDictionary[option]).Username})? ";
        options = new List<string>() { "Yes", "No" };
        //create navigation menu to confirm choice
        option = Navigation.DisplayNavigation(header, options);
        switch (option)
        {
            case 0:
                //remove the account by option and show message
                database.AccountData.Remove(database.GetUserByIndex(indexDictionary[option]));
                Console.WriteLine("Account has been removed! Press any key to continue...");
                break;
            case 1:
                //cancell operation
                Console.WriteLine("cancelled removing account! Press any key to continue...");
                break;
        }
        Console.ReadKey();
    }
}
/// <summary>
/// child account that Represents a User account with specific functionalites
/// </summary>
class User : Account
{
    //list of items to store items for as a shoppingcart.
    private List<Item> _shoppinCartItems = new List<Item>();

    //a filed to store the credits a user owns.
    private float _credits = 1000;
    /// <summary>
    /// A basic constructor to create a User Account 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param> <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public User(string username, string password)
    {
        this.Username = username;
        base.PasswordHash = password;
        IsAdmin = false;
    }
    /// <summary>
    /// Registers a User account in a database
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="database"></param>
    public static void Register(string username, string password, Database database)
    {
        //create a user account
        User user = new(username, password);
        //save user account in database
        database.SaveUser(user);
    }
    /// <summary>
    /// prints shopping cart properties
    /// </summary>
    private void PresentShoppingCart()
    {
        Console.WriteLine($"There are {_shoppinCartItems.Count} Items in your shopping cart.");
        float totalPrice = 0;
        foreach (Item item in _shoppinCartItems)
        {
            totalPrice += item.Price;
        }
        Console.WriteLine($"Total Price: {totalPrice}kr");
        Console.WriteLine($"Your Credits: {_credits}kr");
        Console.WriteLine();
        Console.WriteLine("Items:");
        for (int i = 0; i < _shoppinCartItems.Count; i++)
        {
            _shoppinCartItems[i].DisplayInfo();
        }
    }
    /// <summary>
    /// Browse Items published by admins
    /// </summary>
    /// <param name="database"></param>
    public void BrowseItems(Database database)
    {
        //prepare contents for a navigation menu.
        string header = $"Available Items:";
        List<string> options = new List<string>();
        for (int i = 0; i < database.Items.Count; i++)
        {
            options.Add(database.Items[i].GetInfo());
        }
        //create a navigation menu to pick an item.
        int option = Navigation.DisplayNavigation(header, options);
        //add the chosed item in the shoppingcart.
        _shoppinCartItems.Add(database.GetItemByIndex(option));
        //display message and continue.
        Console.WriteLine("Item were added to your shopping cart, press any key to continue...");
        Console.ReadKey();
    }
    /// <summary>
    /// prints cart items
    /// </summary> <summary>
    /// 
    /// </summary>
    public void ViewCartItems()
    {

        PresentShoppingCart();
        //display message to continue
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();
    }
    /// <summary>
    /// Removes an item from The shopping Cart.
    /// </summary>
    public void RemoveCartItem()
    {
        //if the item count is greater than 0
        if (Navigation.CheckAndHandleListEmpty<Item>(_shoppinCartItems, "There are no items in the shopping cart."))
        {
            return;
        }
        else
        {


            //prepare contents for navigation menu
            string header = "Which item do you want to remove?";
            List<string> options = new List<string>();
            for (int i = 0; i < _shoppinCartItems.Count; i++)
            {
                options.Add(_shoppinCartItems[i].GetInfo());
            }
            //create navigation menu to choose between items.
            int option = Navigation.DisplayNavigation(header, options);

            header = "Are you sure you want to remove this item from your cart??";
            options = new List<string>() { "Yes", "No" };
            //create navigation menu to choose between items.
            option = Navigation.DisplayNavigation(header, options);

            switch (option)
            {
                case 0:
                    //removes the chosen item.
                    _shoppinCartItems.RemoveAt(option);
                    //displays a message to continue.
                    Console.WriteLine("Item is now removed from your shopping cart, press any key to continue...");
                    break;
                case 1:
                    //cancell
                    Console.WriteLine("Item Remove cancelled! Press any key to continue...");
                    break;

            }
            Console.ReadKey();
        }
    }
    /// <summary>
    /// A function to checkout for items added in the shopping cart.
    /// </summary>
    public void CheckOut()
    {
        //present items in the cart
        PresentShoppingCart();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        //prepare navigation menu to confirm if the user want to buy the items.
        string header = "Are you sure you want to buy these items?";
        List<string> options = new List<string>() { "Yes", "No" };
        int option = Navigation.DisplayNavigation(header, options);
        //counts the total price to pay
        float totalPrice = 0;
        foreach (Item item in _shoppinCartItems)
        {
            totalPrice += item.Price;
        }
        switch (option)
        {
            case 0:
                //if  yes and the credits are greater than or equal to the total price 
                if (_credits >= totalPrice)
                {
                    //subtract the total price from the credits
                    _credits -= totalPrice;
                    //display that the items are ordered
                    Console.WriteLine("Items are now ordered!");
                    //clear the shopping cart.
                    _shoppinCartItems.Clear();
                }
                else
                {
                    //display that there are no sufficent funds
                    Console.WriteLine("Error: Insufficent funds");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
            case 1:
                //if no then cancell payment.
                Console.WriteLine("Payment Cancelled! Press any key to continue...");
                Console.ReadKey();
                break;
        }


    }
    /// <summary>
    /// A function to view or add credits.
    /// </summary>
    public void SeeOrAddCredits()
    {
        //prepare for a navigation menu to view credits and ask if the user wants to add more credits.
        string header = $"You have {_credits}kr available, do you want to add more Credits?";
        List<string> options = new List<string>() { "Yes", "No" };
        int option = Navigation.DisplayNavigation(header, options);

        switch (option)
        {
            case 0:
                //if yes add credits
                AddCredits();
                break;
            case 1:
                //if no cancell
                break;
        }

        /// <summary>
        /// A Handled function to add more Credits to a user account
        /// </summary>
        void AddCredits()
        {   //ask the user for the requiruments to add the ammount
            Console.WriteLine("How much do you want to add?");
            float ammount = CredentialPrompts.HandledReadFloat("Ammount: ");
            //ask the user for his password to confirm the operation
            string inputPassword = CredentialPrompts.HandledReadPassword("Please Enter your password to confirm: ");
            //if the password is correct
            if (Authenticator.HashPassword(inputPassword) == PasswordHash)
            {
                //add the ammount and display the info message
                _credits += ammount;
                Console.WriteLine("Credits are now added, press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                //if the password is wrong, show a message.
                Console.WriteLine("Wrong password! Credits not added, press any key to return...");
                Console.ReadKey();
            }
        }

    }


}