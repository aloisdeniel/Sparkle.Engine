using System;
using Sparkle.Engine.Core;
using Microsoft.Xna.Framework;
using Sparkle.Engine.Core.Entities;

namespace Sparkle.Engine.Samples.Shared
{
	public class ExampleEntity : Entity
	{
		public ExampleEntity (World w) : base (w)
		{
			this.CreateSprite ("profilepic", 100, 100);
			sprite = this.CreateSprite ("gradient", 40, 40);
			sprite.Position.Value = new Microsoft.Xna.Framework.Vector3 (20, 20, 0);
			sprite.Rotation.Acceleration = 0.5f;
			sprite.Rotation.MaxVelocity = 0.8f;

			this.Rotation.Acceleration = 0.5f;
			this.Rotation.MaxVelocity = 0.4f;
			this.Position.Acceleration = new Vector3 (1, 1, 0);
			this.Position.MaxVelocity = new Vector3 (5, 5, 0);
			this.Position.MaxValue = new Vector3 (300, 300, 0);
		}

		private Sprite sprite;

		protected override void DoUpdate (Microsoft.Xna.Framework.GameTime gameTime)
		{
			base.DoUpdate (gameTime);
		}

		protected override void DoDraw (Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
		{
			base.DoDraw (sb);
		}

	}
}

