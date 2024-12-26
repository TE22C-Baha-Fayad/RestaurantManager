using System.Text.Json.Serialization;

class Item
{
    public string Name {get; set;}
    public string Description {get; set;}
    public float Price{get;set;}
    /// <summary>
    /// a constructor to build an item object
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="price"></param>
    [JsonConstructor]
    public Item(string name, string description, float price)
    {
        this.Name = name;
        this.Description = description;
        this.Price = price;
    }
    /// <summary>
    /// display information about the items properties
    /// </summary> <summary>
    /// 
    /// </summary>
    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Cost: {Price}kr");
        Console.WriteLine("-------------------------------------");
    }
    /// <summary>
    /// gets the info about the item in a form of string
    /// </summary>
    /// <returns>string item information</returns>
    public string GetInfo()
    {
        string content = $"Name: {Name} | Cost: {Price}kr | Description: {Description}|";
        return content;
    }

}