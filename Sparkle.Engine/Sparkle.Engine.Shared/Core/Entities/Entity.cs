namespace Sparkle.Engine.Core.Entities
{
    using Sparkle.Engine.Core.Components;
    using System.Collections.Generic;
    using System.Linq;
    using Sparkle.Engine.Base;
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Main object in the game. A set of components can be attached to an instance to add behaviors.
    /// </summary>
    public class Entity
    {
        public Entity ()
        {
            this.Id = Identifier.Generate();
            this.components = new List<Component>();
        }

        /// <summary>
        /// Unique identifier of the entity in the game.
        /// </summary>
        public int Id { get; set; }

        #region Components

        private List<Component> components;

        public event EventHandler<ComponentEventArgs> ComponentAttached;

        public event EventHandler<ComponentEventArgs> ComponentDetached;

        /// <summary>
        /// All attached components.
        /// </summary>
        public IList<Component> Components
        {
            get { return new ReadOnlyCollection<Component>(components); }
        }
        
        /// <summary>
        /// Gets all components of a given type.
        /// </summary>
        /// <typeparam name="T">Type of the components</typeparam>
        /// <returns>A collection of components from the given type (never null).</returns>
        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return this.components.OfType<T>();
        }

        /// <summary>
        /// Gets the first component of a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first component of the given type, or null if no component from this type is attached.</returns>
        public T GetComponent<T>() where T : Component
        {
            return this.components.FirstOrDefault((c) => c is T) as T;
        }

        /// <summary>
        /// Attaches a component to the entity.
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            if(component.Owner != null)
            {
                component.Owner.RemoveComponent(component);
            }

            this.components.Add(component);
            component.Owner = this;
            component.Attached();

            if (ComponentAttached != null)
                this.ComponentAttached(this,new ComponentEventArgs() { Component = component });
        }

        /// <summary>
        /// Creates a component of the given type and attaches a component to the entity.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T AddComponent<T>() where T : Component
        {
            var component = (T)Activator.CreateInstance(typeof(T));
            this.AddComponent(component);
            return component;
        }

        /// <summary>
        /// Detaches a component from the entity.
        /// </summary>
        /// <param name="component"></param>
        public void RemoveComponent(Component component)
        {
            this.components.Remove(component);
            component.Detached();
            component.Owner = null;

            if (ComponentDetached != null)
                this.ComponentDetached(this, new ComponentEventArgs() { Component = component });
        }

        #endregion
    }
}
