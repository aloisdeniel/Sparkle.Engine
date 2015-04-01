namespace Sparkle.Engine.Core.Components
{
    using Sparkle.Engine.Base.Geometry;

    /// <summary>
    /// A component that represents the bounding box of the instance. It can be used to optimize drawn, or updated areas.
    /// </summary>
    public class Bounds : Component
    {
        public Frame Frame { get; set; }
    }
}