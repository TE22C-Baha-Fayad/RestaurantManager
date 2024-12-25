class Item
{
    public string name;
    public string description;
    public float price;

    public Item(string name, string description, float cost)
    {
        this.name = name;
        this.description = description;
        this.price = cost;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Cost: {price}kr");
        Console.WriteLine("-------------------------------------");
    }
    public string GetInfo()
    {
        string content = $"Name: {name} | Cost: {price}kr | Description: {description}|";
        return content;
    }

}