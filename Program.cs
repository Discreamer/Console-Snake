using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace Snake
{
    class Data
    {
        public static int y = 4;
        public static int x = 4;
        public static int Tail = 1;
        public static string vect = "right";
        public static int timetomove = 500;
    }
    class Field
    {


        public int[,] MetField = new int[10, 10]
          {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,Data.Tail,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,9999,0,0},
            {0,0,0,0,0,0,0,0,0,0}
         };
        public void ShowField()
        {
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (MetField[i, j] == 0)
                    {
                        Console.Write(" ");
                    }
                    else if (MetField[i, j] == Data.Tail)
                    {
                        Console.Write("$");
                    }
                    else if (MetField[i, j] < Data.Tail)
                    {
                        Console.Write("#");
                    }
                    else if (MetField[i, j] == 9999)
                    {
                        Console.Write("@");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                    Console.Write(" ");
                }
                Console.Write("|\n");
            }
            Console.WriteLine("--------------------\n");
            Console.WriteLine("Направление: " + Data.vect);
            Console.WriteLine("Длина хвоста:" + (Data.Tail - 1).ToString());
        }
        public void DoStep()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (MetField[i, j] != 0 && MetField[i, j] != 9999)
                    {
                        MetField[i, j] = MetField[i, j] - 1;
                    }
                }
            }
            if (Data.vect == "right")
            {
                Data.x += 1;
            }
            else if (Data.vect == "left")
            {
                Data.x -= 1;
            }
            else if (Data.vect == "up")
            {
                Data.y -= 1;
            }
            else if (Data.vect == "down")
            {
                Data.y += 1;
            }
        }
    }
    class Program
    {
        static ConsoleKeyInfo WaitForKey(int ms)
        {
            int delay = 0;
            while (delay < ms)
            {
                if (Console.KeyAvailable)
                {
                    return Console.ReadKey();
                }
                Thread.Sleep(10);
                delay += 10;
            }
            return new ConsoleKeyInfo((char)0, (ConsoleKey)0, false, false, false);
        }
        static void Main(string[] args)
        {
            Data.y = 4;
            Data.x = 4;
            Data.Tail = 1;
            Data.vect = "right";
            Field MainField = new Field();
            long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            MainField.ShowField();
            for (; ; )
            {
                ConsoleKeyInfo me = WaitForKey(10);

                if (me.Key == ConsoleKey.DownArrow)
                {
                    Data.vect = "down";
                    MainField.ShowField();
                }
                else if (me.Key == ConsoleKey.UpArrow)
                {
                    Data.vect = "up";
                    MainField.ShowField();
                }
                else if (me.Key == ConsoleKey.LeftArrow)
                {
                    Data.vect = "left";
                    MainField.ShowField();
                }
                else if (me.Key == ConsoleKey.RightArrow)
                {
                    Data.vect = "right";
                    MainField.ShowField();
                }
                long Notnow = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (Notnow - now > Data.timetomove)
                {
                    MainField.ShowField();
                    MainField.DoStep();
                    now = Notnow;
                    if (Data.Tail == 98) ;
                    if (Data.y >= 10 || Data.x >= 10 || Data.y < 0 || Data.x < 0 || (MainField.MetField[Data.y, Data.x] != 0 && MainField.MetField[Data.y, Data.x] != 9999))
                    {
                        break;
                    }
                    if (MainField.MetField[Data.y, Data.x] == 9999)
                    {
                        bool F = false;
                        while (!F)
                        {
                            Random rnd = new Random();
                            int rnd1 = rnd.Next(0, 10);
                            int rnd2 = rnd.Next(0, 10);
                            if (MainField.MetField[rnd1, rnd2] == 0)
                            {
                                F = true;
                                Data.Tail += 1;
                                MainField.MetField[rnd1, rnd2] = 9999;
                            }
                        }
                    }
                    MainField.MetField[Data.y, Data.x] = Data.Tail;
                    MainField.ShowField();
                }
            }
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.WriteLine("Your score: " + Data.Tail.ToString());
            Console.ReadKey();
            Main(null);
        }
    }
}
