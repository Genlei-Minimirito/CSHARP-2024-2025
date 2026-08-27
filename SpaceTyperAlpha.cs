using System;
using System.Collections.Generic;
using System.Threading;

class HotKeys
{
    static List<string> words = new List<string>
    {
        "SPACE", "LASER", "GALAXY", "PLANET", "ASTEROID", "COMET", "STAR", "UNIVERSE",
        "GRAVITY", "ORBIT", "ROCKET", "BLACKHOLE", "CONSTELLATION", "SUPERNOVA", "METEOR",
        "SATELLITE", "SOLAR", "TELESCOPE", "COSMOS", "INTERSTELLAR", "ASTRONAUT", "PLANETARY"
    };

    static List<FallingWord> activeWords = new List<FallingWord>();
    static Random random = new Random();
    static int screenWidth = 30; // Increased screen width
    static int screenHeight = 15; // Increased screen height
    static int spaceshipPosition = screenWidth / 2;
    static int score = 0;
    static int lives = 3;
    static bool gameRunning = true;

    static void Main()
    {
        Console.WriteLine("Welcome to the Space Shooter Typing Game!");
        Console.WriteLine("Type the words before they reach the spaceship to destroy them.");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey();

        // Start game loop
        while (gameRunning)
        {
            Console.Clear();
            SpawnWord();
            UpdateWords();
            DisplayGame();

            if (Console.KeyAvailable)
            {
                string input = Console.ReadLine().ToUpper();
                ProcessInput(input);
            }

            Thread.Sleep(500);  // Reduced delay to make it faster

            if (lives <= 0)
            {
                gameRunning = false;
            }
        }

        Console.Clear();
        Console.WriteLine("Game Over!");
        Console.WriteLine($"Final Score: {score}");
        Console.WriteLine("Thanks for playing!");
    }

    static void SpawnWord()
    {
        // Spawn a new word if there are less than 5 active words on the screen
        if (activeWords.Count < 5)
        {
            string word = words[random.Next(words.Count)];
            int xPos = random.Next(0, screenWidth - word.Length);
            activeWords.Add(new FallingWord(word, xPos, 0));
        }
    }

    static void UpdateWords()
    {
        // Move each active word down the screen
        for (int i = activeWords.Count - 1; i >= 0; i--)
        {
            activeWords[i].Y++;

            // If a word reaches the spaceship's row, player loses a life
            if (activeWords[i].Y >= screenHeight - 1)
            {
                activeWords.RemoveAt(i);
                lives--;
            }
        }
    }

    static void DisplayGame()
    {
        // Display the score and lives
        Console.WriteLine($"Score: {score} | Lives: {lives}");
        Console.WriteLine("Type the words to shoot them before they reach the spaceship!");

        // Create a grid to display the game area
        char[,] screen = new char[screenHeight, screenWidth];

        // Place each falling word on the screen
        foreach (var word in activeWords)
        {
            for (int i = 0; i < word.Word.Length; i++)
            {
                int x = word.X + i;
                if (x < screenWidth && word.Y < screenHeight)
                {
                    screen[word.Y, x] = word.Word[i];
                }
            }
        }

        // Display the game area
        for (int y = 0; y < screenHeight; y++)
        {
            for (int x = 0; x < screenWidth; x++)
            {
                // Display spaceship at the bottom
                if (y == screenHeight - 1 && x == spaceshipPosition)
                    Console.Write("<=>");  // Spaceship symbol
                else
                    Console.Write(screen[y, x] == '\0' ? ' ' : screen[y, x]);
            }
            Console.WriteLine();
        }
    }

    static void ProcessInput(string input)
    {
        for (int i = activeWords.Count - 1; i >= 0; i--)
        {
            if (activeWords[i].Word == input)
            {
                score += activeWords[i].Word.Length * 10;
                Console.WriteLine($"Shot down '{input}'!");
                activeWords.RemoveAt(i);
                break;
            }
        }
    }
}

class FallingWord
{
    public string Word;
    public int X;
    public int Y;

    public FallingWord(string word, int x, int y)
    {
        Word = word;
        X = x;
        Y = y;
    }
}
