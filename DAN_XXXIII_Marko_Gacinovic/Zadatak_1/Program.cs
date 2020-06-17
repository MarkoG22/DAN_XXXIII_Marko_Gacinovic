using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // array of threads
            Thread[] tArray = new Thread[4];
            tArray[0] = new Thread(() => Matrix());
            tArray[1] = new Thread(() => RandomOdd());
            tArray[2] = new Thread(() => MatrixFromFile());
            tArray[3] = new Thread(() => SumFromFile());

            // loop for giving thread names
            for (int i = 0; i < tArray.Length; i++)
            {
                if (i % 2 == 0)
                {
                    tArray[i].Name = string.Format("Thread_{0}", i + 1);
                    Console.WriteLine("Thread_{0} is created.", i + 1);
                }
                else
                {
                    tArray[i].Name = string.Format("Thread_{0}{0}", i + 1);
                    Console.WriteLine("Thread_{0}{0} is created.", i + 1);
                }
            }

            // stopwatch to take time for first two threads
            Stopwatch s = new Stopwatch();
            s.Start();

            tArray[0].Start();
            tArray[1].Start();
            tArray[0].Join();
            tArray[1].Join();
            s.Stop();

            TimeSpan ts = s.Elapsed;

            Console.WriteLine("\nElapsed time for both {0} and {1} threads: {2}\n", tArray[0].Name, tArray[1].Name, ts);

            // starting and joining first two threads
            tArray[2].Start();
            tArray[3].Start();
            tArray[2].Join();
            tArray[3].Join();

            Console.ReadLine();
        }

        /// <summary>
        /// method for unit matrix
        /// </summary>
        public static void Matrix()
        {
            int[,] matrix = new int[100, 100];

            try
            {
                // streamwriter for writing the matrix to the file
                using (StreamWriter sw = new StreamWriter("../../FileByThread_1.txt"))
                {
                    // two loops for filling the unit matrix
                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            if (i == j)
                            {
                                matrix[i, j] = 1;
                            }
                            //else
                            //{
                            //    matrix[i, j] = 0;
                            //}
                            sw.Write(matrix[i, j]);
                        }
                        sw.WriteLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// method for writing random odd numbers to the file
        /// </summary>
        public static void RandomOdd()
        {
            try
            {
                // streamwriter for writing random odd numbers to the file
                using (StreamWriter sw = new StreamWriter("../../FileByThread_22.txt"))
                {
                    Random rnd = new Random();

                    int counter = 0;

                    // loops for writing 1000 random odd numbers
                    while (counter < 1000)
                    {
                        int a = rnd.Next(0, 10000);
                        if (a % 2 != 0)
                        {
                            sw.WriteLine(a);
                            counter++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// method for reading the matrix from the file and writing on the console
        /// </summary>
        public static void MatrixFromFile()
        {
            try
            {
                // streamreader to read the matrix from the file
                using (StreamReader sr = new StreamReader("../../FileByThread_1.txt"))
                {
                    string line;

                    // loop for writing lines from file to the console
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// method for summing the lines from the file
        /// </summary>
        public static void SumFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader("../../FileByThread_22.txt"))
                {
                    Thread.Sleep(1000);
                    string line;
                    int sum = 0;

                    // loop for calculating the sum
                    while ((line = sr.ReadLine()) != null)
                    {
                        int a = int.Parse(line);
                        sum = sum + a;
                    }

                    Console.WriteLine("\nOdd numbers sum: {0}", sum);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
