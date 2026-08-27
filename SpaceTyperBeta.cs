using SpaceTyper;
using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static List<string> words = new List<string>
    {
        "SPACE", "LASER", "GALAXY", "PLANET", "ASTEROID", "COMET", "STAR", "UNIVERSE",
        "GRAVITY", "ORBIT", "ROCKET", "BLACKHOLE", "CONSTELLATION", "SUPERNOVA", "METEOR",
        "SATELLITE", "SOLAR", "TELESCOPE", "COSMOS", "INTERSTELLAR", "ASTRONAUT", "PLANETARY"
    };

    static List<FloatingWord> activeWords = new List<FloatingWord>();
    static Random random = new Random();
    static int screenWidth = 50;  // Increased screen width
    static int screenHeight = 20; // Increased screen height
    static int score = 0;
    static int lives = 10;
    static bool gameRunning = true;
    static int gameSpeed = 300;
    static DateTime gameStartTime;
    static int totalCharactersTyped = 0; // Tracks correct characters typed


    static void Main()
    {
        ShowBanner();
        ShowLogo();
        ShowLoadingScreen();
        ShowMainMenu();
    }
    static void ShowBanner()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        string[] asciiArt = new string[]
 {
    "░▒▓█▓▒░░▒▓█▓▒░░▒▓██████▓▒░▒▓████████▓▒░░▒▓█▓▒░░▒▓█▓▒░░▒▓████████▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓███████▓▒░ ",
    "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░    ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░        ",
    "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░    ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░        ",
    "░▒▓████████▓▒░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░    ░▒▓███████▓▒░░▒▓██████▓▒░  ░▒▓██████▓▒░ ░▒▓██████▓▒░  ",
    "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░    ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░         ░▒▓█▓▒░          ░▒▓█▓▒░ ",
    "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▒░    ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░         ░▒▓█▓▒░          ░▒▓█▓▒░ ",
    "░▒▓█▓▒░░▒▓█▓▒░░▒▓██████▓▒░  ░▒▓█▓▒░    ░▒▓█▓▒░░▒▓█▓▒░▒▓████████▓▒░  ░▒▓█▓▒░   ░▒▓███████▓▒░  ",
    "                                                                                                    "
 };
        // Calculate the vertical starting position to center the ASCII art
        int topMargin = (Console.WindowHeight - asciiArt.Length) / 2;
        int leftMargin = (Console.WindowWidth - asciiArt[0].Length) / 2;

        // Add vertical padding
        for (int i = 0; i < topMargin; i++)
        {
            Console.WriteLine();
        }

        // Print each line of ASCII art with horizontal padding
        foreach (string line in asciiArt)
        {
            Console.WriteLine(line.PadLeft(leftMargin + line.Length));
        }

        Thread.Sleep(3000); // Show banner for 3 seconds

    }
    static void ShowLogo()
    {
        Console.Clear();
        string[] asciiArt = new string[]
       { "                                      @@@                 @@@@                                       ",
            "                                     @@@@@               @@@@@                                       ",
            "                                    @@@@@@@@             @@@@@@                                      ",
            "                                    @@@@@@@@@            @@@@@@                                      ",
            "                                    @@@@@@@@@@@          @@@@@@                                      ",
            "                                    @@@@@@@@@@@@         @@@@@@                                      ",
            "                                    @@@@@@ @@@@@@        @@@@@@                                      ",
            "                                    @@@@@@  @@@@@@@      @@@@@@                                      ",
            "                                    @@@@@@    @@@@@@@    @@@@@@                                      ",
            "                                    @@@@@@     @@@@@@@   @@@@@@                                      ",
            "                                    @@@@@@       @@@@@@  @@@@@@                                      ",
            "                    @               @@@@@@        @@@@@@@@@@@@                    @@@@               ",
            "                  @@@@@             @@@@@@       @@@@@@@@@@@@                   @@@@@@@              ",
            "                  @@@@@@            @@@@@@      @@@@@@@@@@@@                   @@@@@@@               ",
            "                   @@@@@@@          @@@@@@     @@@@@  @@@@@@@                @@@@@@@@                ",
            "                   @@@@@@@@         @@@@@@@@@@@@@@@@@@@@@@@@@@@           @@@@@@@@@@@                ",
            "     @@@            @@@@@@@@@     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@       @@@@@@@@@@@                ",
            "    @@@@@@          @@@@@@@@@@  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   @@@@@@@@@@@@           @@@@@ ",
            "     @@@@@@@        @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  @@@@@@@@@@@@@@@ @@@@@@        @@@@@@@@ ",
            "     @@@@@@@@@@     @@@@@@ @@@@@@@@@@@@@ @@@@@@@@@@@@@@@@@@  @@@@@@@@@@@@@   @@@@@      @@@@@@@@@@  ",
            "      @@@@@@@@@@@    @@@@@  @@@@@@@@@@@  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    @@@@@    @@@@@@@@@@@@   ",
            "       @@@@@@@@@@@@  @@@@@  @@@@@@@@        @@@@@@@@@@   @@@@@@   @@@@@@   :@@@@  @@@@@@@@@@@@@@    ",
            "        @@@@@@@@@@@@@@@@@@@ @@@@@@@@@@@@ @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@   @@@@@@@@@@@@@.@@@@@@     ",
            "        @@@@@@ @@@@@@@@@@@@ @@@@@@@@@@@@ @@@@@@@@@@@@@@@@@@  @@@@@@@@@@@  @@@@@@@@@@@@  @@@@@@      ",
            "         @@@@@@  @@@@@@@@@@ @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  @@@@@@@@@@@ @@@@@@@@@@    @@@@@        ",
            "          @@@@@@    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@     @@@@@@         ",
            "           @@@@@     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ @@@       @@@@@@          ",
            "            @@@@@       @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@          @@@@@@           ",
            "            @@@@@@         @@@@@@@@@@@@@@@@@@           @@@@@@@@@@@@@@@@@@        @@@@@@            ",
            "             @@@@@@        @@@@@@@@@@@@@@@@@            @@@@@@@@@@@@@@@@@@       @@@@@@             ",
            "              @@@@@        @@@@@@@@@@@@@@@@              @@@@@@@@@@@@@@@@        @@@@@              ",
            "               @@@@         @@@@@@@@@@@@@@                @@@@@@@@@@@@@@                             ",
            "                               @@@@@@@@@                     @@@@@@@@@                               ",
            "                                                                                                     ",
            "                                                                                                     ",
            "                   @@  @@ @@@   @@ @@@ @@@     @@@@@@    @@   @@@ .@@ @@@@@@ @@@@@@                  ",
            "                  @@@@@@@:@@@@  @@ @@@ @@@    @@@   @@  @@@@  @@@ @@@ @@    @@@                      ",
            "                  @@@@@@@@@@@@@@@@@@@@@@@@    @@@@@@@@  @@@@  @@@@@@@@@@@@@@ @@@@@@                  ",
            "                  @@@@@@@@@@  @@@@@@%@@@@@@   @@@# @@@ @@  @@ @@@@@ @@@@         @@                  ",
            "                  @@ @@ @@@@   @@@@@  @ @@@     @@@@@ @@@  @@@@@ @+ @@@@@@@@ @@@@@@                  "
        };

        // to center the ASCII art
        int topMargin = (Console.WindowHeight - asciiArt.Length) / 2;
        int leftMargin = (Console.WindowWidth - asciiArt[0].Length) / 2;

        // Add vertical padding
        for (int i = 0; i < topMargin; i++)
        {
            Console.WriteLine();
        }

        // Print each line of ASCII art
        foreach (string line in asciiArt)
        {
            Console.WriteLine(line.PadLeft(leftMargin + line.Length));
        }


        Thread.Sleep(3000); // Show banner for 3 seconds
        Console.Clear();
    }

    static void ShowLoadingScreen()
    {
        Console.Clear();

        int width = 30;
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;

        int barLeft = (consoleWidth - width) / 2;
        int barTop = consoleHeight / 2;

        Console.SetCursorPosition((consoleWidth - 15) / 2, barTop - 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Loading...");

        for (int i = 0; i <= width; i++)
        {
            Console.SetCursorPosition(barLeft, barTop);
            Console.Write("[" + new string('■', i) + new string(' ', width - i) + "]");
            Console.ForegroundColor = ConsoleColor.Red;
            Thread.Sleep(50);
        }

        Thread.Sleep(500);
        Console.Clear();
    }

    static void ShowMainMenu()
    {
        int selectedIndex = 0; // Index of the currently selected menu item
        Console.ForegroundColor = ConsoleColor.Red;
        string[] menu = new string[]
        {
        "   *************************************",
        "      )    )               )         ) (     ",
        "    ( /( ( /(   *   )    ( /(      ( /( )\\ )  ",
        "    )\\()))\\())` )  /(    )\\())(    )\\()|()/(  ",
        "   ((_\\((_\\)  ( )(_)) |((_\\ )\\  ((_\\ /(_)) ",
        "    _((_) ((_)(_(_())  |_ ((_|(_)__ ((_|_))   ",
        "   | || |/ _ \\|_   _|  | |/ /| __\\ \\ / / __|  ",
        "   | __ | (_) | | |      ' < | _| \\ V /\\__ \\  ",
        "   |_||_|\\___/  |_|     _|\\_\\|___| |_| |___/   ",
        "   *************************************",
        };

        string[] options = { "Space Typer", "(Coming Soon!)", "Exit" };

        while (true)
        {
            Console.Clear();

            // Center header text vertically
            int headerStartY = (Console.WindowHeight - (menu.Length + options.Length + 2)) / 2;
            int headerStartX = (Console.WindowWidth - menu[0].Length) / 2;

            // Display the header menu centered
            foreach (var line in menu)
            {
                Console.SetCursorPosition(headerStartX, headerStartY++);
                Console.WriteLine(line);
            }

            // Center the options menu
            int optionsStartY = headerStartY + 2; // Add spacing below header
            foreach (var option in options)
            {
                Console.SetCursorPosition((Console.WindowWidth - options[0].Length) / 2, optionsStartY++);
            }

            // Display options with highlight for selected index
            for (int i = 0; i < options.Length; i++)
            {
                int leftMargin = (Console.WindowWidth - options[i].Length) / 2;
                Console.SetCursorPosition(leftMargin, headerStartY + 2 + i);
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Highlight color
                    Console.WriteLine($"> {options[i]} <");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray; // Normal color
                    Console.WriteLine($"  {options[i]}  ");
                }
            }

            // Reset cursor color
            Console.ForegroundColor = ConsoleColor.Red;

            // Handle user input
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex - 1 + options.Length) % options.Length; // Wrap around to the last option
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex + 1) % options.Length; // Wrap around to the first option
                    break;
                case ConsoleKey.Enter:
                    // Execute the selected option
                    if (selectedIndex == 0)
                    {
                        PlaySpaceTyper();
                    }
                    else if (selectedIndex == 2) // Exit option
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("This option is not yet implemented. Press any key to continue...");
                        Console.ReadKey();
                    }
                    break;
            }
        }
    }

    static void CenterDisplay(string[] content)
    {
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;

        int topMargin = (consoleHeight - content.Length) / 2;

        for (int i = 0; i < topMargin; i++)
        {
            Console.WriteLine();
        }

        foreach (string line in content)
        {
            int leftMargin = (consoleWidth - line.Length) / 2;
            Console.WriteLine(line.PadLeft(leftMargin + line.Length));
        }
    }

    static void PlaySpaceTyper()
    {
        ShowTitleScreen();
        SelectLevel();

        InitializeGame();

        GameLoop();

        ShowGameOverScreen();
    }

    static void ShowTitleScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        string[] title = new string[]
        {
        "*************************************",
        "             SPACE TYPER             ",
        "*************************************",
        "",
        "RULES:",
        "1. Words will float around on the screen.",
        "2. Your goal is to type each word as quickly as possible.",
        "3. You earn points based on speed and accuracy:",
        "   - Fast and accurate typing earns more points.",
        "   - Errors will reduce your life.",
        "4. Theere's a 4 difficulties, select 1 based on your skill.",
        "5. Press Enter to start when you're ready.",
        "",
        "Good luck, Space Typer!"
        };

        CenterDisplay(title);
        Console.ReadKey();
    }


    static void SelectLevel()
    {
        ConsoleKey key;
        int selectedIndex = 0;
        string[] options = new string[]
        {
        "Beginner (10 lives)",
        "Intermediate (8 lives)",
        "Advanced (5 lives)",
        "Expert (3 lives)",
        "Back to Main Menu"
        };

        while (true)
        {
            Console.Clear();
            int screenHeight = Console.WindowHeight;
            int verticalPadding = (screenHeight - (options.Length + 2)) / 2; // Calculate vertical padding
            string title = "Select Difficulty Level:";

            // Add vertical padding to center the menu on the screen
            Console.WriteLine(new string('\n', verticalPadding));
            CenterText(title);
            Console.WriteLine();

            // Display menu options with the selected option highlighted
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Highlight selected option
                    CenterText($"> {options[i]}");
                    Console.ResetColor();
                }
                else
                {
                    CenterText($"  {options[i]}");
                }
            }

            // Read user input for navigation
            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == options.Length - 1) ? 0 : selectedIndex + 1;
                    break;
                case ConsoleKey.Enter:
                    HandleSelection(selectedIndex);
                    return;
            }
        }
    }

    // Centers text based on the console width
    static void CenterText(string text)
    {
        int screenWidth = Console.WindowWidth;
        int textWidth = text.Length;
        int padding = (screenWidth - textWidth) / 2;
        Console.WriteLine(new string(' ', padding) + text);
    }

    // Handles selection of difficulty level
    static void HandleSelection(int selectedIndex)
    {
        switch (selectedIndex)
        {
            case 0:
                lives = 10;
                gameSpeed = 300;
                Console.WriteLine("Beginner level selected!");
                break;
            case 1:
                lives = 8;
                gameSpeed = 250;
                Console.WriteLine("Intermediate level selected!");
                break;
            case 2:
                lives = 5;
                gameSpeed = 200;
                Console.WriteLine("Advanced level selected!");
                break;
            case 3:
                lives = 3;
                gameSpeed = 150;
                Console.WriteLine("Expert level selected!");
                break;
            case 4:
                Console.WriteLine("Returning to main menu...");
                ShowMainMenu();
                break;
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

        static void InitializeGame()
        {
            activeWords.Clear();
            score = 0;
            totalCharactersTyped = 0; // Reset character count
            gameStartTime = DateTime.Now; // Record start time
            gameRunning = true;
        }

   
    static void GameLoop()
    {
        while (gameRunning)
        {
            // Spawn words if fewer than 5 are on the screen
            if (activeWords.Count < 5)
            {
                SpawnWord();
            }

            // Update word positions
            foreach (var word in activeWords)
            {
                word.MoveRandomly(screenWidth, screenHeight);
            }

            // Update the display
            DisplayGame();

            // Check for user input
            if (Console.KeyAvailable)
            {
                Console.SetCursorPosition(0, screenHeight + 2); // Position for user input
                Console.Write("Enter word: ");
                string input = Console.ReadLine()?.ToUpper();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    ProcessInput(input);
                }
            }

            // Pause the game for a short duration
            Thread.Sleep(gameSpeed);

            // Check for game over condition
            if (lives <= 0)
            {
                gameRunning = false;
            }
        }
    }

    static void SpawnWord()
    {
        string word = words[random.Next(words.Count)];
        int xPos = random.Next(1, screenWidth - word.Length); // Spawn within horizontal bounds
        int yPos = random.Next(1, screenHeight); // Spawn within vertical bounds
        activeWords.Add(new FloatingWord(word, xPos, yPos));
    }

    static void ProcessInput(string input)
    {
        bool matched = false;

        for (int i = activeWords.Count - 1; i >= 0; i--)
        {
            if (activeWords[i].Word == input)
            {
                totalCharactersTyped += input.Length; // Count correct characters
                score += input.Length * 10; // Update score
                activeWords.RemoveAt(i);
                matched = true;
                break;
            }
        }

        if (!matched)
        {
            lives--;
            Console.SetCursorPosition(0, screenHeight + 3);
            Console.WriteLine($"Misspelled! Lives remaining: {lives}");
        }
    }

    static int CalculateWPM()
    {
        TimeSpan elapsedTime = DateTime.Now - gameStartTime;
        double minutesElapsed = elapsedTime.TotalMinutes;
        return minutesElapsed > 0 ? (int)(totalCharactersTyped / 5 / minutesElapsed) : 0;
    }


    static void DisplayGame()
    {
        Console.Clear();
        int centerX = (Console.WindowWidth - screenWidth) / 2;
        int centerY = (Console.WindowHeight - screenHeight) / 2;
        DrawBorder();
        Console.SetCursorPosition(centerX, centerY - 1);

        int wpm = CalculateWPM(); // Get current WPM
        Console.WriteLine($"Score: {score}   Lives Left: {lives}   WPM: {wpm}");

        foreach (var word in activeWords)
        {
            Console.SetCursorPosition(centerX + word.X, centerY + word.Y);
            Console.Write(word.Word);
        }

        Console.SetCursorPosition(centerX, centerY + screenHeight + 1);
    }

    static void DrawBorder()
    {
        // Get console dimensions
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;

        // Determine where the border starts and ends
        int topMargin = (consoleHeight - screenHeight - 2) / 2;
        int leftMargin = (consoleWidth - screenWidth - 2) / 2;

        // Draw the top border
        Console.SetCursorPosition(leftMargin, topMargin);
        Console.Write("+" + new string('-', screenWidth) + "+");

        // Draw the sides
        for (int i = 0; i < screenHeight; i++)
        {
            Console.SetCursorPosition(leftMargin, topMargin + i + 1); // Move to the next line within the border
            Console.Write("|" + new string(' ', screenWidth) + "|");
        }

        // Draw the bottom border
        Console.SetCursorPosition(leftMargin, topMargin + screenHeight + 1);
        Console.Write("+" + new string('-', screenWidth) + "+");
    }

    static void ShowGameOverScreen()
    {
        Console.Clear();
        int finalWPM = CalculateWPM(); // Calculate final WPM

        // ASCII art for the game over screen
        Console.ForegroundColor = ConsoleColor.Red;
        string[] gameOverArt = new string[]
        {
        " ░▒▓██████▓▒░ ░▒▓██████▓▒░░▒▓██████████████▓▒░░▒▓████████▓▒░       ░▒▓██████▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓████████▓▒░▒▓███████▓▒░  ",
        "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░             ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░ ",
        "░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░             ░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░ ",
        "░▒▓█▓▒▒▓███▓▒░▒▓████████▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓██████▓▒░        ░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒▒▓█▓▒░░▒▓██████▓▒░ ░▒▓███████▓▒░  ",
        "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░             ░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▓█▓▒░ ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░ ",
        "░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░             ░▒▓█▓▒░░▒▓█▓▒░ ░▒▓█▓▓█▓▒░ ░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░ ",
        " ░▒▓██████▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░░▒▓█▓▒░▒▓████████▓▒░       ░▒▓██████▓▒░   ░▒▓██▓▒░  ░▒▓████████▓▒░▒▓█▓▒░░▒▓█▓▒░ ",
        "                                                                                                                         ",
        $"Final Score: {score}   Final WPM: {finalWPM}",
        "Thank you for playing!",
        "Press any key to return to the main menu."

        };
        Console.ForegroundColor = ConsoleColor.White;
        // Get console dimensions
        int consoleWidth = Console.WindowWidth;
        int consoleHeight = Console.WindowHeight;

        // Calculate vertical centering
        int topMargin = (consoleHeight - gameOverArt.Length) / 2;

        // Print top margin
        for (int i = 0; i < topMargin; i++)
        {
            Console.WriteLine();
        }

        // Print each line of ASCII art, centered horizontally
        foreach (string line in gameOverArt)
        {
            int leftMargin = (consoleWidth - line.Length) / 2;
            Console.WriteLine(line.PadLeft(leftMargin + line.Length));
        }

        // Wait for user to press a key
        Console.ReadKey();
        ShowMainMenu();
    }

   
}