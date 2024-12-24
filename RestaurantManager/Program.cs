
namespace RestaurantManager
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            Database database = new Database();
            UserInterface.DisplayMainMenu(database);
        }
    }
}