using System;

namespace Lab_1_Csharp
{
    class Program
    {
        //entering the field
        static int IntInputUser()
        {
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

        //entering coordinates of the shooting
        static int ShootCoor(string[,] field)
        {
            int number;
            string get;
            get = Console.ReadLine();
            while (!int.TryParse(get, out number) || number > field.GetLength(0) || number < 1)
            {
                Console.WriteLine("Enter a correct coordinates");
                get = Console.ReadLine();
            }
            return number;
        }

        //creating the field
        static void CreateField(string[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = " O";
                }
            }
        }

        
        static void CreateBackOffield(bool[,] checkField)
        {
            for (int i = 0; i < checkField.GetLength(0); i++)
            {
                for (int j = 0; j < checkField.GetLength(1); j++)
                {
                    checkField[i, j] = false;
                }
            }
        }

        //drawing the field
        static void Draw(string[,] field)
        {
            Console.Write("   ");

            for (int i = 1; i < field.GetLength(0) + 1; i++)
            {
                Console.Write(" {0}",i);
                
            }
            Console.WriteLine();

            int k = 1;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                Console.Write("{0, 3}", " " + k);
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write(field[i, j]);
                }
                Console.WriteLine();
                k++;
            }
        }

        static int Attempts(string[,] field)
        {
            Console.WriteLine("How many attempts do you want?");
            int attempts;
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
            return attempts;
        }

        static void RandomGenShips(bool[,] checkField, int amountOfShips)
        {
            Random rand = new Random();
            int i, j;
            bool checker;
            for (int l = 0; l < amountOfShips; )
            {
                checker = false;
                int seed = rand.Next(2);
                switch (seed)
                {
                    case 0: //gorizantal
                        j = rand.Next(checkField.GetLength(0) - 2);
                        i = rand.Next(checkField.GetLength(0));
                        for (int k = 0; k < 3; k++)
                        {
                            //check for a existing
                            if (checkField[i, j])
                            {
                                break;
                            }
                            else
                            {
                                checker = true;
                            }
                            checkField[i, j] = true;
                            j++;
                        }
                        if (checker)
                        {
                            l++;
                        }
                        break;

                    case 1: //vertical
                        i = rand.Next(checkField.GetLength(0) - 2);
                        j = rand.Next(checkField.GetLength(0));
                        for (int k = 0; k < 3; k++)
                        {
                            //check for a existing
                            if (checkField[i, j])
                            {
                                break;
                            }
                            else
                            {
                                checker = true;
                            }
                            checkField[i, j] = true;
                            i++;
                        }
                        if (checker)
                        {
                            l++;
                        }
                        break;
                }
            }
        }



        //game procces
        static void GameLogic(string[,] field, bool[,] checkField, int attempts, int amountOfShips)
        {
            int counter = 0;
            int winCounter = 0;
            bool inGame = true;
            while (inGame)
            {
                Draw(field);
                Console.WriteLine("Take your shoot");
                int xShoot = ShootCoor(field);
                int yShoot = ShootCoor(field);
                Console.Clear();

                //shooting
                if (checkField[xShoot - 1, yShoot - 1])
                {
                    field[xShoot - 1, yShoot - 1] = " X";
                    Console.WriteLine("Right on target!");
                    winCounter++;
                }
                else
                {
                    field[xShoot - 1, yShoot - 1] = " .";
                    Console.WriteLine("You missed");
                }
                //checking of the end
                if (counter == attempts)
                {
                    Console.WriteLine("Don`t worry");
                    inGame = false;
                    Console.ReadLine();
                }
                if (winCounter == amountOfShips * 3)
                {
                    Console.WriteLine("Well played!!!");
                    inGame = false;
                    Console.ReadLine();
                }
                counter++;
            }
        }

        static void Main()
        {
            int amountOfShips = 2;

            Console.WriteLine("Enter field size, please");
            int sizeField = IntInputUser();


            //creating the field array
            string[,] field = new string[sizeField, sizeField];

            CreateField(field);


            //creating the back of field array
            bool[,] checkField = new bool[sizeField, sizeField];

            CreateBackOffield(checkField);


            //entering a number of shoots
            int attempts = Attempts(field);

            //spawning ships
            RandomGenShips(checkField, amountOfShips);


            Console.WriteLine("To play, enter two numbers");

            //start
            GameLogic(field, checkField, attempts, amountOfShips);
            Console.WriteLine("Thanks for a game");
            Console.WriteLine("See you next time");
        }
    }
}
