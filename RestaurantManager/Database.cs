using System.Net.Http.Headers;
using System.Text.Json.Serialization;
/// <summary>
/// A class that contains general Database information about the restaurant
/// </summary>
class Database
{

    // create a property for accountData
    public List<Account> AccountData { get; set; } = new List<Account>() { new Admin("12345678", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f"), new User("Baha", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f") };
    //create a property for published items
    public List<Item> Items { get; set; } = new List<Item>() { new Item("kebab pizza", "A tasty pizza with kebab.", 129.99f) };

    /// <summary>
    /// saves an account in the accountlist
    /// </summary>
    /// <param name="account"></param> <summary>
    /// 
    /// </summary>
    /// <param name="account"></param>
    public void SaveUser(Account account)
    {
        AccountData.Add(account);
    }
    /// <summary>
    /// Gets a user from the accountlist by username
    /// </summary>
    /// <param name="username"></param>
    /// <returns>account</returns>
    public Account GetUser(string username)
    {
        foreach (Account account in AccountData)
        {
            // if account username match the username from the parameter
            if (account.Username == username)
            {
                //return the account
                return account;
            }
        }
        //if account isn't found return null
        return null;
    }
    /// <summary>
    /// gets an item by index
    /// </summary>
    /// <param name="index"></param>
    /// <returns>item</returns>
    public Item GetItemByIndex(int index)
    {
        //return the item
        return Items[index];
    }
    /// <summary>
    /// gets an account by index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Account GetUserByIndex(int index)
    {
        //retrun the account
        return AccountData[index];
    }

}