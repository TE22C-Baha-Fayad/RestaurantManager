class Navigation
{
    public static int DisplayNavigation(string header, List<string> Options, int innitialCursorY = 1, int jumpsteps = 1)
    {
        Console.CursorVisible = false;
        int lastcursorYPosition = innitialCursorY;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(header);
            foreach (string option in Options)
            {
                Console.WriteLine($"  {option}");
            }
            Console.SetCursorPosition(0, lastcursorYPosition);
            string navigationCharacter = "=>";
            Console.Write(navigationCharacter);
            ConsoleKey key = Console.ReadKey().Key;

            if (key == ConsoleKey.UpArrow && lastcursorYPosition > innitialCursorY)
            {
                lastcursorYPosition -= jumpsteps;

            }
            else if (key == ConsoleKey.DownArrow && lastcursorYPosition < innitialCursorY + Options.Count - 1)
            {

                lastcursorYPosition += jumpsteps;

            }
            else if (key == ConsoleKey.Enter)
            {
                int currnetCursorY = Console.GetCursorPosition().Top;
                for (int i = 0; i < Options.Count; i++)
                {
                    if (currnetCursorY == i + innitialCursorY)
                    {
                        Console.Clear();
                        return i;
                    }
                }
            }
        }
    }
}