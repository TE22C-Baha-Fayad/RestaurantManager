

using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

class Account
{
    public string username;
    public string passwordHash;

    public static Account Login(string username, string passwordTxt, Database database)
    {
       
        Account account = Authenticator.ValidateUser(database.accountData, username, passwordTxt);
        if (account != null)
        {
            return account;
        }
        return null;
    }
}
class Admin : Account
{
    public Admin(string username, string password)
    {
        this.username = username;
        passwordHash = password;
    }
    public static void Register(string username, string password, Database database)
    {
        Admin user = new Admin(username, password);
        database.SaveUser(user);
    }
    public void ViewItems(Database database)
    {
        for (int i = 0; i < database.items.Count; i++)
        {
            database.items[i].DisplayInfo();
        }
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();

    }
    public void ListNewItem(Database database)
    {
        Console.Write("Item name:");
        string itemName;
        while (true)
        {
            itemName = Console.ReadLine();
            if (itemName == "")
            {
                Console.WriteLine("This field can't be empty");
            }
            else
            {
                break;
            }
        }
        Console.Write("Item Description:");
        string itemDescription = Console.ReadLine();
        float itemCost = CredentialPrompts.HandledReadFloat("Item Cost:");
        Item item = new Item(itemName, itemDescription, itemCost);
        database.items.Add(item);
    }
    public void EditItem(Database database)
    {
        while (true)
        {
            string header = "Choose An Item Navigate Using (Up&Down) Arrows And Press (Enter) To Confirm:";
            List<string> options = new List<string>();
            for (int i = 0; i < database.items.Count; i++)
            {
                options.Add(database.items[i].name);
            }
            int option = Navigation.DisplayNavigation(header, options);
            Item item = database.GetItemByIndex(option);
            Console.WriteLine("The item has the following info:");
            item.DisplayInfo();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            header = "Choose A Property Navigate Using (Up&Down) Arrows And Press (Enter) To Confirm:";
            options = new List<string>() { "Item Name", "Item Description", "Item Cost", "Return back" };
            option = Navigation.DisplayNavigation(header, options);


            switch (option)
            {
                case 0:
                    Console.Write("Item Name:");
                    item.name = Console.ReadLine();
                    Console.WriteLine("Item name changed successfully! Press any key to conitnue...");
                    Console.ReadKey();

                    return;
                case 1:
                    Console.Write("Item Description:");
                    item.description = Console.ReadLine();
                    Console.WriteLine("Item Description changed successfully! Press any key to conitnue...");
                    Console.ReadKey();
                    return;
                case 2:
                    item.price = CredentialPrompts.HandledReadFloat("Item Cost:");
                    Console.WriteLine("Item Cost changed successfully! Press any key to conitnue...");
                    Console.ReadKey();
                    return;
                case 3:
                    return;
            }

        }

    }
    public void RemoveItem(Database database)
    {

        string header = "Choose An Item Navigate Using (Up&Down) Arrows And Press (Enter) To Confirm:";
        List<string> options = new List<string>();
        for (int i = 0; i < database.items.Count; i++)
        {
            options.Add(database.items[i].name);
        }
        int option = Navigation.DisplayNavigation(header, options);
        Item item = database.GetItemByIndex(option);
        database.items.Remove(item);
        Console.WriteLine("Item is now removed! Press any key to conitnue...");
        Console.ReadKey();

    }
}
class User : Account
{
    private List<Item> ShoppinCartItems = new List<Item>();
    private float credits = 1000;
    public User(string username, string password)
    {
        this.username = username;
        base.passwordHash = password;
    }
    public static void Register(string username, string password, Database database)
    {
        User user = new User(username, password);
        database.SaveUser(user);
    }
    public void PresentShoppingCart()
    {
        Console.WriteLine($"There are {ShoppinCartItems.Count} Items in your shopping cart.");
        float totalPrice = 0;
        foreach (Item item in ShoppinCartItems)
        {
            totalPrice += item.price;
        }
        Console.WriteLine($"Total Price: {totalPrice}kr");
        Console.WriteLine($"Your Credits: {credits}kr");
        Console.WriteLine();
        Console.WriteLine("Items:");
        for (int i = 0; i < ShoppinCartItems.Count; i++)
        {
            ShoppinCartItems[i].DisplayInfo();
        }
    }
    public void BrowseItems(Database database)
    {
        string header = $"Available Items:";
        List<string> options = new List<string>();
        for (int i = 0; i < database.items.Count; i++)
        {
            options.Add(database.items[i].GetInfo());
        }
        int option = Navigation.DisplayNavigation(header, options);
        ShoppinCartItems.Add(database.GetItemByIndex(option));
        Console.WriteLine("Item were added to your shopping cart, press any key to continue...");
        Console.ReadKey();
    }
    public void ViewCartItems()
    {
        PresentShoppingCart();
        Console.WriteLine("press any key to continue...");
        Console.ReadKey();
    }
    public void RemoveCartItem()
    {
        if (ShoppinCartItems.Count > 0)
        {
            string header = "Which item do you want to remove?";
            List<string> options = new List<string>();
            for (int i = 0; i < ShoppinCartItems.Count; i++)
            {
                options.Add(ShoppinCartItems[i].GetInfo());
            }
            int option = Navigation.DisplayNavigation(header, options);
            ShoppinCartItems.RemoveAt(option);
            Console.WriteLine("Item is now removed from your shopping cart, press any key to continue...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine($"There are {ShoppinCartItems.Count} Items in the shopping cart.");
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
        }
    }
    public void CheckOut()
    {
        // TODO: group same items ex: 5x pizza, 3x hamburger.
        string header = "Are you sure you want to buy these items?";
        List<string> options = new List<string>() { "Yes", "No" };
        int option = Navigation.DisplayNavigation(header, options,1,1,PresentShoppingCart);
        float totalPrice = 0;
        foreach (Item item in ShoppinCartItems)
        {
            totalPrice += item.price;
        }
        switch (option)
        {
            case 0:
                if (credits >= totalPrice)
                {
                    credits -= totalPrice;
                    Console.WriteLine("Items are now ordered!");
                    ShoppinCartItems.Clear();
                }
                else
                {
                    Console.WriteLine("Error: Insufficent funds");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                break;
                case 1:
                Console.WriteLine("Payment Cancelled! Press any key to continue...");
                Console.ReadKey();
                break;
        }


    }


}