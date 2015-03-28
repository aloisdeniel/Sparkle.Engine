using Microsoft.Xna.Framework.Graphics;

namespace Sparkle.Engine.Core
{
	using System;
	using Microsoft.Xna.Framework;
	using System.Collections.Generic;
	using Sparkle.Engine.Base;
	using Sparkle.Engine.Base.Shapes;
	using Sparkle.Engine.Core.Entities;
	using Sparkle.Engine.Core.Controllers;
    using Sparkle.Engine.Base.Dynamics;
    using Sparkle.Engine.Tools;
    using Sparkle.Engine.Core.Tiles;

	public class World : Entity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Sparkle.Engine.Core.World"/> class.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		/// <param name="cameraSize">Camera size.</param>
        public World (float width, float height, Size cameraSize)
		{
            this.Background = new DynamicColor(Color.Black);
			this.Controllers = new List<IController> ();
			this.MainSamplerState = SamplerState.PointClamp;
			this.Size = new Size (width, height);
			this.entities = new EntityTree (this.Bounds);
			this.Camera = new Camera (width / 2, height / 2, cameraSize.Width, cameraSize.Height);
            this.entityPrototypes = new Dictionary<Enum, Func<Entity>>();
            this.Tiles = new TileMap(this, this.Bounds);
		}

        public TileMap Tiles { get; private set; }

		/// <summary>
		/// Gets the bounds in which entities of the world are updated (if no size had been precised the entire world is updated).
		/// </summary>
		/// <value>The update bounds.</value>
		public Frame UpdateArea {
			get {
				if (this.UpdateAreaSize == null)
					return this.Bounds;

				var c = this.Camera.Position.Value;
				var s = (Size)this.UpdateAreaSize;
				var w2 = s.Width / 2;
				var h2 = s.Height / 2;
				return new Frame (c.X - w2, c.Y - h2, c.X + w2, c.Y + h2);
			}
		}

		public SamplerState MainSamplerState { get; set; }

		/// <summary>
		/// Update area size around camera center. If null, all the entities of the world are updated on each loop.
		/// </summary>
		public Size? UpdateAreaSize { get; set; }

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public DynamicColor Background { get; set; }

		/// <summary>
		/// Gets or sets the main camera.
		/// </summary>
		/// <value>The camera.</value>
		public Camera Camera { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the debugging is activated.
		/// </summary>
		/// <value><c>true</c> if this instance is debugging; otherwise, <c>false</c>.</value>
		public bool IsDebugging { get; set; }

		/// <summary>
		/// The entities of the world.
		/// </summary>
		private EntityTree entities;

		/// <summary>
		/// The entity prototypes.
		/// </summary>
		private Dictionary<Enum, Func<Entity>> entityPrototypes;


		public List<IController> Controllers { get; private set; }

		/// <summary>
		/// Registers the entity prototype.
		/// </summary>
		/// <returns>The entity.</returns>
		/// <param name="name">Name.</param>
		/// <param name="entity">Entity.</param>
        public void RegisterEntity(Enum identifier, Func<Entity> entity)
		{
			this.entityPrototypes [identifier] = () => this.AddEntity (entity ());
		}

        public void RegisterEntity<E>(Enum identifier) where E : Entity
		{
			this.RegisterEntity (identifier, () => (Entity)Activator.CreateInstance (typeof(E), new object[] { this }));
		}

		/// <summary>
		/// Spawns the entity type.
		/// </summary>
		/// <returns>The entity.</returns>
		/// <param name="name">Name.</param>
        public Entity SpawnEntity(Enum identifier, float x, float y)
		{
			if (!this.entityPrototypes.ContainsKey (identifier))
				return null;

			var entity = this.entityPrototypes [identifier] ();
			entity.Position.Value = new Vector3 (x, y, 0);

			return entity;
		}

		/// <summary>
		/// Adds the entity to the world.
		/// </summary>
		/// <returns>The entity.</returns>
		/// <param name="entity">Entity.</param>
		public Entity AddEntity (Entity entity)
		{
			if (entity == null)
				return null;

			entity.World = this;
			this.entities.Add (entity);
			return entity;
		}

		public IEnumerable<Entity> FindEntities (Func<Entity,bool> predicat)
		{
			return this.entities.SearchObject (predicat);
		}

		/// <summary>
		/// Removes the entity from the world.
		/// </summary>
		/// <returns><c>true</c>, if entity was removed, <c>false</c> otherwise.</returns>
		/// <param name="entity">Entity.</param>
		public bool RemoveEntity (Entity entity)
		{
			entity.World = null;
			return this.entities.Remove (entity);
		}

		public override void LoadContent (Microsoft.Xna.Framework.Content.ContentManager content)
		{
			base.LoadContent (content);

            if (this.Tiles != null)
            {
                this.Tiles.LoadContent(content);
            }

			var allentities = this.entities.GetAllObjects ();

			foreach (var entity in allentities) {
				entity.LoadContent (content);
			}
		}

		public override void UnloadContent (Microsoft.Xna.Framework.Content.ContentManager content)
		{
            base.UnloadContent(content);

            if (this.Tiles != null)
            {
                this.Tiles.UnloadContent(content);
            }

			var allentities = this.entities.GetAllObjects ();

			foreach (var entity in allentities) {
				entity.UnloadContent (content);
			}
		}

		/// <summary>
		/// Updates all entities of the world that are into the bounds of the camera.
		/// </summary>
		/// <param name="gameTime">Game time.</param>
		protected override void DoUpdate (GameTime gameTime)
		{
			this.Camera.Update (gameTime);

            this.Background.Update(gameTime);

			foreach (var controller in this.Controllers) {
				controller.Update (gameTime);
			}

			var updated = this.entities.GetObjects (this.UpdateArea);

            this.updateNumber = updated.Count;

			foreach (var entity in updated) {

				//Updates the entity.
				entity.Update (gameTime);

				//Updates the quad tree after a potential bounds modification.
				this.entities.Move (entity);
			}
		}

        private int updateNumber;

		protected override void DoDraw (Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
		{
            // 1. Tiles

            if(this.Tiles != null)
            {
                sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, this.MainSamplerState, null, null, null, this.Camera.Transform);
                this.Tiles.Draw(sb);
                sb.End();
            }

            // 2. Entities

			sb.Begin (SpriteSortMode.BackToFront, BlendState.AlphaBlend, this.MainSamplerState, null, null, null, this.Camera.Transform);

			var drawn = this.entities.GetObjects (this.Camera.Bounds);

			foreach (var entity in drawn) {
				entity.Draw (sb);
			}	

			sb.End ();

            // 2. Debugging tools

            if (this.IsDebugging)
            {
                sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, this.Camera.Transform);
                
				foreach (var entity in drawn) {
					entity.DrawBounds (sb);
				}

                sb.DrawFrame(Microsoft.Xna.Framework.Color.Chocolate, this.Camera.Bounds);

                sb.End();
			}
		}
	}
}