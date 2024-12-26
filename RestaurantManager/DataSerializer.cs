
using System.Text.Json;
/// <summary>
/// A simple class to save and load data using json
/// </summary>
/// <typeparam name="T"></typeparam>
class DataSerializer<T>
{
    //decleare data of generic type
    private T _data;
    //create a constant for the name of the file
    private const string FileName = "\\data.json";
    //create a constructor for the file to set the data
    public DataSerializer(T data)
    {
        _data = data;
    }

    /// <summary>
    /// saves the data into data.json file
    /// </summary> <summary>
    /// 
    /// </summary>
    public void SaveData()
    {
        string serializedData = JsonSerializer.Serialize<T>(_data);
        File.WriteAllText(Directory.GetCurrentDirectory() + FileName, serializedData);

    }
    /// <summary>
    /// gets the data from the data.json file
    /// </summary>
    /// <returns>data</returns>
    public static T GetData()
    {
        string jsonData = File.ReadAllText(Directory.GetCurrentDirectory() + FileName);
        T data = JsonSerializer.Deserialize<T>(jsonData);
        return data;
    }
}
/// <summary>
/// DataSerializer class with handful Functions regarding serialization operation.
/// </summary>
class DataSerializer
{
    /// <summary>
    /// Filters the accounts in a list and converts to either Admin or user account based on the IsAdmin property.
    /// </summary>
    /// <param name="accounts"></param>
    /// <returns>The converted list</returns> <summary>
    /// 
    /// </summary>
    /// <param name="accounts"></param>
    /// <returns></returns>
    public static List<Account> AccountListConverter(List<Account> accounts)
    {
        //create a list 
        List<Account> convertedAccounts = new List<Account>();
        
        foreach (Account account in accounts)
        {
            //if the account is marked as admin
            if (account.IsAdmin)
            {
                //create a new admin account and add it to the converted list
                Admin admin = new(account.Username, account.PasswordHash);
                convertedAccounts.Add(admin);
            }
            else
            {
                //create a new user account and add it to the converted list
                User user = new(account.Username, account.PasswordHash) ;
                convertedAccounts.Add(user);
            }
        }
        return convertedAccounts;
    }

}