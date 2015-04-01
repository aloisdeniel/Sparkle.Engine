namespace Sparkle.Engine.Core.Entities
{
    using Sparkle.Engine.Core.Components;
    using System.Collections.Generic;
    using System.Linq;
    using Sparkle.Engine.Base;

    /// <summary>
    /// Main object in the game. A set of components can be attached to an instance to add behaviors.
    /// </summary>
    public class Entity
    {
        public Entity ()
        {
            this.Id = Identifier.Generate();
            this.Components = new List<Component>();
        }

        /// <summary>
        /// Unique identifier of the entity in the game.
        /// </summary>
        public int Id { get; set; }
        
        #region Components

        /// <summary>
        /// All attached components.
        /// </summary>
        private List<Component> Components { get; private set; }

        /// <summary>
        /// Gets all components of a given type.
        /// </summary>
        /// <typeparam name="T">Type of the components</typeparam>
        /// <returns>A collection of components from the given type (never null).</returns>
        public IEnumerable<Component> GetComponents<T>() where T : Component
        {
            return this.Components.OfType<T>();
        }

        /// <summary>
        /// Gets the first component of a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The first component of the given type, or null if no component from this type is attached.</returns>
        public T GetComponent<T>() where T : Component
        {
            return this.Components.FirstOrDefault((c) => c is T) as T;
        }

        /// <summary>
        /// Attaches a component to the entity.
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            this.Components.Add(component);
        }

        /// <summary>
        /// Detaches a component from the entity.
        /// </summary>
        /// <param name="component"></param>
        public void RemoveComponent(Component component)
        {
            this.Components.Remove(component);
        }

        #endregion
    }
}
