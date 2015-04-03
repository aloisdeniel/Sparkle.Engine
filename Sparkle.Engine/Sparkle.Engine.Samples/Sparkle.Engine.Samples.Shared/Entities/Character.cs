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
        public Character(string texture, float x, float y)
        {
            var sprite = this.AddComponent<Sprite>();
            sprite.TextureName = texture;
            sprite.Width = 32;
            sprite.Height = 48;

            var animation = this.AddComponent<SpriteAnimation>();
            animation.Interval = 200;
            animation.Add(MovingSprite.WalkDownAnim , 32, 48, new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0));
            animation.Add(MovingSprite.WalkLeftAnim, 32, 48, new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1));
            animation.Add(MovingSprite.WalkRightAnim, 32, 48, new Point(0, 2), new Point(1, 2), new Point(2, 2), new Point(3, 2));
            animation.Add(MovingSprite.WalkUpAnim, 32, 48, new Point(0, 3), new Point(1, 3), new Point(2, 3), new Point(3, 3));

            var body = this.AddComponent<Body>();
            body.Position = new Microsoft.Xna.Framework.Vector3(x, y, 0);
            body.Scale = new Vector3(2, 2, 1);

            this.AddComponent<MovingSprite>();

        }
    }
}
