using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Tokens;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int Mid1, Mid2, Mid3;
            int Fin1, Fin2, Fin3;
            float FRating1, FRating2, FRating3, FRating4;
            float GWA;

            //to calculate the First Subject's Final Rating
            Console.WriteLine("----------------\n Student Remarks\n----------------\n");
            Console.WriteLine("MAJOR");
            Console.WriteLine("Midterm: ");
            Mid1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Finals: ");
            Fin1 = Convert.ToInt32(Console.ReadLine());
            FRating1 = (Mid1 + Fin1) / 2;
            Console.WriteLine("FRatings: " + FRating1);

            //to calculate the First Subject's Final Rating
            Console.WriteLine("----------------\n Student Remarks\n----------------\n");
            Console.WriteLine("MAJOR");
            Console.WriteLine("Midterm: ");
            Mid2 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Finals: ");
            Fin2 = Convert.ToInt32(Console.ReadLine());
            FRating2 = (Mid2 + Fin2) / 2;
            Console.WriteLine("FRatings: " + FRating2);

            //to calculate the First Subject's Final Rating
            Console.WriteLine("----------------\n Student Remarks\n----------------\n");
            Console.WriteLine("MAJOR");
            Console.WriteLine("Midterm: ");
            Mid3 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Finals: ");
            Fin3 = Convert.ToInt32(Console.ReadLine());
            FRating3 = (Mid3 + Fin3) / 2;
            Console.WriteLine("FRatings: " + FRating3);

            GWA = (FRating1 + FRating2 + FRating3) / 3;

           
            Console.WriteLine("GWA:" + GWA);

            if (GWA > 100)
            {
                Console.WriteLine("Invalid Grades");
            }

            //to calculate if the grades is valid for with highest honors
            else if (GWA >= 98 && GWA < 100)
            {
                Console.WriteLine("With Highest Honors");
            }

            //to calculate if the grades is valid for with high honors
            else if (GWA >= 95 && GWA < 97.99)
            {
                Console.WriteLine("With High Honors");
            }
            //to calculate if the grades is valid for with honors
            else if (GWA >= 90 && GWA < 94.99)
            {
                Console.WriteLine("With Honors");
            }
            //to calculate if the grades is passed
            else if (GWA >= 75 && GWA < 89.99)
            {
                Console.WriteLine("Passed");
            }
            //to calculate if the grades is failed
            else if (GWA < 75)
            {
                Console.WriteLine("Failed");
            }




        }
    }
}