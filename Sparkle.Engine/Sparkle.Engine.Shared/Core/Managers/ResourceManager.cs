namespace Sparkle.Engine.Core.Managers
{
    using Sparkle.Engine.Core.Resources;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class ResourceManager
    {
        public ResourceManager()
        {
            this.resources = new List<Resource>();
        }

        private List<Resource> resources;

        public IList<Resource> Entities
        {
            get { return new ReadOnlyCollection<Resource>(resources); }
        }

        public void AddResource(Resource resource)
        {
            this.resources.Add(resource);
        }

        public IEnumerable<T> GetResources<T>() where T : Resource
        {
            return this.resources.OfType<T>();
        }
    }
}
