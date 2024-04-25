namespace ASCII_Donut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*float Y = 0, B = 0, A = 0;
            while (true)
            {
                int height = 22;
                int width = 80;

                int length = width * height;
                float circleRadius = 0.3f;
                float torusRadius = 1;

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

                for (float theta = 0; theta < 2 * Math.PI; theta += 0.01f)
                {
                    for (float phi = 0; phi < 2 * Math.PI; phi += 0.01f)
                    {
                        float sinPhi = MathF.Sin(phi);
                        float cosPhi = MathF.Cos(phi);
                        float sinTheta = MathF.Sin(theta);
                        float cosTheta = MathF.Cos(theta);

                        float circleX = cosPhi * (cosTheta * circleRadius + torusRadius);
                        float circleY = sinTheta * circleRadius;
                        float circleZ = sinPhi * torusRadius;

                        float finalX = R1 * circleX + R2 * circleY + R3 * circleZ;
                        float finalY = R4 * circleX + R5 * circleY + R6 * circleZ;
                        float finalZ = R7 * circleX + R8 * circleY + R9 * circleZ;

                        if (finalZ == 0) continue; // Avoid division by zero

                        float invZ = 1 / finalZ;

                        int xp = (int)(width / 2 + 5 * invZ * finalX * width);
                        int yp = (int)(height / 2 - invZ * finalY * height);
                        if (xp < 0 || xp >= width || yp < 0 || yp >= height) continue; // Ensure the indices are within the screen bounds

                        int idx = xp + yp * width;

                        if (idx >= 0 && idx < width * height)
                        {
                            if (invZ > zBuffer[idx])
                            {
                                zBuffer[idx] = invZ;
                                int luminance = (int)(12 * ((sinTheta * sinPhi - cosTheta * cosPhi * cosPhi) * cosB - cosTheta * cosPhi * sinPhi - sinTheta * sinPhi * sinB));
                                luminance = Math.Clamp(luminance, 0, 11); // Clamp luminance to valid range
                                buf[idx] = ".,-~:;=!*#$@"[luminance];
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

               // Y += 0.02f;
                A += 0.02f;
               // B += 0.08f;
            }*/




            float torusX = 0, torusY = 0, torusZ = -3;

            float Y = 0, B = 0, A = 0;
            while (true)
            {
                //int height = Console.WindowHeight;
                //int width = Console.WindowWidth;

                 int height = 22;
                 int width = 80;

                int length = width * height;
                float circleRadius = 0.3f;
                float torusRadius = 1f;

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

                for (float theta = 0; theta < 2 * Math.PI; theta += 0.02f)
                {
                    for (float phi = 0; phi < 2 * Math.PI; phi += 0.02f)
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
                        float finalX = R1 * circleX + R2 * circleY + R3 * circleZ + torusX;
                        float finalY = R4 * circleX + R5 * circleY + R6 * circleZ + torusY;
                        float finalZ = R7 * circleX + R8 * circleY + R9 * circleZ + torusZ;

                        if (finalZ == 0) continue;
                        float invZ = 1 / finalZ;

                        int xp = (int)(width / 2 + 5 * invZ * finalX * width);
                        int yp = (int)(height / 2 - invZ * finalY * height);

                        if (xp < 0 || xp >= width || yp < 0 || yp >= height) continue; // Ensure the indices are within the screen bounds
                        int idx = xp + yp * width;

                        if (idx > 0 && idx < width * height)
                        {
                            if (invZ > zBuffer[idx])
                            {
                                //Light is probably at 1,1,1
                                zBuffer[idx] = invZ;
                                int luminance = (int)(12 * ((sinTheta * sinPhi - cosTheta * cosPhi * cosPhi) * cosB - cosTheta * cosPhi * sinPhi - sinTheta * sinPhi * sinB));
                                luminance = Math.Clamp(luminance, 0, 11);
                                buf[idx] = ".,-~:;=!*#$@"[luminance];
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


                Y += 0.1f;
                //A += 0.04f;
                //B += 0.08f;
            }
        }
    }
}
