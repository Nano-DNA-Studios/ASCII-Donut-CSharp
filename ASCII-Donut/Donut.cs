
namespace ASCII_Donut
{
    /// <summary>
    /// Represents a Donut that can be rendered to the console
    /// </summary>
    internal class Donut
    {
        /// <summary>
        /// X Position of the Donut
        /// </summary>
        public float XPos { get; set; }

        /// <summary>
        /// Y Position of the Donut
        /// </summary>
        public float YPos { get; set; }

        /// <summary>
        /// Z Position of the Donut
        /// </summary>
        public float ZPos { get; set; }

        /// <summary>
        /// Light Source in the Scene
        /// </summary>
        public Light LightSource { get; private set; }

        /// <summary>
        /// Luminence Values
        /// </summary>
        private string _luminenceValues = ".,-~:;=!*#$@";

        /// <summary>
        /// Step Size of the Theta Angle (The circle making the donut ring)
        /// </summary>
        public float thetaStep = 0.02f;

        /// <summary>
        /// Step Size of the Phi Angle (the donut spin)
        /// </summary>
        public float phiStep = 0.03f;

        /// <summary>
        /// Radius of the Circle part of the torus
        /// </summary>
        public float R1 = 1f;

        /// <summary>
        /// Radius of the torus, distance from the center of the torus to the center of the circle
        /// </summary>
        public float R2 = 4f;

        /// <summary>
        /// Distance from the screen
        /// </summary>
        public float K2 = 10;

        /// <summary>
        /// Scaling Factor
        /// </summary>
        public float K1;

        /// <summary>
        /// Height of the Screen
        /// </summary>  
        private int _height { get; set; }

        /// <summary>
        /// Width of the Screen
        /// </summary>
        private int _width { get; set; }

        /// <summary>
        /// Previous Height of the Screen
        /// </summary>
        private int _lastHeight;

        /// <summary>
        /// Previous Width of the Screen
        /// </summary>
        private int _lastWidth;

        /// <summary>
        /// Angle Around the X Axis
        /// </summary>
        public float A;

        /// <summary>
        /// Angle Around the Z Axis
        /// </summary>
        public float B;

        /// <summary>
        /// Screen Buffer, the text that will represent the displayed donut
        /// </summary>
        private char[] _buf;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Donut(Light lightSource)
        {
            LightSource = lightSource;
            _height = Console.WindowHeight;
            _width = Console.WindowWidth;
            K1 = _width * K2 * 3 / (24 * (R1 + R2));
        }

        /// <summary>
        /// Renders the Donut to the Console
        /// </summary>
        public void Render()
        {
            int length = _width * _height;

            _buf = new char[length];
            float[] zBuffer = new float[length];
            Array.Fill(_buf, ' ');
            Array.Fill(zBuffer, float.MinValue);

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

                    float x = XPos + circleX * x1 - circleY * x2;
                    float y = YPos + circleX * y1 + circleY * y2;
                    float z = K2 + circleX * z1 + circleY * z2;

                    float ooz = 1 / z;

                    int xp = (int)(_width / 2 + K1 * ooz * x);
                    int yp = (int)(_height / 2 - K1 * ooz * y);

                    xp = Math.Clamp(xp, 0, _width - 1);
                    yp = Math.Clamp(yp, 0, _height - 1);

                    int idx = xp + yp * _width;

                    float nx = cosTheta;
                    float ny = sinTheta;

                    float Nx = nx * x1 - ny * x2;
                    float Ny = nx * y1 + ny * y2;
                    float Nz = nx * z1 + ny * z2;

                    float mag = MathF.Sqrt(Nx * Nx + Ny * Ny + Nz * Nz);

                    Nx = Nx / mag;
                    Ny = Ny / mag;
                    Nz = Nz / mag;

                    float Luminence = Nx * LightSource.NormalizedX + Ny * LightSource.NormalizedY + Nz * LightSource.NormalizedZ;

                    if (idx > length || idx < 0)
                        continue;

                    if (ooz > zBuffer[idx])
                    {
                        int luminance_index = (int)((Luminence + 1) * (_luminenceValues.Length) / (2) * LightSource.GetIntensity(x, y, z));
                        luminance_index = Math.Clamp(luminance_index, 0, _luminenceValues.Length - 1);
                        zBuffer[idx] = ooz;
                        _buf[idx] = _luminenceValues[luminance_index];
                    }
                }
            }

            Display();
        }

        /// <summary>
        /// Displays the Donut to the Console, centers it on the console.
        /// </summary>
        private void Display()
        {
            int height = Console.WindowHeight;
            int width = Console.WindowWidth;

            if (height != _lastHeight && width != _lastWidth)
                Console.Clear();

            int startX = (width - _width) / 2;
            int startY = (height - _height) / 2;

            for (int y = 0; y < _height; y++)
            {
                Console.SetCursorPosition(startX, startY + y);
                for (int x = 0; x < _width; x++)
                {
                    int index = y * _width + x;
                    Console.Write(_buf[index]);
                }
            }

            _lastHeight = _height;
            _lastWidth = _width;
        }
    }
}
