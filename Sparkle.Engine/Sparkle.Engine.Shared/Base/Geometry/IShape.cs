namespace Sparkle.Engine.Base.Geometry
{
    using System;
    using Sparkle.Engine.Base;
    using Microsoft.Xna.Framework;
    using Sparkle.Engine.Base.Geometry;

    /// <summary>
    /// Representation of a geometrical shape.
    /// </summary>
	public interface IShape
	{
        /// <summary>
        /// The Axis Aligned Bounding Box of this shape. It is a frame that contains all the shape.
        /// </summary>
		Frame Aabb { get; }

		#region Contains

        /// <summary>
        /// Indicates whether this element contains the given point.
        /// </summary>
        /// <param name="point">The point</param>
        /// <returns></returns>
		bool Contains (Vector2 point);

        /// <summary>
        /// Indicates whether this element contains the given frame.
        /// </summary>
        /// <param name="shape">The frame</param>
        /// <returns></returns>
		bool Contains (Frame shape);

        /// <summary>
        /// Indicates whether this element contains the given circle.
        /// </summary>
        /// <param name="shape">The circle</param>
        /// <returns></returns>
		bool Contains (Circle shape);

        /// <summary>
        /// Indicates whether this element contains the given segment.
        /// </summary>
        /// <param name="shape">The segment</param>
        /// <returns></returns>
		bool Contains (Segment shape);

		#endregion

		#region Intersects

        /// <summary>
        /// Indicates whether this element intersects with the given frame.
        /// </summary>
        /// <param name="shape">The frame</param>
        /// <returns></returns>
		bool Intersects (Frame shape);

        /// <summary>
        /// Indicates whether this element intersects with the given circle.
        /// </summary>
        /// <param name="shape">The circle</param>
        /// <returns></returns>
		bool Intersects (Circle shape);

        /// <summary>
        /// Indicates whether this element intersects with the given segment.
        /// </summary>
        /// <param name="shape"The segment></param>
        /// <returns></returns>
		bool Intersects (Segment shape);

		#endregion
	}
}

