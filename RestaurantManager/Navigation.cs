/// <summary>
/// A navigation class to manage input
/// </summary>
class Navigation
{
    /// <summary>
    /// Prints A navigation menu with a header and options to let the user pick from 
    /// </summary>
    /// <param name="header"></param>
    /// <param name="Options"></param>
    /// <param name="innitialCursorY"></param>
    /// <param name="jumpsteps"></param>
    /// <returns>the index of the option</returns>
    public static int DisplayNavigation(string header, List<string> Options, int innitialCursorY = 1, int jumpsteps = 1)
    {
        Console.CursorVisible = false;
        //create a variable to store the cursorposition.
        int cursorYPosition = innitialCursorY;
        while (true)
        {
            Console.Clear();
            //write the header or the title
            Console.WriteLine(header);
            //display every option in the option list.
            foreach (string option in Options)
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
            else if (key == ConsoleKey.DownArrow && cursorYPosition < innitialCursorY + Options.Count - 1)
            {
                //adds jumpsteps to navigate downwards
                cursorYPosition += jumpsteps;

            }//if key is enter
            else if (key == ConsoleKey.Enter)
            {
                //define cursor y position
                int currnetCursorY = Console.GetCursorPosition().Top;
                //loop through each option
                for (int i = 0; i < Options.Count; i++)
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
}