using System;
using System.Threading;

class GameCode
{
    static int gridSize = 10;
    static int playerX = 0;
    static int playerY = 0;
    static int treasureX;
    static int treasureY;
    static bool[,] mines = new bool[gridSize, gridSize];

    static void Main(string[] args)
    {
        InitializeGame();
        while (true)
        {
            Console.Clear();
            DrawGrid();
            ConsoleKeyInfo keyInfo = Console.ReadKey(); // Поясняю за строчку. Тут прога ждет пока игрок нажмет на какую либо кнопку
//а когда это происходит, то она запоминает эту кнопку и её потом можно юзать. У меня так управление сделано на ней.
            HandleInput(keyInfo.Key);
        }
    }

    static void InitializeGame()
    {
        Random rnd = new Random();
        treasureX = rnd.Next(gridSize);
        treasureY = rnd.Next(gridSize);

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                //20 проц щанс спавна минки
                if (rnd.Next(5) == 0)
                {
                    mines[i, j] = true;
                }
            }
        }
    }

    static void DrawGrid()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (i == playerY && j == playerX)
                {
                    Console.Write("P ");
                }
                else if (i == treasureY && j == treasureX)
                {
                    Console.Write("T ");
                }
                else
                {
                    Console.Write(mines[i, j] ? ". " : ". ");
                }
            }
            Console.WriteLine();
        }
    }

    static void HandleInput(ConsoleKey key)
    {
        int newX = playerX;
        int newY = playerY;
        string victory = "Congratulations! You found the treasure.";
        string lose = "Boom! You hit a mine. Game over!";

        switch (key)
        {
            case ConsoleKey.W:
                newY = Math.Max(0, playerY - 1);
                break;
            case ConsoleKey.S:
                newY = Math.Min(gridSize - 1, playerY + 1);
                break;
            case ConsoleKey.A:
                newX = Math.Max(0, playerX - 1);
                break;
            case ConsoleKey.D:
                newX = Math.Min(gridSize - 1, playerX + 1);
                break;
        }

        // Ну тут если на мину наступил
        if (mines[newY, newX])
        {
            Console.Clear();
            foreach (char c in lose)
            {
                Console.Write(c);
                Thread.Sleep(60);
            }
            Console.WriteLine();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (i == playerY && j == playerX)
                    {
                        Console.Write("P ");
                    }
                    else if (i == treasureY && j == treasureX)
                    {
                        Console.Write("T ");
                    }
                    else
                    {
                        Console.Write(mines[i, j] ? "x " : ". ");
                    }
                }
                Console.WriteLine();
            }

            Thread.Sleep(2000);
            Environment.Exit(0);
        }

        playerX = newX;
        playerY = newY;

        //А тут если наступил на сокровище
        if (playerX == treasureX && playerY == treasureY)
        {
            Console.Clear();
            foreach (char c in victory)
            {
                Console.Write(c);
                Thread.Sleep(60);
            }
            Console.WriteLine();

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (i == playerY && j == playerX)
                    {
                        Console.Write("P ");
                    }
                    else if (i == treasureY && j == treasureX)
                    {
                        Console.Write("T ");
                    }
                    else
                    {
                        Console.Write(mines[i, j] ? "x " : ". ");
                    }
                }
                Console.WriteLine();
            }

            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}

// если че вот эти
//foreach (char c in victory или lose)
//{
//    Console.Write(c);
//    Thread.Sleep(60);
//}
//Console.WriteLine();
// Это я типо я красиво вывожу текст чтобы глаз радовался