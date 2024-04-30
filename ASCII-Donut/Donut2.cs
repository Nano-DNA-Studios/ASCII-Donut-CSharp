using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Donut
{
    internal class Donut2
    {
        public float positionX = 0;
        public float positionY = 0;
        public float positionZ = -10;

        public float lightX = 10;
        public float lightY = 0;
        public float lightZ = 0;

        float circleRadius = 0.5f;
        float torusRadius = 5f;

        int height = 20;
        int width = 80;

        public float Y = 0, B = 0, A = 0;

        string luminenceValues = ".,-~:;=!*#$@";

        public void Render()
        {
            int width = 80;
            int height = 22;
            char[] buf = new char[width * height];
            float[] zBuffer = new float[width * height];
            Array.Fill(buf, ' ');
            Array.Fill(zBuffer, 0);

            float cosB = MathF.Cos(B), sinB = MathF.Sin(B);
            float cosA = MathF.Cos(A), sinA = MathF.Sin(A);

            for (float theta = 0; theta < 2 * Math.PI; theta += 0.07f)
            {
                for (float phi = 0; phi < 2 * Math.PI; phi += 0.02f)
                {
                    float sinPhi = (float)Math.Sin(phi);
                    float cosPhi = (float)Math.Cos(phi);
                    float sinTheta = (float)Math.Sin(theta);
                    float cosTheta = (float)Math.Cos(theta);

                    float circleX = cosPhi + 2;
                    float circleY = sinPhi;

                    float x = circleX * (cosTheta * cosB - sinTheta * sinA * sinB);
                    float y = circleY * cosA + circleX * sinTheta * sinA;
                    float z = sinB * sinTheta * circleX * cosA + circleY * sinA + 5;
                    float ooz = 1 / z;

                    int xp = (int)(width / 2 + 1.5 * ooz * x * width);
                    int yp = (int)(height / 2 - ooz * y * height);
                    int idx = xp + yp * width;

                    if (idx > 0 && idx < width * height)
                    {
                        if (ooz > zBuffer[idx])
                        {
                            zBuffer[idx] = ooz;
                            int luminance = (int)(-8 * ((sinTheta * sinPhi - cosTheta * cosPhi * cosPhi) * cosB - cosTheta * cosPhi * sinPhi - sinTheta * sinPhi * sinB));
                            if (luminance > 0)
                            {
                                buf[idx] = ".,-~:;=!*#$@"[luminance];
                            }
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
        }

    }

}
