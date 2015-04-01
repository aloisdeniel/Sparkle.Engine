namespace Sparkle.Engine.Base
{
    public class Identifier
    {
        /// <summary>
        /// Lock root.
        /// </summary>
        private static object root = new object();

        /// <summary>
        /// Last generated identifier.
        /// </summary>
        private static int current = 0;

        /// <summary>
        /// Generates a unique identifier.
        /// </summary>
        /// <returns></returns>
        public static int Generate()
        {
            lock(root)
            {
                return current++;
            }
        }
    }
}
