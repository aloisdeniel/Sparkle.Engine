using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparkle.Engine.Core.Systems
{
    public class Behaviors : System, Base.IUpdateable
    {
        public Behaviors(SparkleGame game) : base(game)
        {

        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            var components = this.Game.Scene.GetComponents<Behavior>();
            
            foreach (var behavior in components)
            {
                behavior.Update(time);
            }
        }
  
    }
}
