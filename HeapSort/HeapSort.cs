//Heap Sort 
//Alex Mattis
//CS340-001
//2/1/2016


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to continue or q to quit.");
            while (Console.ReadLine().ToString().CompareTo("q") != 0)    //Loop main to easily test files consecutively
            {
                Console.WriteLine("Please enter the desired filepath: ");
                string path = Console.ReadLine();

                string[] wordArray = ReadFile(path);                    //read file into string array

                Console.WriteLine("Sorting...");
                var watch = System.Diagnostics.Stopwatch.StartNew();    //stopwatch to time sorting

                HeapSort(wordArray);                                             //Sort array

                watch.Stop();                                           //end stopwatch
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine("\nSorting complete.\nTime Elapsed: " + elapsedMs + " ms\n");

                WriteSortedFile(wordArray);                             //write sorted array to file
                Console.WriteLine("\n\nPress enter to continue or q to quit.");
            }
        }

        static void Heapify(string[] arr, int arrIndex, int heapSize)
        {
            int left = (arrIndex * 2);         //left child of arrIndex
            int right = (arrIndex * 2) + 1;     //right child of arrIndex
            int largest = arrIndex;

            if (left <= heapSize && arr[left].CompareTo(arr[arrIndex]) > 0)
            {
                largest = left;
            }

            if (right <= heapSize && arr[right].CompareTo(arr[largest]) > 0)
            {
                largest = right;
            }

            if (largest != arrIndex)
            {
                string tmp = arr[arrIndex];
                arr[arrIndex] = arr[largest];
                arr[largest] = tmp;

                Heapify(arr, largest, heapSize);
            }
        }

        static void HeapSort(string[] arr)
        {
            //Build Heap
            int heapSize = arr.Length - 1;

            var heapWatch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = (heapSize) / 2; i >= 0; i--)
            {
                Heapify(arr, i, heapSize);
            }
            heapWatch.Stop();
            var elapsedMs = heapWatch.ElapsedMilliseconds;

            Console.WriteLine("Time to build heap: " + elapsedMs + " ms\n");

            for (int i = heapSize; i >= 0; i--)
            {
                string tmp = arr[0];
                arr[0] = arr[i];
                arr[i] = tmp;

                heapSize--;
                Heapify(arr, 0, heapSize);
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
