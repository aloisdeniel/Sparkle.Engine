using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sparkle.Engine.Core
{
    public class Scene
    {
        public Scene()
        {
            this.Entities = new List<Entity>();
        }

        public List<Entity> Entities { get; set; }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            List<T> result = new List<T>();

            foreach (var item in this.Entities)
            {
                result.AddRange(item.GetComponents<T>());
            }

            return result;
        }
    }
}
