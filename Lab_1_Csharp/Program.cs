using System;

namespace Lab_1_Csharp
{
    class Game
    {

        public int rows;
        int columns;
        int ships;
        int[,] field;
        int attempts;

        /// <summary>
        /// TryParse to enter size
        /// </summary>
        /// <returns> Field size in range from 4 to 10 </returns>
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

        /// <summary>
        /// Generate field with:
        /// zero(empty point);
        /// one(ship point);
        /// </summary>
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

        /// <summary>
        /// TryParse to enter coordinate
        /// </summary>
        /// <returns>number of coordinate in range from 1 to count of rows</returns>
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

        /// <summary>
        /// TryParse to enter amount of attemts
        /// Reducing to max of field`s points
        /// </summary>
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

        /// <summary>
        /// Innit of GameClass
        /// </summary>
        public Game()
        {
            rows = InputDimension();
            columns = rows;
            field = new int[rows, columns];
            ships = 2;
            GenerateField();
            Attempts();
        }

        /// <summary>
        /// Drawing according to the number field
        /// </summary>
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

        /// <summary>
        /// checking for point equils one
        /// </summary>
        /// <returns>amount of alive points</returns>
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

        /// <summary>
        /// Changing from 0 to 3 if you missed
        /// Chenging from 1 to 2 if you take it
        /// </summary>
        /// <returns>amount of attempts after user shooting</returns>
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
