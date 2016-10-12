using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a file path: ");
            string path = Console.ReadLine();
            while(path != "q")
            {
                run(path);
                Console.Write("Enter another file path or 'q' to quit: ");
                path = Console.ReadLine();
            }
            
        }

        public static void run(string path)
        {
            List<Item> items, knapsackSolution;
            int maxWeight, totalVal;
            string line;
            string[] lineTokens;

            try
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(path)) //open file
                {
                    line = file.ReadLine();
                    lineTokens = line.Split(' ');
                    items = new List<Item>(line[0]);        //number of items
                    maxWeight = int.Parse(lineTokens[1]);   //knapsack max weight

                    int i = 1;
                    while ((line = file.ReadLine()) != null)
                    {
                        lineTokens = line.Split(' ');
                        items.Add(new Item(i, int.Parse(lineTokens[0]), int.Parse(lineTokens[1]))); //add item 
                        i++;
                    }

                    knapsackSolution = Knapsack(items, maxWeight, out totalVal); //call Knapsack Algorithm
                    Console.WriteLine("The optimal Knapsack solution has a total value of "
                                       + totalVal + " using items ");
                    Console.Write("{");
                    for(int j = 0; j <= knapsackSolution.Count - 1; j++) //loop through solution and print item IDs
                    {
                        Console.Write(knapsackSolution[j].ToString());
                        if(j < knapsackSolution.Count - 1)
                        {
                            Console.Write(", ");
                        }
                    }
                    Console.Write("}\n");
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
        }

        //Knapsack algorithm
        //the out int parameter allows us to return 2 values: the list of packed items and their combined value
        public static List<Item> Knapsack(List<Item> items, int max, out int totalVal)
        {
            int[,] values = new int[items.Count + 1, max + 1];      // multidim array to store item values
            bool[,] isPacked = new bool[items.Count + 1, max + 1];  // multidim array to keep track of items that will be packed


            for (int i = 1; i <= items.Count; i++)
            {
                Item currItem = items[i - 1];
                for (int bagSpace = 1; bagSpace <= max; bagSpace++)
                {
                    if (bagSpace >= currItem.weight)                        // check if there is room for current item
                    {
                        int spaceRemaining = bagSpace - currItem.weight;
                        int valRemaining = 0;
                        if (spaceRemaining > 0)
                        {
                            valRemaining = values[i - 1, spaceRemaining];
                        }

                        int currItemTotalVal = currItem.value + valRemaining;   // add item value to value of remaining space and assign to total value
                        if (currItemTotalVal > values[i - 1, bagSpace])         // check if the total value is greater than the previous items value
                        {
                            isPacked[i, bagSpace] = true;                   // pack item
                            values[i, bagSpace] = currItemTotalVal;         // add total value to array
                        }
                        else
                        {
                            isPacked[i, bagSpace] = false;                  // don't pack
                            values[i, bagSpace] = values[i - 1, bagSpace];  // keep previous items value stored
                        }
                    }
                }
            }

            List<Item> packedItems = new List<Item>();
            int space = max;
            int itemCount = items.Count;

            while(itemCount > 0)                                 // loop to find packed items
            {
                bool packed = isPacked[itemCount, space];
                if(packed)
                {
                    packedItems.Add(items[itemCount - 1]);       // add packed items 
                    space = space - items[itemCount - 1].weight; // update space remaining in bag
                }

                itemCount--;
            }

            totalVal = values[items.Count, max];
            packedItems.Reverse();
            return packedItems;
        }
    }
}
