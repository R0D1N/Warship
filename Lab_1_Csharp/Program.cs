using System;

namespace Lab_1_Csharp
{
    class Game
    {
        public int rows;
        private int columns;
        private int ships;
        private int[,] field;
        private int attempts;

        static int InputDimension()
        {
            Console.WriteLine("Enter a size of the field");
            int number;
            string get;
            get = Console.ReadLine();
            while (!int.TryParse(get, out number) || number > 10 || number < 4)
            {
                Console.WriteLine("Enter a number in range from 4 to 10");
                get = Console.ReadLine();
            }
            return number;
        }

        void GenerateField()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = 0;
                }
            }

            Random rand = new Random();
            int x, y;
            bool checker;
            for (int i = 0; i < ships;)
            {
                checker = false;
                int seed = rand.Next(2);
                switch (seed)
                {
                    case 0: //horizantal
                        y = rand.Next(rows - 2);
                        x = rand.Next(rows);
                        for (int k = 0; k < 3; k++)
                        {
                            //check for a existing
                            if (field[x, y] == 1)
                            {
                                break;
                            }
                            else
                            {
                                checker = true;
                            }
                            field[x, y] = 1;
                            y++;
                        }
                        if (checker)
                        {
                            i++;
                        }
                        break;

                    case 1: //vertical
                        x = rand.Next(rows - 2);
                        y = rand.Next(rows);
                        for (int k = 0; k < 3; k++)
                        {
                            //check for a existing
                            if (field[x, y] == 1)
                            {
                                break;
                            }
                            else
                            {
                                checker = true;
                            }
                            field[x, y] = 1;
                            x++;
                        }
                        if (checker)
                        {
                            i++;
                        }
                        break;
                }
            }
        }

        int InputCoordinate()
        {
            int number;
            string get;
            get = Console.ReadLine();
            while (!int.TryParse(get, out number) || number > rows || number < 1)
            {
                Console.WriteLine("Enter a correct coordinates");
                get = Console.ReadLine();
            }
            return number;
        }

        void Attempts()
        {
            Console.WriteLine("How many attempts do you want?");
            string get;
            get = Console.ReadLine();
            while (!int.TryParse(get, out attempts) || attempts > 100 || attempts < 6)
            {
                Console.WriteLine("Enter a number in range from 6 to 100");
                get = Console.ReadLine();
            }
            if (attempts >= field.Length)
            {
                attempts = field.Length - 1;
                Console.WriteLine("Amount of attempts is {0}", field.Length - 1);
            }
        }

        public Game()
        {
            rows = InputDimension();
            columns = rows;
            field = new int[rows, columns];
            ships = 2;
            GenerateField();
            Attempts();
        }

        void Draw()
        {
            Console.Write("   ");

            for (int i = 1; i < field.GetLength(0) + 1; i++)
            {
                Console.Write(" {0}", i);

            }
            Console.WriteLine();

            for (int i = 1; i < field.GetLength(0)+1; i++)
            {
                Console.Write("{0, 3}", " " + i);
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    switch (field[i - 1, j])
                    {
                        case 2:
                            Console.Write(" X");
                            break;
                        case 3:
                            Console.Write(" .");
                            break;
                        default:
                            Console.Write(" O");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        int CountLiveShips()
        {
            int liveShips = 0;
            for (int i = 1; i < rows + 1; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (field[i - 1, j] == 1)
                    {
                        liveShips++;
                    }
                }
            }
            return liveShips;
        }

        public int Shoot()
        {
            Draw();
            int xShoot = InputCoordinate();
            int yShoot = InputCoordinate();
            Console.Clear();
            attempts--;

            switch (field[xShoot - 1, yShoot - 1])
            {
                case 0:
                    field[xShoot - 1, yShoot - 1] = 3;
                    Console.WriteLine("You missed");
                    break;
                case 1:
                    field[xShoot - 1, yShoot - 1] = 2;
                    Console.WriteLine("Right on target!");
                    break;
            }

            if (CountLiveShips() == 0)
            {
                Console.WriteLine("Well played!!!");
                Console.ReadLine();
                return -1;
            }

            return attempts;
        }
    }

    class Program
    {
        static void Main()
        {
            Game game= new Game();
            do
            {
                Console.WriteLine("Take your shoot");
            }
            while (game.Shoot() > 0);
            Console.WriteLine("Thanks for a game");
            Console.WriteLine("See you next time");
        }
    }
}
