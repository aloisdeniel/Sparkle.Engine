using System;
using Sparkle.Engine.Core;
using Sparkle.Engine.Core.Entities;

namespace Sparkle.Engine.Samples.Shared
{
	public class GreenGuy : Entity
	{
		public GreenGuy (World w) : base (w)
		{
			var sprite = this.CreateSprite ("spritesheet", 16 * 5, 32 * 5);

			sprite.AddAnimation ("idle", 6, 2).From (0, 0).To (2).WithInterval (100);
			sprite.AddAnimation ("jump", 6, 2).From (0, 0).To (5).During (1000);

			sprite.PlayAnimation ("idle", Sparkle.Engine.Base.RepeatMode.LoopWithReverse);

            this.Position.Friction = new Microsoft.Xna.Framework.Vector3(0.14f, 0.14f, 0.14f);

			this.Rotation.Acceleration = 1;
			this.Rotation.MaxVelocity = 2;
		}
	}
}

