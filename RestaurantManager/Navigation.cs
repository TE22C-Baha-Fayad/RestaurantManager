/// <summary>
/// A navigation class to manage input
/// </summary>
class Navigation
{
    /// <summary>
    /// Prints A navigation menu with a header and options to let the user pick from 
    /// </summary>
    /// <param name="header"></param>
    /// <param name="options"></param>
    /// <param name="innitialCursorY"></param>
    /// <param name="jumpsteps"></param>
    /// <returns>the index of the option</returns>
    public static int DisplayNavigation(string header, List<string> options,string instruction="Pick a choice and Navigate Using (Up&Down) Arrows And Press (Enter) To Confirm:", int innitialCursorY = 2, int jumpsteps = 1)
    {
        Console.CursorVisible = false;
        //create a variable to store the cursorposition.
        int cursorYPosition = innitialCursorY;
        while (true)
        {
            Console.Clear();
            //write the header or the title
            Console.WriteLine(header);
        
            //Print instruction
            Console.WriteLine(instruction);
            //display every option in the option list.
            foreach (string option in options)
            {
                Console.WriteLine($"  {option}");
            }
            //sets the cursor Y position to the cursor position
            Console.SetCursorPosition(0, cursorYPosition);
            //create a variable for the navigation character
            string navigationCharacter = "=>";
            //write the character 
            Console.Write(navigationCharacter);
            //read pressed key
            ConsoleKey key = Console.ReadKey().Key;
            //if key is up and the cursor position is 
            if (key == ConsoleKey.UpArrow && cursorYPosition > innitialCursorY)
            {
                //subtract jumpsteps to navigate upwards
                cursorYPosition -= jumpsteps;

            }
            else if (key == ConsoleKey.DownArrow && cursorYPosition < innitialCursorY + options.Count - 1)
            {
                //adds jumpsteps to navigate downwards
                cursorYPosition += jumpsteps;

            }//if key is enter
            else if (key == ConsoleKey.Enter)
            {
                //define cursor y position
                int currnetCursorY = Console.GetCursorPosition().Top;
                //loop through each option
                for (int i = 0; i < options.Count; i++)
                {
                    if (currnetCursorY == i + innitialCursorY)
                    {
                        //return the option idex
                        Console.Clear();
                        return i;
                    }
                }
            }
        }
    }
    /// <summary>
    /// confirms Logout and return option
    /// </summary>
    /// <returns>option: Yes = 0 and No = 1</returns>
    public static int ConfirmLogOut()
    {
        string header = $"Are you sure you want to Logout? ";
        List<string> options = new List<string>() { "Yes", "No" };
        //create navigation menu to confirm choice
        int option = Navigation.DisplayNavigation(header, options);
        return option;
    }
    /// <summary>
    /// Checks and handles if item list is empty in the database
    /// </summary>
    /// <param name="database"></param>
    /// <returns>True if item list is empty and False if not</returns>
    public static bool CheckAndHandleListEmpty<T>(List<T> values, string message)
    {
        //check if the item list is empty 
        if (values.Count == 0)
        {
            //display info message
            Console.WriteLine(message + ", Press any key to continue...");
            Console.ReadKey();
            return true;
        }
        return false;
    }
}