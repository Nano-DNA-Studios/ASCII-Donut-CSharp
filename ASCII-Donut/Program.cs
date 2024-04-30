namespace ASCII_Donut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Donut donut = new Donut();

            while (true)
            {
                donut.Render();
                donut.A += 0.04f;
                 donut.B += 0.08f;
            }
        }
    }
}
