
namespace ASCII_Donut
{
    internal class DonutTest2
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

    internal class DonutTest
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
            int length = width * height;

            char[] buf = new char[length];
            float[] zBuffer = new float[length];
            Array.Fill(buf, ' ');
            Array.Fill(zBuffer, float.MinValue);

            float cosY = MathF.Cos(Y), sinY = MathF.Sin(Y);
            float cosB = MathF.Cos(B), sinB = MathF.Sin(B);
            float cosA = MathF.Cos(A), sinA = MathF.Sin(A);

            //Rotation Matrix
            float R1 = cosA * cosB;
            float R2 = cosA * sinB * sinY - sinA * cosY;
            float R3 = cosA * sinB * cosY + sinA * sinY;
            float R4 = sinA * cosB;
            float R5 = sinA * sinB * sinY + cosA * cosY;
            float R6 = sinA * sinB * cosY - cosA * sinY;
            float R7 = -sinB;
            float R8 = cosB * sinY;
            float R9 = cosB * cosY;

            /* float R1 = cosA * cosB;
             float R2 = -sinA;
             float R3 = cosA * sinB;
             float R4 = sinA * cosB;
             float R5 = cosA;
             float R6 = sinA * sinB;
             float R7 = -sinB;
             float R8 = 0;
             float R9 = cosB;*/

            for (float theta = 0; theta < 2 * Math.PI; theta += 0.01f)
            {
                for (float phi = 0; phi < 2 * Math.PI; phi += 0.01f)
                {
                    //Calculate Sins and Cosines
                    float sinPhi = (float)Math.Sin(phi);
                    float cosPhi = (float)Math.Cos(phi);
                    float sinTheta = (float)Math.Sin(theta);
                    float cosTheta = (float)Math.Cos(theta);

                    //Calculate the 3D coordinates without world rotation
                    float circleX = cosPhi * (cosTheta * circleRadius + torusRadius);
                    float circleY = sinTheta * circleRadius;
                    float circleZ = sinPhi * torusRadius;

                    //Calculate the 3D coordinates with world rotation
                    float finalX = R1 * circleX + R2 * circleY + R3 * circleZ + positionX;
                    float finalY = R4 * circleX + R5 * circleY + R6 * circleZ + positionY;
                    float finalZ = R7 * circleX + R8 * circleY + R9 * circleZ + positionZ;

                    float nx = cosPhi * cosTheta;
                    float ny = sinTheta;
                    float nz = sinPhi;

                    nx = R1 * nx + R2 * ny + R3 * nz;
                    ny = R4 * nx + R5 * ny + R6 * nz;
                    nz = R7 * nx + R8 * ny + R9 * nz;

                    if (finalZ == 0) continue;
                    float invZ = 1 / finalZ;

                    int xp = (int)(width / 2 + 10 * invZ * finalX * width);
                    int yp = (int)(height / 2 - invZ * finalY * height);

                    if (xp < 0 || xp >= width || yp < 0 || yp >= height) continue; // Ensure the indices are within the screen bounds
                    int idx = xp + yp * width;

                    if (idx > 0 && idx < width * height)
                    {
                        if (invZ > zBuffer[idx])
                        {


                            // Dot product with light direction, assume light direction as normalized
                            float dotProduct = nx * lightX + ny * lightY + nz * lightZ;
                            int luminance = (int)(luminenceValues.Length * (dotProduct + 1) / 2.0); // Normalize and scale to character index

                            luminance = Math.Clamp(luminance, 0, luminenceValues.Length - 1);
                            buf[idx] = luminenceValues[luminance];
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
