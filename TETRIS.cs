using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace TetrisConsole
{
    class Program
    {
        private const int Width = 10;
        private const int Height = 20;
        private static int[,] grid = new int[Height, Width];
        private static List<int[,]> tetrominoes = new List<int[,]>
        {
            new int[,] {{1, 1, 1, 1}},                   // I
            new int[,] {{1, 1}, {1, 1}},                 // O
            new int[,] {{0, 1, 1}, {1, 1, 0}},           // S
            new int[,] {{1, 1, 0}, {0, 1, 1}},           // Z
            new int[,] {{1, 1, 1}, {0, 1, 0}},           // T
            new int[,] {{1, 1, 1}, {1, 0, 0}},           // L
            new int[,] {{1, 1, 1}, {0, 0, 1}},           // J
        };
        private static int score = 0;
        private static int currentX = Width / 2 - 1;
        private static double currentY = 0;
        private static int[,] currentTetromino;
        private static int[,] nextTetromino;
        private static ConsoleColor currentColor;
        private static ConsoleColor nextColor;
        private static bool gameOver = false;
        private const string LeaderboardFile = "leaderboard.txt";

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            while (true)
            {
                ShowStartMenu();
                StartGame();
                if (gameOver)
                {
                    EnterLeaderboard();
                }
            }
        }

        private static void ShowStartMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====== TETRIS ======");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Show Leaderboard");
            Console.WriteLine("3. Quit");
            Console.WriteLine("Press the number to select an option.");

            while (true)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.D1 || key == ConsoleKey.NumPad1)
                {
                    break; // Start game
                }
                else if (key == ConsoleKey.D2 || key == ConsoleKey.NumPad2)
                {
                    ShowLeaderboard();
                }
                else if (key == ConsoleKey.D3 || key == ConsoleKey.NumPad3)
                {
                    Environment.Exit(0); // Quit game
                }
            }
        }

        private static void StartGame()
        {
            ResetGame();
            var random = new Random();
            currentTetromino = tetrominoes[random.Next(tetrominoes.Count)];
            currentColor = GetRandomColor();
            nextTetromino = tetrominoes[random.Next(tetrominoes.Count)];
            nextColor = GetRandomColor();

            Thread gameThread = new Thread(GameLoop);
            gameThread.Start();

            while (!gameOver)
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow) Move(-1, 0);
                else if (key == ConsoleKey.RightArrow) Move(1, 0);
                else if (key == ConsoleKey.DownArrow) MoveDownSmooth();
                else if (key == ConsoleKey.UpArrow) Rotate();
            }

            Console.WriteLine("Game Over! Press any key to continue...");
            Console.ReadKey();
        }

        private static void GameLoop()
        {
            int frames = 0;
            while (!gameOver)
            {
                if (frames % 10 == 0) MoveDownSmooth(); // Every 10 frames, move block down a little
                DrawScreen();
              
                frames++;
            }
        }

        private static void DrawScreen()
        {
            DrawBoundary();
            DrawGrid();
            DrawNextBlock();
            DrawScore();
        }

        private static void DrawBoundary()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int y = 0; y <= Height; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write("|");
                Console.SetCursorPosition(Width * 2 + 2, y);
                Console.Write("|");
            }

            for (int x = 0; x <= Width * 2 + 2; x += 2)
            {
                Console.SetCursorPosition(x, Height);
                Console.Write("==");
            }
        }

        private static void DrawGrid()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Console.SetCursorPosition(x * 2 + 1, y);
                    if (grid[y, x] == 1)
                    {
                        Console.ForegroundColor = currentColor;
                        Console.Write("[]");
                    }
                    else if (IsTetrominoBlock(x, y))
                    {
                        Console.ForegroundColor = currentColor;
                        Console.Write("[]");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
            }
        }

        private static void DrawNextBlock()
        {
            Console.SetCursorPosition(Width * 2 + 5, 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Next Block:");

            for (int y = 0; y < nextTetromino.GetLength(0); y++)
            {
                Console.SetCursorPosition(Width * 2 + 5, 4 + y);
                for (int x = 0; x < nextTetromino.GetLength(1); x++)
                {
                    if (nextTetromino[y, x] == 1)
                    {
                        Console.ForegroundColor = nextColor;
                        Console.Write("[]");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
            }
        }

        private static void DrawScore()
        {
            Console.SetCursorPosition(Width * 2 + 5, 10);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score: {score}");
        }

        private static void MoveDownSmooth()
        {
            if (!Move(0, 0.5)) // Move down by half a row for smoother effect
            {
                MergeTetromino();
                ClearLines();
                SpawnNewTetromino();
            }
        }

        private static bool Move(int dx, double dy)
        {
            int newCurrentX = currentX + dx;
            int newCurrentY = (int)(currentY + dy);

            if (CanMove(newCurrentX, newCurrentY))
            {
                currentX = newCurrentX;
                currentY += dy;
                return true;
            }
            return false;
        }

        private static bool CanMove(int newX, int newY)
        {
            for (int y = 0; y < currentTetromino.GetLength(0); y++)
            {
                for (int x = 0; x < currentTetromino.GetLength(1); x++)
                {
                    if (currentTetromino[y, x] == 1)
                    {
                        int checkX = newX + x;
                        int checkY = newY + y;
                        if (checkX < 0 || checkX >= Width || checkY >= Height || (checkY >= 0 && grid[checkY, checkX] == 1))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static void Rotate()
        {
            int[,] rotatedTetromino = new int[currentTetromino.GetLength(1), currentTetromino.GetLength(0)];
            for (int y = 0; y < currentTetromino.GetLength(0); y++)
                for (int x = 0; x < currentTetromino.GetLength(1); x++)
                    rotatedTetromino[x, currentTetromino.GetLength(0) - 1 - y] = currentTetromino[y, x];

            int originalX = currentX;
            double originalY = currentY;

            if (CanMove(0, 0))
                currentTetromino = rotatedTetromino;
            else
            {
                currentX = originalX;
                currentY = originalY;
            }
        }

        private static void MergeTetromino()
        {
            for (int y = 0; y < currentTetromino.GetLength(0); y++)
                for (int x = 0; x < currentTetromino.GetLength(1); x++)
                    if (currentTetromino[y, x] == 1)
                        grid[(int)currentY + y, currentX + x] = 1;
        }

        private static void ClearLines()
        {
            for (int y = Height - 1; y >= 0; y--)
            {
                bool fullLine = true;
                for (int x = 0; x < Width; x++)
                {
                    if (grid[y, x] == 0)
                    {
                        fullLine = false;
                        break;
                    }
                }

                if (fullLine)
                {
                    score += 100;
                    for (int row = y; row > 0; row--)
                        for (int col = 0; col < Width; col++)
                            grid[row, col] = grid[row - 1, col];
                    y++;
                }
            }
        }

        private static void SpawnNewTetromino()
        {
            var random = new Random();
            currentTetromino = nextTetromino;
            currentColor = nextColor;
            nextTetromino = tetrominoes[random.Next(tetrominoes.Count)];
            nextColor = GetRandomColor();
            currentX = Width / 2 - 1;
            currentY = 0;

            if (!CanMove(currentX, (int)currentY))
            {
                gameOver = true;
            }
        }

        private static bool IsTetrominoBlock(int x, int y)
        {
            int localX = x - currentX;
            int localY = y - (int)currentY;
            return localX >= 0 && localY >= 0 && localX < currentTetromino.GetLength(1) && localY < currentTetromino.GetLength(0) && currentTetromino[localY, localX] == 1;
        }

        private static void ResetGame()
        {
            grid = new int[Height, Width];
            score = 0;
            gameOver = false;
            currentY = 0;
        }

        private static void EnterLeaderboard()
        {
            Console.Clear();
            Console.Write("Enter your name for the leaderboard: ");
            string playerName = Console.ReadLine();
            SaveScoreToLeaderboard(playerName, score);
        }

        private static void SaveScoreToLeaderboard(string name, int score)
        {
            var leaderboardEntries = LoadLeaderboard();
            leaderboardEntries.Add(new LeaderboardEntry(name, score));
            leaderboardEntries = leaderboardEntries.OrderByDescending(e => e.Score).Take(10).ToList(); // Keep top 10 scores

            using (StreamWriter writer = new StreamWriter(LeaderboardFile))
            {
                foreach (var entry in leaderboardEntries)
                {
                    writer.WriteLine($"{entry.Name}:{entry.Score}");
                }
            }
        }

        private static void ShowLeaderboard()
        {
            Console.Clear();
            Console.WriteLine("===== LEADERBOARD =====");
            var leaderboardEntries = LoadLeaderboard();
            foreach (var entry in leaderboardEntries)
            {
                Console.WriteLine($"{entry.Name}: {entry.Score}");
            }
            Console.WriteLine("Press 1 to play again...");
            Console.ReadKey();
        }

        private static List<LeaderboardEntry> LoadLeaderboard()
        {
            var entries = new List<LeaderboardEntry>();
            if (File.Exists(LeaderboardFile))
            {
                string[] lines = File.ReadAllLines(LeaderboardFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(':');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        entries.Add(new LeaderboardEntry(parts[0], score));
                    }
                }
            }
            return entries;
        }

        private static ConsoleColor GetRandomColor()
        {
            var colors = new[] { ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta };
            var random = new Random();
            return colors[random.Next(colors.Length)];
        }

        private class LeaderboardEntry
        {
            public string Name { get; }
            public int Score { get; }

            public LeaderboardEntry(string name, int score)
            {
                Name = name;
                Score = score;
            }
        }
    }
}