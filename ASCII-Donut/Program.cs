namespace ASCII_Donut
{
    /// <summary>
    /// The ASCII Donut Render Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main function of the CLI Program, Prompts the user to resize their terminal. Once they say yes it renders the Donut with a rotation.
        /// </summary>
        /// <param name="args"></param>
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

            Donut donut = new Donut(new Light(0, 1, -1, 75));

            donut.ZPos = 10;

            while (true)
            {
                donut.Render();
                donut.ZPos += 0.1f;
                donut.A += 0.04f;
                donut.B += 0.08f;
            }
        }
    }
}
