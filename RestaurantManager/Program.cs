
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
            }
            catch(FileNotFoundException)
            {
                //if the file isn't found create a new database instance.
                database = new Database();
            }
            catch(JsonException)
            {
                //if the database file is corrupted or empty, create a new database and overwrite
               database = new Database();
            }
            //save the new database if changed.
            new DataSerializer<Database>(database).SaveData();
            //Display the mainmenu for data base of the restaurant
            UserInterface.DisplayMainMenu(database);
        }
    }
}