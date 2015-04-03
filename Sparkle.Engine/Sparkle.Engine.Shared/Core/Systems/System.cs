namespace Sparkle.Engine.Core.Systems
{
    /// <summary>
    /// A system has the responsability for updating a set of entity components.
    /// </summary>
    public abstract class System
    {
        public SparkleGame Game { get; set; }

        /// <summary>
        /// Indicates whether the instance should be updated or not.
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
