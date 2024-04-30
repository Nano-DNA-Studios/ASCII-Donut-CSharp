
namespace ASCII_Donut
{
    internal class Donut
    {
        /// <summary>
        /// Luminence Values
        /// </summary>
        string luminenceValues = ".,-~:;=!*#$@";

        public float thetaStep = 0.02f;

        public float phiStep = 0.03f;

        /// <summary>
        /// Radius of the Circle part of the torus
        /// </summary>
        public float R1 = 0.3f;

        /// <summary>
        /// Radius of the torus, distance from the center of the torus to the center of the circle
        /// </summary>
        public float R2 = 1;

        /// <summary>
        /// Distance from the screen
        /// </summary>
        public float K2 = 2;

        /// <summary>
        /// Scaling Factor
        /// </summary>
        public float K1;

        /// <summary>
        /// Light Position X
        /// </summary>
        public float LightX = 0;

        /// <summary>
        /// Light Position Y
        /// </summary>
        public float LightY = 1;

        /// <summary>
        /// Light Position Z
        /// </summary>
        public float LightZ = -1;

        /// <summary>
        /// Height of the Screen
        /// </summary>
        int height = Console.WindowHeight;

        /// <summary>
        /// Width of the Screen
        /// </summary>
        int width = Console.WindowWidth;

        /// <summary>
        /// Angle Around the X Axis
        /// </summary>
        public float A;

        /// <summary>
        /// Angle Around the Z Axis
        /// </summary>
        public float B;

        public void Render()
        {
            height = Console.WindowHeight;
            width = Console.WindowWidth;

            K1 = width * K2 * 3 / (24 * (R1 + R2));

            int length = width * height;

            char[] buf = new char[length];
            float[] zBuffer = new float[length];
            Array.Fill(buf, ' ');
            Array.Fill(zBuffer, float.MinValue);

            //float cosY = MathF.Cos(Y), sinY = MathF.Sin(Y);
            float cosB = MathF.Cos(B), sinB = MathF.Sin(B);
            float cosA = MathF.Cos(A), sinA = MathF.Sin(A);

            for (float theta = 0; theta < 2 * Math.PI; theta += thetaStep)
            {
                float cosTheta = MathF.Cos(theta);
                float sinTheta = MathF.Sin(theta);

                float circleX = R2 + R1 * cosTheta;
                float circleY = R1 * sinTheta;

                for (float phi = 0; phi < 2 * Math.PI; phi += phiStep)
                {
                    float cosPhi = MathF.Cos(phi);
                    float sinPhi = MathF.Sin(phi);

                    float x1 = cosB * cosPhi + sinB * sinA * sinPhi;
                    float x2 = cosA * sinB;

                    float y1 = sinB * cosPhi - cosB * sinA * sinPhi;
                    float y2 = cosA * cosB;

                    float z1 = cosA * sinPhi;
                    float z2 = sinA;

                    float x = circleX * x1 - circleY * x2;
                    float y = circleX * y1 + circleY * y2;
                    float z = K2 + circleX * z1 + circleY * z2;

                    float ooz = 1 / z;

                    int xp = (int)(width / 2 + K1 * ooz * x);
                    int yp = (int)(height / 2 - K1 * ooz * y);

                    xp = Math.Clamp(xp, 0, width - 1);
                    yp = Math.Clamp(yp, 0, height - 1);

                    int idx = xp + yp * width;

                    float nx = cosTheta;
                    float ny = sinTheta;

                    float Nx = nx * x1 - ny * x2;
                    float Ny = nx * y1 + ny * y2;
                    float Nz = nx * z1 + ny * z2;

                    float Luminence = Nx * LightX + Ny * LightY + Nz * LightZ;

                    if (idx > length || idx < 0)
                        continue;

                    // test against the z-buffer.  larger 1/z means the pixel is
                    // closer to the viewer than what's already plotted.
                    if (ooz > zBuffer[idx])
                    {
                        int luminance_index = (int)(((Luminence + MathF.Sqrt(2)) * (11)) / (2 * MathF.Sqrt(2)));

                        zBuffer[idx] = ooz;
                        //int luminance_index = (int)(((Luminence + MathF.Sqrt(2))) * 8) / 2;
                        // luminance_index is now in the range 0..11 (8*sqrt(2) = 11.3)
                        buf[idx] = luminenceValues[luminance_index];
                    }

                }
            }

            string display = "";
            for (int i = 0; i < buf.Length; i++)
            {
                display += buf[i];
                if (i % width == width - 1)
                    display += "\n";
            }

            Console.Clear();
            Console.Write(display);

           // Thread.Sleep(10);


            /* Console.Clear();
             for (int k = 0; k < buf.Length; k++)
             {
                 Console.Write(buf[k]);
                 if (k % width == width - 1)
                     Console.WriteLine();
             }*/

            Thread.Sleep(50);
        }
    }
}
