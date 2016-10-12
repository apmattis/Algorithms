using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_Optimal_Sequence_Alignment
{
    class Program
    {
        static Dictionary<string, int> dict; 

        static void Main(string[] args)
        {
            dict = createDictionary();

            
            Console.WriteLine("Enter first string : ");
            string a = Console.ReadLine();
            a = a.ToLower();
            Console.WriteLine("Enter second string : ");
            string b = Console.ReadLine();
            b = b.ToLower();

            while (!a.Equals("q") && !b.Equals("q")) 
            {
                int[,] sequence = new int[a.Length + 1, b.Length + 1];
                sequence[0, 0] = 0;

                int[,] alignedSequences = alignSequences(dict, sequence, a, b);

                Console.WriteLine("Max Alignment Score : " + alignedSequences[a.Length, b.Length]);
                printAlignments(alignedSequences, a, b, a.Length, b.Length, "", "");
                Console.WriteLine("Enter another first string : ");
                a = Console.ReadLine();
                a = a.ToLower();
                Console.WriteLine("Enter another second string : ");
                b = Console.ReadLine();
                b = b.ToLower();
            }
            
        }

        static int[,] alignSequences(Dictionary<string, int> d, int[,] sequence, string a, string b)
        {
            for (int i = 1; i <= a.Length; i++)
                sequence[i, 0] = dict["-" + a[i - 1].ToString()] + sequence[i - 1, 0]; //add letter scores based on first string

            for (int i = 1; i <= b.Length; i++)
                sequence[0, i] = dict[b[i - 1].ToString() + "-"] + sequence[0, i - 1]; //add letter scores based on second string

            for (int i = 1; i <= a.Length; i++)
                for (int j = 1; j <= b.Length; j++)
                    sequence[i, j] = Math.Max(sequence[i - 1, j - 1] + dict[a[i - 1].ToString() + b[j - 1]],
                            Math.Max(sequence[i - 1, j] + dict[a[i - 1].ToString() + "-"],
                            sequence[i, j - 1] + dict["-" + b[j - 1]]));               //optimize alignment based on scores of both strings
            return sequence;
        }

        static Dictionary<string, int> createDictionary()
        {
            dict = new Dictionary<string, int>();
            string[] nonIdentVowels = {"ae", "ai", "ao", "au", "ea", "ei", "eo", "eu", "ia", "ie", "io", "iu",
                                        "oa", "oe", "oi", "ou", "ua", "ue", "ui", "uo"};                        //array of non-identical vowels to check 
            string[] mismatchPairs = { "bp", "pb", "ck", "kc", "cs", "sc", "dt", "td", "ey", "ye", "gj", "jg",
                                       "iy", "yi", "kq", "qk", "mn", "nm", "sz", "zs", "vw", "wv" };            //array of mismatched pairs to check

            
            for (char c = 'a'; c <= 'z'; c++)               //loop through alphabet
            {   
                string s1 = c.ToString() + c.ToString();
                
                string gap1 = c.ToString() + "-";
                string gap2 = "-" + c.ToString();  
                dict.Add(s1, 2);                            //add matching letters to dictionary with score 2
                dict.Add(gap1, -2);                         //add gap "x-"
                dict.Add(gap2, -2);                         //add gap "-y"

                for (char c2 = 'z'; c2 >= 'a'; c2--)        //loop through alphabet again
                {
                    string s2 = c.ToString() + c2.ToString();
                    if (nonIdentVowels.Contains(s2))            //check if string is a non-identical vowel
                        dict.Add(s2, 1);
                    else if (mismatchPairs.Contains(s2))        //check if string is a mismatch pair
                        dict.Add(s2, 1);
                    else if (!dict.ContainsKey(s2))             //if neither above, check for duplicate
                        dict.Add(s2, -1);
                }
            }

            return dict;
        }

        static void printAlignments(int[,] sequence, string a, string b, int aLength, int bLength, string str1, string str2)
        {
            if (aLength == 0 || bLength == 0)
            {
                while (aLength == 0 && bLength != 0)
                {
                    str1 = "-" + str1;
                    str2 = b[--bLength] + str2;
                }
                while (bLength == 0 && aLength != 0)
                {
                    str1 = a[--aLength] + str1;
                    str2 = '-' + str2;
                }
                Console.WriteLine("\n" + str1 + "\n" + str2 + "\n");
                return;
            }

            if (sequence[aLength, bLength] == (dict[a[aLength - 1] + b[bLength - 1].ToString()] + sequence[aLength - 1, bLength - 1]))
                printAlignments(sequence, a, b, aLength - 1, bLength - 1, a[aLength - 1] + str1, b[bLength - 1] + str2);

            else if (sequence[aLength, bLength] == (dict[a[aLength - 1] + "-"] + sequence[aLength - 1, bLength]))
                printAlignments(sequence, a, b, aLength - 1, bLength, a[aLength - 1] + str1, "-" + str2);

            else if (sequence[aLength, bLength] == (dict["-" + b[bLength - 1]] + sequence[aLength, bLength - 1]))
                printAlignments(sequence, a, b, aLength, bLength - 1, "-" + str1, b[bLength - 1] + str2);
        }
    }
}
