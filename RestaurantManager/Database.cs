using System.Net.Http.Headers;

class Database{
    public List<Account> accountData = new List<Account>();
    public List<Item> items = new List<Item>();

    public void SaveUser(Account user){
        accountData.Add(user);
    }
    public Account GetUser(string username)
    {
        foreach(Account account in accountData)
        {
            if(account.username == username){
                return account;
            }
        }
        return null;
    }
    public Item GetItemByIndex(int index)
    {
       return items[index];
    }

}