using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Resources
{
    public abstract class Resource : Base.ILoadable
    {
        public abstract void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content);

        public virtual void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {

        }
    }
}
