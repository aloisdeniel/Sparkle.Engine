using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sparkle.Engine.Core.Components;
using Sparkle.Engine.Core.Entities;
using Sparkle.Engine.Samples.Shared.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Entities
{
    public class Character : Entity
    {
        public Character(float x, float y)
        {
            var inputs = this.AddComponent<Input>();
            inputs.ObserveKey(MoveBehavior.UpCommand,Keys.Up);
            inputs.ObserveKey(MoveBehavior.DownCommand,Keys.Down);
            inputs.ObserveKey(MoveBehavior.RightCommand,Keys.Right);
            inputs.ObserveKey(MoveBehavior.LeftCommand,Keys.Left);

            this.AddComponent<MoveBehavior>();

            var sprite = this.AddComponent<Sprite>();
            sprite.TextureName = "darthvader";
            sprite.Width = 32;
            sprite.Height = 48;

            var animation = this.AddComponent<SpriteAnimation>();
            animation.Interval = 200;
            animation.Add(MoveBehavior.WalkDownAnim , 32, 48, new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0));
            animation.Add(MoveBehavior.WalkLeftAnim, 32, 48, new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1));
            animation.Add(MoveBehavior.WalkRightAnim, 32, 48, new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2));
            animation.Add(MoveBehavior.WalkUpAnim, 32, 48, new Point(0, 3), new Point(1, 3), new Point(2, 3), new Point(3, 3));

            var body = this.AddComponent<Body>();
            body.Position = new Microsoft.Xna.Framework.Vector3(x, y, 0);

        }
    }
}
