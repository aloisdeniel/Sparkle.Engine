using Sparkle.Engine.Core.Components;
using Sparkle.Engine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Sparkle.Engine.Core.Managers
{
    public class EntityManager
    {
        public EntityManager()
        {
            this.entities = new List<Entity>();
        }

        private List<Entity> entities;

        public IList<Entity> Entities
        {
            get { return new ReadOnlyCollection<Entity>(entities); }
        }

        public event EventHandler<ComponentEventArgs> ComponentAttached;

        public event EventHandler<ComponentEventArgs> ComponentDetached;

        private void RaiseComponentAttached(Component component)
        {
            if (ComponentAttached != null)
                this.ComponentAttached(this, new ComponentEventArgs() { Component = component });
        }

        private void RaiseComponentDetached(Component component)
        {
            if (ComponentDetached != null)
                this.ComponentDetached(this, new ComponentEventArgs() { Component = component });
        }

        public void AddEntity(Entity entity)
        {
            this.entities.Add(entity);

            foreach (var component in entity.Components)
            {
                this.RaiseComponentAttached(component);
            }

            entity.ComponentAttached += entity_ComponentAttached;
            entity.ComponentDetached += entity_ComponentDetached;
        }

        public void RemoveEntity(Entity entity)
        {
            this.entities.Remove(entity);

            entity.ComponentAttached -= entity_ComponentAttached;
            entity.ComponentDetached -= entity_ComponentDetached;

            foreach (var component in entity.Components)
            {
                this.RaiseComponentDetached(component);
            }
        }

        private void entity_ComponentAttached(object sender, ComponentEventArgs e)
        {
            this.RaiseComponentAttached(e.Component);
        }

        private void entity_ComponentDetached(object sender, ComponentEventArgs e)
        {
            this.RaiseComponentDetached(e.Component);
        }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            List<T> result = new List<T>();

            foreach (var item in this.entities)
            {
                result.AddRange(item.GetComponents<T>());
            }

            return result;
        }
    }
}
