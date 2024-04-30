
namespace ASCII_Donut
{
    internal class DonutReal
    {
        public float thetaStep = 0.02f;

        public float phiStep = 0.03f;

        /// <summary>
        /// Radius of the Circle part of the torus
        /// </summary>
        public float R1 = 1f;

        /// <summary>
        /// Radius of the torus, distance from the center of the torus to the center of the circle
        /// </summary>
        public float R2 = 2;

        /// <summary>
        /// Distance from the screen
        /// </summary>
        public float K2 = 5;

        /// <summary>
        /// Scaling Factor
        /// </summary>
       // public float K1 = width * K2 * 3 / (8 * (R1 + R2));

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


        /* public void Render2 ()
         {
             float K1 = width * K2 * 3 / (8 * (R1 + R2));

             float cosA = (float)Math.Cos(A), sinA = (float)Math.Sin(A);
             float cosB = (float)Math.Cos(B), sinB = (float)Math.Sin(B);

             char[] output = new char[width * height];
             float[] zbuffer = new float[width * height];

             // theta goes around the cross-sectional circle of a torus
             for (float theta = 0; theta < 2 * Math.PI; theta += thetaStep)
             {
                 // precompute sines and cosines of theta
                 float costheta = (float)Math.Cos(theta), sintheta = (float)Math.Sin(theta);

                 // phi goes around the center of revolution of a torus
                 for (float phi = 0; phi < 2 * Math.PI; phi += phiStep)
                 {
                     // precompute sines and cosines of phi
                     float cosphi = (float)Math.Cos(phi), sinphi = (float)Math.Sin(phi);

                     // the x,y coordinate of the circle, before revolving (factored
                     // out of the above equations)
                     float circlex = R2 + R1 * costheta;
                     float circley = R1 * sintheta;

                     // final 3D (x,y,z) coordinate after rotations, directly from
                     // our math above
                     float x = circlex * (cosB * cosphi + sinA * sinB * sinphi)
                               - circley * cosA * sinB;
                     float y = circlex * (sinB * cosphi - sinA * cosB * sinphi)
                               + circley * cosA * cosB;
                     float z = K2 + cosA * circlex * sinphi + circley * sinA;
                     float ooz = 1 / z;  // "one over z"

                     // x and y projection.  note that y is negated here, because y
                     // goes up in 3D space but down on 2D displays.
                     int xp = (int)(width / 2 + K1 * ooz * x);
                     int yp = (int)(height / 2 - K1 * ooz * y);

                     // calculate luminance.  ugly, but correct.
                     float L = cosphi * costheta * sinB - cosA * costheta * sinphi -
                               sinA * sintheta + cosB * (cosA * sintheta - costheta * sinA * sinphi);
                     // L ranges from -sqrt(2) to +sqrt(2).  If it's < 0, the surface
                     // is pointing away from us, so we won't bother trying to plot it.
                     if (L > 0)
                     {
                         // test against the z-buffer.  larger 1/z means the pixel is
                         // closer to the viewer than what's already plotted.
                         if (ooz < zbuffer[xp+ yp * width])
                         {
                             zbuffer[xp + yp * width] = ooz;
                             int luminance_index = (int)(L * 8);
                             // luminance_index is now in the range 0..11 (8*sqrt(2) = 11.3)
                             // now we lookup the character corresponding to the
                             // luminance and plot it in our output:
                             output[xp + yp * width] = ".,-~:;=!*#$@"[luminance_index];
                         }
                     }
                 }
             }

             // now, dump output[] to the screen.
             // bring cursor to "home" location, in just about any currently-used
             // terminal emulation mode
             Console.SetCursorPosition(0, 0);
             for (int j = 0; j < height; j++)
             {
                 for (int i = 0; i < width; i++)
                 {
                     Console.Write(output[i + j * width]);
                 }
                 Console.WriteLine();
             }
         }
 */


        public void Render()
        {
            height = Console.WindowHeight;
             width = Console.WindowWidth;

            float K1 = width * K2 * 3 / (8 * (R1 + R2));

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
                for (float phi = 0; phi < 2 * Math.PI; phi += phiStep)
                {
                    float cosPhi = MathF.Cos(phi);
                    float sinPhi = MathF.Sin(phi);

                    float circleX = R2 + R1 * cosTheta;
                    float circleY = R1 * sinTheta;

                    float x = circleX * (cosB * cosPhi + sinB * sinA * sinPhi) - circleY * cosA * sinB;
                    float y = circleX * (sinB * cosPhi - cosB * sinA * sinPhi) + circleY * cosA * cosB;
                    float z = K2 + circleX * cosA * sinPhi + circleY * sinA;

                    float ooz = 1 / z;

                    int xp = (int)(width / 2 + K1 * ooz * x);
                    int yp = (int)(height / 2 - K1 * ooz * y);

                    int idx = xp + yp * width;

                    float nx = cosTheta;
                    float ny = sinTheta;

                    float Nx = nx * (cosB * cosPhi + sinB * sinA * sinPhi) - ny * cosA * sinB;
                    float Ny = nx * (sinB * cosPhi - cosB * sinA * sinPhi) + ny * cosA * cosB;
                    float Nz = nx * cosA * sinPhi + ny * sinA;


                    float Luminence = Nx * LightX + Ny * LightY + Nz * LightZ;

                    if (idx > length || idx < 0)
                        continue;

                    if (Luminence > 0)
                    {
                        // test against the z-buffer.  larger 1/z means the pixel is
                        // closer to the viewer than what's already plotted.
                        if (ooz > zBuffer[idx])
                        {
                            zBuffer[idx] = ooz;
                            int luminance_index = (int)(((Luminence + MathF.Sqrt(2)) / 2) * 8);
                            // luminance_index is now in the range 0..11 (8*sqrt(2) = 11.3)
                            buf[idx] = ".,-~:;=!*#$@"[luminance_index];
                        }
                    }
                }
            }

            Console.Clear();
            for (int k = 0; k < buf.Length; k++)
            {
                Console.Write(buf[k]);
                if (k % width == width - 1)
                    Console.WriteLine();
            }

            //Thread.Sleep(1000);
        }
    }
}
