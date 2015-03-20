using System;
using Sparkle.Engine.Core;
using Sparkle.Engine.Base.Dynamics;
using Sparkle.Engine.Base;
using Sparkle.Engine.Core.Entities;

namespace Sparkle.Engine.Samples.Shared
{
	public class OrangeGuy : Entity
	{
		public OrangeGuy (World w) : base (w)
		{
			var sprite = this.CreateSprite ("spritesheet", 16 * 5, 32 * 5);

			sprite.AddAnimation ("idle", 6, 2).From (0, 1).To (5).WithInterval (100);
			sprite.AddAnimation ("jump", 6, 2).From (0, 1).To (5).During (1000);

			sprite.PlayAnimation ("idle", Sparkle.Engine.Base.RepeatMode.Loop);

            this.Position.Animation = new DynamicAnimation3(
                new Microsoft.Xna.Framework.Vector3(0, 0, 0),
                new Microsoft.Xna.Framework.Vector3(400, 400, 0), 3000);
            this.Position.Animation.Start(RepeatMode.LoopWithReverse);
		}
	}
}

