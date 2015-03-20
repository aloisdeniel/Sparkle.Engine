namespace Sparkle.Engine.Core
{
	using Microsoft.Xna.Framework;
	using Sparkle.Engine.Base;
	using Sparkle.Engine.Base.Shapes;
    using Sparkle.Engine.Core.Entities;

	/// <summary>
	/// A world camera object, that is responsible for defining the current visible area of the world.
	/// </summary>
	public class Camera : Entity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Core.Camera"/> class.
		/// </summary>
		public Camera (float x, float y, float width, float height)
		{
			this.Position.Value = new Microsoft.Xna.Framework.Vector3 (x, y, 1);
			this.Size = new Size (width, height);
		}

		public float Zoom {
			get {
				return 1.0f / this.Z;
			}
		}


		/// <summary>
		/// Gets the world transform matrix for this camera.
		/// </summary>
		/// <value>The transform.</value>
		public Matrix Transform { 
			get { 
				return Matrix.CreateTranslation (new Vector3 (-1 * this.X, -1 * this.Y, 0))
				* Matrix.CreateRotationZ (this.Rotation.Value)
				* Matrix.CreateScale (new Vector3 (this.Zoom, this.Zoom, 0))
				* Matrix.CreateTranslation (new Vector3 (Size.Width * 0.5f, Size.Height * 0.5f, 0)); 
			} 
		}

		/// <summary>
		/// Converts a screen position into a world position.
		/// </summary>
		/// <returns>The world position.</returns>
		/// <param name="screenposition">Screenposition.</param>
		public Vector2 GetWorldPosition (Vector2 screenposition)
		{
			return Position.Value.ToVector2 () + (screenposition - new Vector2 (Size.Width, Size.Height) / 2) / this.Zoom;
		}

		/// <summary>
		/// Converts a world position into a screen position.
		/// </summary>
		/// <returns>The screen position.</returns>
		/// <param name="worldposition">Worldposition.</param>
		public Vector2 GetScreenPosition (Vector2 worldposition)
		{
			Vector2 v = (worldposition - Position.Value.ToVector2 ()) * this.Zoom + (new Vector2 (Size.Width, Size.Height) / 2);

			if (v.X < 0 || v.X > Size.Width || v.Y < 0 || v.Y > Size.Height) {
				return new Vector2 (-1, -1);
			}

			return v;
		}
	}
}

