﻿namespace ASCII_Donut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool start = false;

            while (!start)
            {
                Console.WriteLine("Resize your console, once you're ready input (Y)");
                string line = Console.ReadLine();
                if (line == "Y")
                    start = true;
            }

            Donut donut = new Donut(new Light(0, 1, -1, 30));

            while (true)
            {
                donut.Render();
                donut.ZPos += 0.1f;
                //donut.YPos += 0.1f;
                donut.A += 0.04f;
                donut.B += 0.08f;
                //donut.LightSource.UpdateStrength(donut.LightSource.Strength + 0.1f);
            }
        }
    }
}
