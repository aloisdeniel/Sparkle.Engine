using Sparkle.Engine.Tools;

namespace Sparkle.Engine.Core.Entities
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Text;
	using System.Drawing;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Sparkle.Engine.Base;
	using Sparkle.Engine.Base.Dynamics;
	using Sparkle.Engine.Base.Shapes;
	using Sparkle.Engine.Core.Controllers;

	public class Entity : UpdatableBase, Sparkle.Engine.Base.IDrawable, ILoadable, IQuadStorable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Core.Entity"/> class.
		/// </summary>
		public Entity (World world)
		{
			this.World = world;
			this.IsVisible = true;
			this.Position = new Dynamic3 (0.0f, 0.0f, 0.0f);
			this.Rotation = new Dynamic (0.0f);
			this.Scale = new Dynamic2 (1.0f, 1.0f);
			this.Children = new List<Entity> ();
			this.sprites = new Dictionary<string, Sprite> ();
		}

		public Entity () : this (null)
		{

		}

		#region Fields

		private IDynamic<Vector3> position;

		private IDynamic<float> rotation;

		private IDynamic<Vector2> scale;

		private Entity parent;

		private World world;

		private Dictionary<string, Sprite> sprites;

		#endregion

		/// <summary>
		/// Gets the x component value of the current position.
		/// </summary>
		/// <value>The x component value of the current position.</value>
		public float X {
			get { return this.Position.Value.X; }
		}

        /// <summary>
        /// Indicates whether this entity has been removed from the world.
        /// </summary>
        public bool IsAlive
        {
            get;
            private set;
        }

		/// <summary>
		/// Gets the y component value of the current position.
		/// </summary>
		/// <value>The y component value of the current position.</value>
		public float Y {
			get { return this.Position.Value.Y; }
		}

		/// <summary>
		/// Gets the z component value of the current position.
		/// </summary>
		/// <value>The z component value of the current position.</value>
		public float Z {
			get { return this.Position.Value.Z; }
		}

		/// <summary>
		/// Gets the parent entity the current entity is linked to (optionnal).
		/// </summary>
		/// <value>The parent.</value>
		public Entity Parent {
			get { return this.parent; }
			set { 
				if (this.parent != null) {
					this.Parent.Detach (this);
				}

				if (value != null) {
					value.Attach (this);
				}
			}
		}

		/// <summary>
		/// Gets the children entities.
		/// </summary>
		/// <value>The children.</value>
		public List<Entity> Children { get; private set; }

		/// <summary>
		/// Gets the world.
		/// </summary>
		/// <value>The world.</value>
		public World World {
			get { 
				if (this.world == null && this.Parent != null) {
					return this.Parent.World;
				}
				return this.world;
			}
			set { this.world = value; }
		}

		/// <summary>
		/// Gets or sets the size of the bounds of this entity.
		/// </summary>
		/// <value>The size.</value>
		public Sparkle.Engine.Base.Size Size { get; set; }

		public virtual Frame Bounds {
			get { 
				var value = this.AbsolutePosition.Value;
				return new Frame (
					(value.X - this.Size.Width / 2), 
					(value.Y - this.Size.Height / 2), 
					this.Size.Width, 
					this.Size.Height);
			}
		}

		public IDynamic<Vector3> AbsolutePosition {
			get {

				if (this.Parent != null) {

					var parentRotation = this.Parent.Rotation.Value;

					var cos = Math.Cos (parentRotation);
					var sin = Math.Sin (parentRotation);

					var x = this.Parent.Position.Value.X + this.position.Value.X * cos - this.position.Value.Y * sin;
					var y = this.Parent.Position.Value.Y + this.position.Value.Y * sin + this.position.Value.X * cos;
					var z = this.Parent.Position.Value.Z + this.position.Value.Z;

					return new Dynamic3 () {
						Value = new Vector3 ((float)x, (float)y, (float)z),
						Velocity = this.position.Velocity + this.Parent.AbsolutePosition.Velocity,
						Acceleration = this.position.Acceleration + this.Parent.AbsolutePosition.Acceleration,
					};
				}

				return this.position;
			}
		}

		public IDynamic<Vector3> Position {
			get { return this.position; }
			set { this.position = value; }
		}

		public IDynamic<float> AbsoluteRotation {
			get { 

				if (this.Parent != null) {
					var p = this.Parent.AbsoluteRotation;
					return new Dynamic () {
						Value = this.rotation.Value + p.Value,
						Velocity = this.rotation.Velocity + p.Velocity,
						Acceleration = this.rotation.Acceleration + p.Acceleration,
					};
				}

				return this.rotation;
			}
		}

		public IDynamic<float> Rotation {
			get { return this.rotation; }
			set { this.rotation = value; }
		}

		public IDynamic<Vector2> AbsoluteScale {
			get { 
				if (this.Parent != null) {
					return new Dynamic2 () {
						Value = this.scale.Value * this.Parent.AbsoluteScale.Value,
						Velocity = this.scale.Velocity * this.Parent.AbsoluteScale.Velocity,
						Acceleration = this.scale.Acceleration * this.Parent.AbsoluteScale.Acceleration,
					};
				}

				return this.scale;
			}
		}

		public IDynamic<Vector2> Scale {
			get { return this.scale; }
			set { this.scale = value; }
		}

		/// <summary>
		/// Calculate the distance from the specified other entity.
		/// </summary>
		/// <param name="other">Other entity we want the distance from.</param>
		public float Distance (Entity other)
		{
			var thisPosition = this.Position.Value;
			var otherPostion = other.Position.Value;
			return (otherPostion - thisPosition).Length ();
		}

		protected override void DoUpdate (GameTime gameTime)
		{
			this.Position.Update (gameTime);
			this.Rotation.Update (gameTime);
			this.Scale.Update (gameTime);
		}

		#region Sprites

		public Sprite CreateSprite (string textureName, int width, int height)
		{
			var sprite = new Sprite (this, textureName);
			this.sprites.Add (textureName, sprite);
			sprite.Parent = this;
			sprite.World = this.world;
			if (sprite.World != null) {
				sprite.World.AddEntity (sprite);
			}
			sprite.Size = new Sparkle.Engine.Base.Size (width, height);
			return sprite;
		}

		public Sprite GetSprite (string name)
		{
			return sprites [name];
		}

		#endregion

		/// <summary>
		/// Attach the specified child.
		/// </summary>
		/// <param name="child">Child.</param>
		public void Attach (Entity child)
		{
			child.parent = this;
			this.Children.Add (child);
		}

		/// <summary>
		/// Detach the specified child.
		/// </summary>
		/// <param name="child">Child.</param>
		public bool Detach (Entity child)
		{
			if (this.Children.Contains (child)) {
				child.parent = null;
				this.Children.Remove (child);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Kills this entity and all its children.
		/// </summary>
		public void Kill ()
		{
			if (World != null) {
				this.World.RemoveEntity (this);
                this.IsAlive = false;
			}

			foreach (var child in this.Children) {
				child.Kill ();
			}
		}

		#region ILoadable implementation

		public virtual void LoadContent (Microsoft.Xna.Framework.Content.ContentManager content)
		{

		}

		public virtual void UnloadContent (Microsoft.Xna.Framework.Content.ContentManager content)
		{

		}

		#endregion

		#region IDrawable implementation

		public void Draw (SpriteBatch sb)
		{
			if (this.IsVisible) {
				this.DoDraw (sb);
			}
		}

		protected virtual void DoDraw (SpriteBatch sb)
		{
		
		}

		public void DrawBounds (SpriteBatch sb)
		{
			if (this.IsVisible) {

				if (this.Bounds.Width > 0 && this.Bounds.Height > 0)
					sb.DrawFrame (Microsoft.Xna.Framework.Color.OrangeRed, this.Bounds);

				var dir = new Vector2 ((float)Math.Cos (-this.AbsoluteRotation.Value), -(float)Math.Sin (-this.AbsoluteRotation.Value)) * 15;

				sb.DrawVector (Microsoft.Xna.Framework.Color.Black, this.Bounds.Center, this.Bounds.Center + dir);

			}
		}

		public int DrawOrder { get; set; }

		public bool IsVisible { get; set; }

		#endregion
	}
}