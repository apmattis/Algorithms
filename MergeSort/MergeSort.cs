//Merge Sort
//Alex Mattis
//CS340-001
//2/1/2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to continue or q to quit.");
            while (Console.ReadLine().ToString().CompareTo("q") != 0)        //Loop main to easily test files consecutively
            {
                Console.WriteLine("Please enter the desired filepath: ");
                string path = Console.ReadLine();

                string[] wordArray = ReadFile(path);                    //read file into string array

                Console.WriteLine("Sorting..." + DateTime.Now);
                var watch = System.Diagnostics.Stopwatch.StartNew();    //stopwatch to time sorting

                MergeSort(wordArray, 0, wordArray.Length - 1);          //Sort array

                watch.Stop();                                           //end stopwatch
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("Sorting complete.\n Elapsed time: " + elapsedMs + " ms");

                WriteSortedFile(wordArray);                       //write sorted array to file
            }

        }

        static void MergeHalves(string[] wordArray, int left, int mid, int right)
        {
            string[] tmp = new string[(right - left) + 1];
            int tmpIndex = 0; int i = left; int j = mid;

            while ((i <= mid - 1) && (j <= right))
            {
                if (wordArray[i].CompareTo(wordArray[j]) < 0)
                {
                    tmp[tmpIndex] = wordArray[i];
                    tmpIndex++;
                    i++;
                }
                else
                {
                    tmp[tmpIndex] = wordArray[j];
                    tmpIndex++;
                    j++;
                }
            }

            while (i <= mid - 1)
            {
                tmp[tmpIndex] = wordArray[i];
                i++;
                tmpIndex++;
            }

            while (j <= right)
            {
                tmp[tmpIndex] = wordArray[j];
                j++;
                tmpIndex++;
            }

            tmpIndex = 0;
            i = left;
            while (tmpIndex < tmp.Length && i <= right)
            {
                wordArray[i] = tmp[tmpIndex];
                i++;
                tmpIndex++;
            }
        }

        static void MergeSort(string[] wordArray, int left, int right)
        {

            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSort(wordArray, left, mid);
                MergeSort(wordArray, mid + 1, right);
                MergeHalves(wordArray, left, mid + 1, right);
            }
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
    }
}
