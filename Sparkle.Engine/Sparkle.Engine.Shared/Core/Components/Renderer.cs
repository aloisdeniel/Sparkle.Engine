using Sparkle.Engine.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public abstract class Renderer : Component, IQuadStorable
    {
        public abstract Base.Geometry.Frame Bounds { get; }

        /// <summary>
        /// The drawing order of the sprite.
        /// </summary>
        public virtual float Order { get; set; }
    }
}
