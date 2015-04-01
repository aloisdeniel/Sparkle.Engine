using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sparkle.Engine.Core.Entities
{
    public class Entity
    {
        public Entity ()
        {
            this.Components = new List<Component>();
            this.Children = new List<Entity>();
        }

        public int Id { get; set; }

        #region Entities

        public Entity Parent { get; set; }

        public List<Entity> Children { get; set; }

        public void AddChild(Entity entity)
        {
            this.Children.Add(entity);
        }

        public void RemoveChild(Entity entity)
        {
            this.Children.Remove(entity);
        }

        #endregion

        #region Components

        public List<Component> Components { get; private set; }

        public IEnumerable<Component> GetComponents<T>() where T : Component
        {
            return this.Components.OfType<T>();
        }

        public T GetComponent<T>() where T : Component
        {
            return this.Components.FirstOrDefault((c) => c is T) as T;
        }

        public void AddComponent(Component component)
        {
            this.Components.Add(component);
        }

        public void RemoveComponent(Component component)
        {
            this.Components.Remove(component);
        }

        #endregion
    }
}
