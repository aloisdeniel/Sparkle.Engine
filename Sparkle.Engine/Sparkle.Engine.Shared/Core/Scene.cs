using Sparkle.Engine.Core.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using Sparkle.Engine.Core.Entities;
using Microsoft.Xna.Framework;
using Sparkle.Engine.Core.Managers;

namespace Sparkle.Engine.Core
{
    public class Scene
    {
        public Scene(SparkleGame game)
        {
            this.Game = game;
            this.EntityManager = new EntityManager();
            this.ResourceManager = new ResourceManager();
            this.Camera = new Camera();
            this.EntityManager.AddEntity(this.Camera);
            this.BackgroundColor = Color.CornflowerBlue;
        }

        public Color BackgroundColor { get; set; }

        public SparkleGame Game { get; set; }

        public Camera Camera { get; set; }

        public EntityManager EntityManager { get; set; }

        public ResourceManager ResourceManager { get; set; }

        public IEnumerable<T> GetComponents<T>() where T : Component
        {
            return this.EntityManager.GetComponents<T>();
        }

    }
}
