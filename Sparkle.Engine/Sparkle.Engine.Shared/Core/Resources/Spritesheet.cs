using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Resources
{
    public class Spritesheet : Sprite
    {
        public Spritesheet(string texture) : base(texture)
        {
            this.Animations = new Dictionary<string, List<Rectangle>>();
        }

        /// <summary>
        /// All the animation steps.
        /// </summary>
        public Dictionary<string, List<Rectangle>> Animations { get; set; }

        public void Add(string name, int width, int height, params Point[] steps)
        {
            var stepRects = new List<Rectangle>();

            foreach (var point in steps)
            {
                stepRects.Add(new Rectangle(point.X * width, point.Y * height, width, height));
            }

            this.Animations[name] = stepRects;

        }
    }
}
