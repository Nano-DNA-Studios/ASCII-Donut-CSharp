
namespace ASCII_Donut
{
    /// <summary>
    /// Represents a Point Light Source
    /// </summary>
    internal class Light
    {
        /// <summary>
        /// Light Position X
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Light Position Y
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Light Position Z
        /// </summary>
        public float Z { get; private set; }

        /// <summary>
        /// The Normalized X Value of the Light Position
        /// </summary>
        public float NormalizedX { get; private set; }

        /// <summary>
        /// Normalized Y Value of the Light Position
        /// </summary>
        public float NormalizedY { get; private set; }

        /// <summary>
        /// Normalized Z Value of the Light Position
        /// </summary>
        public float NormalizedZ { get; private set; }

        /// <summary>
        /// Magnitude of the Light Position
        /// </summary>
        public float Magnitude { get; private set; }

        /// <summary>
        /// Strength of the Light
        /// </summary>
        public float Strength { get; private set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="x"> The X Position of the Light </param>
        /// <param name="y"> The Y Position of the Light </param>
        /// <param name="z"> The Z Position of the Light </param>
        /// <param name="strength"> The Strength of the Light </param>
        public Light(float x, float y, float z, float strength)
        {
            X = x;
            Y = y;
            Z = z;
            Strength = strength;
            Magnitude = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            NormalizedX = Magnitude > 0 ? X / Magnitude : X;
            NormalizedY = Magnitude > 0 ? Y / Magnitude : Y;
            NormalizedZ = Magnitude > 0 ? Z / Magnitude : Z;
        }

        /// <summary>
        /// Calculates the intensity of the light on an object at a given point based off the Inverse Square Law
        /// </summary>
        /// <param name="ox"> Objects X Position </param>
        /// <param name="oy"> Objects Y Position </param>
        /// <param name="oz"> Objects Z Position </param>
        /// <returns> The Intensity Value at a point on an object. </returns>
        public float GetIntensity(float ox, float oy, float oz)
        {
            float rx = ox - X;
            float ry = oy - Y;
            float rz = oz - Z;

            float r = (float)Math.Sqrt(rx * rx + ry * ry + rz * rz);

            return r > 0 ? Strength / (4 * MathF.PI * r) : Strength / (4 * MathF.PI);
        }

        /// <summary>
        /// Updates the Position of the Light
        /// </summary>
        /// <param name="x"> New X Position of the Light </param>
        /// <param name="y"> New Y Position of the Light </param>
        /// <param name="z"> New Z Position of the Light </param>
        public void UpdatePosition(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            Magnitude = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            NormalizedX = Magnitude > 0 ? X / Magnitude : X;
            NormalizedY = Magnitude > 0 ? Y / Magnitude : Y;
            NormalizedZ = Magnitude > 0 ? Z / Magnitude : Z;
        }

        /// <summary>
        /// Updates the Strength of the Light
        /// </summary>
        /// <param name="strength"> New Light Strength Value </param>
        public void UpdateStrength(float strength)
        {
            Strength = strength;
        }
    }
}
