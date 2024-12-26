using System.Net.Http.Headers;
using System.Text.Json.Serialization;
/// <summary>
/// A class that contains general Database information about the restaurant
/// </summary>
class Database
{
  
    public List<Account> AccountData { get; set; } = new List<Account>() { new Admin("12345678", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f"), new User("Baha", "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f") };
    public List<Item> Items { get; set; } = new List<Item>() { new Item("kebab pizza", "A tasty pizza with kebab.", 129.99f) };

    public void SaveUser(Account user)
    {
        AccountData.Add(user);
    }
    public Account GetUser(string username)
    {
        foreach (Account account in AccountData)
        {
            if (account.Username == username)
            {
                return account;
            }
        }
        return null;
    }
    public Item GetItemByIndex(int index)
    {
        return Items[index];
    }

}