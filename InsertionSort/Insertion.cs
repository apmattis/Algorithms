//Insertion Sort
//Alex Mattis
//CS340-001
//2/1/2016


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertionSort
{
    public class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the desired filepath: ");
            string path = Console.ReadLine();

            string[] wordArray = ReadFile(path);                    //read file into string array

            Console.WriteLine("Sorting..." + DateTime.Now);
            var watch = System.Diagnostics.Stopwatch.StartNew();    //stopwatch to time sorting

            string[] sortedWordArray = Insertion(wordArray);        //sort array

            watch.Stop();                                           //end stopwatch
            var elapsedSec = watch.ElapsedMilliseconds / 1000.00;
            Console.WriteLine("Sorting completed\nTime Elapsed: " + elapsedSec + " seconds");

            WriteSortedFile(sortedWordArray);                       //write sorted array to file   
        }

        static string[] ReadFile(string path)
        {
            string line;
            List<string> wordList = new List<string>();

            try
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(path))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        wordList.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File could not be read!");
                Console.WriteLine(e.Message);
            }


            string[] wordArray = wordList.ToArray();
            return wordArray;
        }

        static void WriteSortedFile(string[] input)
        {
            Console.WriteLine("Please name the sorted file: ");
            string file = Console.ReadLine();
            System.IO.File.WriteAllLines(file, input);
        }


        static string[] Insertion(string[] input)
        {
            int i, j;
            for (i = 0; i < input.Length - 1; i++)
            {
                for (j = i + 1; j > 0; j--)
                {
                    if (input[j - 1].CompareTo(input[j]) > 0)
                    {
                        string tmp = input[j - 1];
                        input[j - 1] = input[j];
                        input[j] = tmp;
                    }
                }
            }
            return input;
        }
    }
}
