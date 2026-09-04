using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MID_TERM_PROJECT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<string[]>> Files = new Dictionary<string, List<string[]>>();
            List<string> Category = new List<string>();
            List<string[]> items = new List<string[]>();
            List<string> ConfigItems = new List<string>();
            List<string[]> Ratings = new List<string[]>();
            List<int> RatingCount = new List<int>();
            List<int> SortedRatingCount= new List<int>();
            List<double> Price = new List<double>();
            List<double> SortedPrice = new List<double>();
            int sortingCount = 0;
            int categoryNum = 0;

            string line = "";
            using (StreamReader sr = new StreamReader("D:\\Southville School Files\\School Works\\Second Semester_First Year\\Data Structures and Algorithms\\MID Terms\\Setup.ini"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] Config = line.Split('=');
                    ConfigItems.AddRange(Config);
                }
            }

            string Line = "";
            using (StreamReader sr = new StreamReader(ConfigItems[3] + "\\" +ConfigItems[1]))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] Values = line.Split(',');
                    items.Add(Values);
                    if (!Category.Contains(Values[5])) 
                    {
                        Category.Add(Values[5]);
                    }
                    if (!RatingCount.Contains(int.Parse(Values[2])))
                    {
                        RatingCount.Add(int.Parse(Values[2]));
                    }
                    if (!Price.Contains(double.Parse(Values[3])))
                    {
                        Price.Add(double.Parse(Values[3]));
                    }

                }
                
            }

            while (RatingCount.Count != 0)
            {
                int lowestIndex = 0;
                for (int i = 1; i < RatingCount.Count; i++)
                {
                    if (RatingCount[i] < RatingCount[lowestIndex])
                    {
                        lowestIndex = i;
                    }
                }   
                SortedRatingCount.Add(RatingCount[lowestIndex]);
                RatingCount.RemoveAt(lowestIndex);
            }

            while (Price.Count != 0)
            {
                int lowestIndex = 0;
                for (int i = 1; i < Price.Count; i++)
                {
                    if (Price[i] < Price[lowestIndex])
                    {
                        lowestIndex = i;
                    }
                }
                SortedPrice.Add(Price[lowestIndex]);
                Price.RemoveAt(lowestIndex);
            }

            Console.WriteLine($"There are {Category.Count} Categories");
            Console.WriteLine("The Program will segregate the files per category.");
            Console.WriteLine("How do you want the program to sort them by? (always in ascending order)");
            Console.WriteLine($"\t [a] rating");
            Console.WriteLine($"\t [b] rating count");
            Console.WriteLine($"\t [c] rating price");
            Console.Write("Please input your answer here : ");
            char uInput = ' ';
            try
            {
                uInput = char.Parse(Console.ReadLine().ToUpper());
            }
            catch
            {
                Console.WriteLine("\t Invalid Input \t");
            }
            
            if (uInput == 'A')
            {
                int max = 0;

                foreach (string[] arr in items)
                {
                    if (int.Parse(arr[1]) > max)
                    {
                        max = int.Parse(arr[1]);
                    }

                }


                while (true)
                {
                    if (sortingCount < 1)
                    {
                        Console.Write($"Sorting {Category[categoryNum]}. . . ");
                        sortingCount++;
                    }

                    for (int arrageNum = 0; arrageNum <= max; arrageNum++)
                    {
                        foreach (string[] arr in items)
                        {

                            if (arr.Contains(Category[categoryNum]))
                            {

                                if (arrageNum == int.Parse(arr[1]))
                                {

                                    Ratings.Add(arr);

                                }
                            }

                        }

                    }

                    Console.WriteLine("Done!");
                    sortingCount= 0;
                    Files.Add(Category[categoryNum], Ratings);
                    Ratings = new List<string[]>();
                    categoryNum++;

                    if (categoryNum == Category.Count - 1)
                    {
                        Files.Add(Category[categoryNum], Ratings);
                        break;
                    }

                }

            }

            else if (uInput == 'B')
            {

                while (true)
                {
                    if (sortingCount < 1)
                    {
                        Console.Write($"Sorting {Category[categoryNum]}. . . ");
                        sortingCount++;
                    }

                    foreach (int num in SortedRatingCount)
                    {
                        foreach (string[] arr in items)
                        {

                            if (arr.Contains(Category[categoryNum]))
                            {

                                if (num == int.Parse(arr[2]))
                                {

                                    Ratings.Add(arr);

                                }
                            }

                        }
                    }
                    
                    Console.WriteLine("Done!");
                    sortingCount= 0;
                    Files.Add(Category[categoryNum], Ratings);
                    Ratings = new List<string[]>();
                    categoryNum++;

                    if (categoryNum == Category.Count - 1)
                    {
                        Files.Add(Category[categoryNum], Ratings);
                        break;
                    }

                }

            }

            else if (uInput == 'C')
            {

                while (true)
                {
                    if (sortingCount < 1)
                    {
                        Console.Write($"Sorting {Category[categoryNum]}. . . ");
                        sortingCount++;
                    }

                    foreach (double num in SortedPrice)
                    {
                        foreach (string[] arr in items)
                        {

                            if (arr.Contains(Category[categoryNum]))
                            {

                                if (num == double.Parse(arr[3]))
                                {

                                    Ratings.Add(arr);

                                }
                            }

                        }
                    }

                    Console.WriteLine("Done!");
                    sortingCount= 0;
                    Files.Add(Category[categoryNum], Ratings);
                    Ratings = new List<string[]>();
                    categoryNum++;

                    if (categoryNum == Category.Count - 1)
                    {
                        Files.Add(Category[categoryNum], Ratings);
                        break;
                    }

                }

            }

            categoryNum= 0;
            foreach (KeyValuePair<string, List<string[]>> kp in Files)
            {
                using (StreamWriter sw = new StreamWriter($"D:\\Southville School Files\\School Works\\Second Semester_First Year\\Data Structures and Algorithms\\MID Terms\\Output\\{kp.Key}.csv"))
                {
                    Console.Write($"Writing {Category[categoryNum]}. . . ");
                    foreach (string[] r in kp.Value)
                    {
                        foreach (string s in r)
                        {
                            sw.Write(s + ",");
                        }
                        sw.WriteLine();

                    }
                    Console.WriteLine("Done!");
                    categoryNum++;
                }
            }


            Console.ReadKey();
        }
    }
}
