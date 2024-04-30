namespace ASCII_Donut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DonutReal donut = new DonutReal();

            //donut.positionZ = -4;
           // donut.A = (float)Math.PI /2;
           // donut.B = (float)Math.PI ;

            while (true)
            {
                donut.Render();
                //donut.positionZ -= 0.1f;
                donut.A += 0.04f;
                 donut.B += 0.04f;
                //donut.lightX -= 0.1f;
            }
        }
    }
}
