using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sparkle.Engine.Core.Components;
using Sparkle.Engine.Core.Entities;
using Sparkle.Engine.Core.Resources;
using Sparkle.Engine.Samples.Shared.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Samples.Shared.Entities
{
    public class Character : Entity
    {
        public Character(Spritesheet sprite, float x, float y)
        {

            var renderer = this.AddComponent<SpriteRenderer>();
            renderer.Sprite = sprite;
            renderer.Width = 32;
            renderer.Height = 48;

            var animation = this.AddComponent<SpriteAnimation>();
            animation.Sheet = sprite;
            animation.Interval = 200;

            var body = this.AddComponent<Body>();
            body.Position = new Microsoft.Xna.Framework.Vector3(x, y, 0);
            body.Scale = new Vector3(2, 2, 1);

            this.AddComponent<MovingSprite>();

        }
    }
}
