namespace Sparkle.Engine.Core.Entities
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Sparkle.Engine.Base;
	using Sparkle.Engine.Base.Shapes;

	/// <summary>
	/// Represents a visible entity with an associated texture.
	/// </summary>
	public class Sprite : Entity
	{
		public Sprite (Entity parent, string texture) : base ()
		{
			this.Parent = parent;
			this.TextureName = texture;
			this.Animations = new Dictionary<string, SpriteAnimation> ();
		}

		/// <summary>
		/// Center of this sprite.
		/// </summary>
		public Vector2? Center { get; set; }

		/// <summary>
		/// The subarea from the texture that will be drawn into destination.
		/// </summary>
		public Rectangle? SourceArea { get; set; }

		/// <summary>
		/// The drawn area on the screen.
		/// </summary>
		public Frame DestinationArea { get; set; }

		#region Texture

		/// <summary>
		/// The name of the texture.
		/// </summary>
		public String TextureName { get; private set; }

		/// <summary>
		/// The texture size.
		/// </summary>
		public Size TextureSize { get; private set; }

		/// <summary>
		/// Texture of the sprite.
		/// </summary>
		public Texture2D Texture { get; private set; }

		#endregion

		#region Animation

		/// <summary>
		/// Current sprite animation (can be null).
		/// </summary>
		public SpriteAnimation CurrentAnimation { get; private set; }

		/// <summary>
		/// All registered animations.
		/// </summary>
		private Dictionary<string, SpriteAnimation> Animations { get; set; }

		/// <summary>
		/// Adds a sprite animation.
		/// </summary>
		/// <param name="name">Name of the animation</param>
		/// <param name="columns">Number of columns in the animation</param>
		/// <param name="rows">Number of rows in the animation</param>
		/// <returns></returns>
		public SpriteAnimation AddAnimation (string name, int columns, int rows)
		{
			var result = new SpriteAnimation (this, columns, rows);
			this.Animations [name] = result;

			if (this.CurrentAnimation == null)
				this.CurrentAnimation = result;

			return result;
		}

		/// <summary>
		/// Sets the current animation and plays it.
		/// </summary>
		/// <param name="name">Name of the animation</param>
		/// <param name="mode">Repeat mode</param>
		public void PlayAnimation (string name, RepeatMode mode = RepeatMode.Once)
		{
			this.CurrentAnimation = this.Animations [name];
			this.CurrentAnimation.Play (mode);
		}

		/// <summary>
		/// Plays the current animation.
		/// </summary>
		/// <param name="mode">Repeat mode</param>
		public void PlayAnimation (RepeatMode mode)
		{
			if (this.CurrentAnimation != null) {
				this.CurrentAnimation.Play (mode);
			}
		}

		/// <summary>
		/// Plays the current animation.
		/// </summary>
		public void PlayAnimation ()
		{
			if (this.CurrentAnimation != null) {
				this.CurrentAnimation.Play ();
			}
		}

		/// <summary>
		/// Set the current animation.
		/// </summary>
		/// <param name="name"></param>
		public void SetAnimation (string name)
		{
			this.CurrentAnimation = this.Animations [name];
			this.CurrentAnimation.Stop ();
		}

		/// <summary>
		/// Stops the current animation.
		/// </summary>
		public void StopAnimation ()
		{
			if (this.CurrentAnimation != null) {
				this.CurrentAnimation.Stop ();
			}
		}

		/// <summary>
		/// Pauses the current animation.
		/// </summary>
		public void PauseAnimation ()
		{
			if (this.CurrentAnimation != null) {
				this.CurrentAnimation.Pause ();
			}
		}

		#endregion

		/// <summary>
		/// Updates the entity.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void DoUpdate (GameTime gameTime)
		{
			base.DoUpdate (gameTime);

			var width = this.Size.Width * this.AbsoluteScale.Value.X;
			var height = this.Size.Height * this.AbsoluteScale.Value.Y;

			this.DestinationArea = new Frame (
				(this.AbsolutePosition.Value.X),
				(this.AbsolutePosition.Value.Y),
				width,
				height);

			if (this.CurrentAnimation != null) {
				this.CurrentAnimation.Update (gameTime);
				this.SourceArea = this.CurrentAnimation.Source;
			}

		}

		/// <summary>
		/// Loads the content.
		/// </summary>
		/// <param name="content"></param>
		public override void LoadContent (Microsoft.Xna.Framework.Content.ContentManager content)
		{
			this.Texture = content.Load<Texture2D> (TextureName);
			this.TextureSize = new Size (this.Texture.Width, this.Texture.Height);
            
			if (this.SourceArea == null)
				this.SourceArea = new Rectangle (0, 0, (int)this.TextureSize.Width, (int)this.TextureSize.Height);
		}

		/// <summary>
		/// Unloads the content.
		/// </summary>
		/// <param name="content"></param>
		public override void UnloadContent (Microsoft.Xna.Framework.Content.ContentManager content)
		{
			this.Texture.Dispose ();
		}

		public Vector2 RotationCenter {
			get {
				var source = (Rectangle)SourceArea;

				return (this.Center == null ? new Vector2 (source.Width, source.Height) / 2 : (Vector2)this.Center);
			}
		}

		/// <summary>
		/// Draw the sprite onto the screen.
		/// </summary>
		/// <param name="sb"></param>
		protected override void DoDraw (SpriteBatch sb)
		{
			base.DoDraw (sb);

			sb.Draw (this.Texture,
				this.DestinationArea.Rectangle, 
				((Rectangle)this.SourceArea), 
				Color.White, 
				this.AbsoluteRotation.Value,
				this.RotationCenter, 
				SpriteEffects.None,
				this.AbsolutePosition.Value.Z);

		}
	}
}
