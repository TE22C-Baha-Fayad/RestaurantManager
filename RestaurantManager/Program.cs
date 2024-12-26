
using System.Text.Json;

namespace RestaurantManager
{
    internal class Program
    {
        static void Main(string[] args)
        {   //declare a database
            Database database;

            try{//try get data from the data.json file and set database to the loaded data
                Database loadedData = DataSerializer<Database>.GetData();
                database = loadedData;
                database.AccountData = DataSerializer.AccountListConverter(loadedData.AccountData);
            }
            catch(Exception)
            {
                //if there are any exceptions then create a new data base and overwrite
                database = new Database();
            }
            //save the new database if changed.
            new DataSerializer<Database>(database).SaveData();
            //Display the mainmenu for data base of the restaurant
            UserInterface.DisplayMainMenu(database);
        }
    }
}