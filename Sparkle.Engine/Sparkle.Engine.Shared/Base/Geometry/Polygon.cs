namespace Sparkle.Engine.Shapes
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A polygon shape.
    /// </summary>
	public class Polygon
	{
		public Polygon (params Vector2[] vertices)
		{
			this.Vertices = vertices;
			this.calculateNormals ();
		}

		public Polygon (Rectangle box)
			: this (new Vector2[] { 
				new Vector2 (box.X, box.Y),
				new Vector2 (box.X + box.Width, box.Y),
				new Vector2 (box.X + box.Width, box.Y + box.Height),
				new Vector2 (box.X, box.Y + box.Height),

			})
		{
		}

		public Vector2[] Vertices { get; private set; }

		public Vector2[] Normals { get; private set; }

		private void calculateNormals ()
		{
			this.Normals = new Vector2[Vertices.Length];

			for (int i = 0; i < Vertices.Length; ++i) {
				Vector2 face = Vertices [(i + 1) % Vertices.Length] - Vertices [i];
				face.Normalize ();
				this.Normals [i] = face;
			}
		}
	}
}

