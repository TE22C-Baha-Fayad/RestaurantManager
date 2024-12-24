

using System.Runtime.Serialization;

class Account
{
    public string username;
    public string passwordHash;
    public bool Login(Database database)
    {
        bool userFound = Authenticator.ValidateUser(database.accountData, username, passwordHash);
        return userFound;
    }
}
class Admin : Account
{
    public Admin(string username, string password)
    {
        this.username = username;
        base.passwordHash = password;
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
        string itemName = Console.ReadLine();
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
                    item.cost = CredentialPrompts.HandledReadFloat("Item Cost:");
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

    }
}
class User : Account
{
    private float credits = 1000;
    public User(string username, string password)
    {
        this.username = username;
        base.passwordHash = password;
    }
    public static void Register(string username, string password, Database database)
    {
        Admin user = new Admin(username, password);
        database.SaveUser(user);
    }

}