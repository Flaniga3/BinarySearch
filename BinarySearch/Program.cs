using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BinarySearch
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey key = ConsoleKey.Enter;

            while (key == ConsoleKey.Enter)
            {
                //Generate an array of random numbers (some rules apply here)
                Random rnd = new Random();
                int[] numbers = new int[100];
                numbers[0] = rnd.Next(1, 6);
                for (int i = 1; i < numbers.Length; i++)
                {
                    numbers[i] = numbers[i - 1] + rnd.Next(1, 11);
                }
                //Max value that could be in the array is 995
                int search = rnd.Next(1, 996);

                //Some courtesy output
                Console.WriteLine($"Your array is: {String.Join(", ", numbers)}");
                Console.WriteLine($"Your randomly generated search number is {search}...");
                Console.WriteLine("Searching...");

                //Using a key value pair so we can pass back both the index found and the number of operations
                KeyValuePair<int, int> foundBin = BinarySearch(numbers, search);
                KeyValuePair<int, int> foundLin = LinearSearch(numbers, search);

                if (foundBin.Key > -1)
                {
                    Console.WriteLine($"found at {foundBin.Key}, in only {foundBin.Value} operations.");
                    Console.WriteLine($"Linear search did the same work in {foundLin.Value} operations. Pathetic.");
                }
                else
                {
                    Console.WriteLine($"Not found, but you only performed {foundBin.Value} Operations.");
                    Console.WriteLine($"Linear search perform {foundLin.Value} operations. What a joke!");
                }

                //Try it again or quit
                Console.WriteLine("Press enter to try again, press any other key to exit...");
                key = Console.ReadKey().Key;
            }
        }

        //Takes a sorted array of integers and a value to search the array for
        //Returns a KeyValuePair containing the index the number was found at (key)
        //and the number of operations performed
        static KeyValuePair<int, int> BinarySearch(int[] array, int search)
        {
            int lo = 0, hi = array.Length - 1;
            int numOps = 0;
            while (lo <= hi)
            {
                numOps++;
                int mid = lo + (hi - lo)/2;
                if (search > array[mid]) lo = mid + 1;
                else if (search < array[mid]) hi = mid - 1;
                else return new KeyValuePair<int, int>(mid, numOps);
            }
            return new KeyValuePair<int, int>(-1, numOps);
        }

        //Takes an array of integers and a value to search the array for
        //Returns a KeyValuePair containing the index the number was found at (key)
        //and the number of operations performed
        static KeyValuePair<int, int> LinearSearch(int[] array, int search)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] == search)
                {
                    return new KeyValuePair<int, int>(i, i + 1);
                }
            }
            return new KeyValuePair<int, int>(-1, 100);
        }
    }
}
