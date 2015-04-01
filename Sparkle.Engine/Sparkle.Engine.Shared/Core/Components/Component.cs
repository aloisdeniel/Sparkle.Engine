using Sparkle.Engine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Components
{
    public abstract class Component
    {
        public Entity Entity { get; set; }
    }
}
