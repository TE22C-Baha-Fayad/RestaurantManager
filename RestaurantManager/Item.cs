class Item
{
    public string name;
    public string description;
    public float cost;

    public Item(string name, string description, float cost)
    {
        this.name = name;
        this.description = description;
        this.cost = cost;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Cost: {cost}kr");
        Console.WriteLine("-------------------------------------");

    }

}