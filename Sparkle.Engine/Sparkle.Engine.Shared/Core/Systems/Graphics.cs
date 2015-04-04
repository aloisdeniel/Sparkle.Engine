using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sparkle.Engine.Base;
using Sparkle.Engine.Base.Geometry;
using Sparkle.Engine.Core.Components;
using Sparkle.Engine.Core.Resources;
namespace Sparkle.Engine.Core.Systems
{
    /// <summary>
    /// The system that updates all sprites
    /// </summary>
    public class Graphics : System, Base.ILoadable, Base.IDrawable, Base.IUpdateable
    {
        public Graphics(SparkleGame game, Frame bounds) : base(game)
        {
            this.sprites = new QuadTree<SpriteRenderer>(bounds);
            this.SamplerState = SamplerState.PointClamp;
        }

        public SamplerState SamplerState { get; set; }

        #region Updating sprite source, destination, color and angle

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            var components = this.Game.Scene.GetComponents<SpriteRenderer>();

            var dt = time.ElapsedGameTime.Milliseconds;

            foreach (var sprite in components)
            {
                this.UpdateSprite(sprite, time);
            }
        }

        private void UpdateSprite(SpriteRenderer sprite, Microsoft.Xna.Framework.GameTime time)
        {
            var animation = sprite.Owner.GetComponent<SpriteAnimation>();

            if (animation != null)
            {
                animation.CurrentTime += time.ElapsedGameTime.Milliseconds;

                sprite.SourceArea = animation.GetSource();
            }

            var transform = sprite.Owner.GetComponent<Transform>();

            if (transform != null)
            {
                var center = sprite.Center;
                var width = sprite.Width * transform.Scale.X;
                var height = sprite.Height * transform.Scale.Y;

                sprite.Order = -transform.Position.Y;
                sprite.Color = transform.Color;
                sprite.Angle = transform.Rotation;
                sprite.DestinationArea = new Frame(transform.Position.X, transform.Position.Y, width, height);
            }
        }

        #endregion

        #region Rendering

        private QuadTree<SpriteRenderer> sprites;

        public bool IsVisible { get { return this.IsEnabled; } }

        public int DrawOrder { get { return 0; } }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            var resources = this.Game.Scene.ResourceManager.GetResources<Sprite>();

            foreach (var sprite in resources)
            {
                sprite.LoadContent(content);
            }

            var components = this.Game.Scene.GetComponents<SpriteRenderer>();
            
            foreach (var sprite in components)
            {
                this.sprites.Add(sprite);
            }

            this.Game.Scene.EntityManager.ComponentAttached += Scene_ComponentAttached;
            this.Game.Scene.EntityManager.ComponentDetached += Scene_ComponentDetached;
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            content.Unload();

            this.Game.Scene.EntityManager.ComponentAttached -= Scene_ComponentAttached;
            this.Game.Scene.EntityManager.ComponentDetached -= Scene_ComponentDetached;
        }
        
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            this.Game.GraphicsDevice.Clear(this.Game.Scene.BackgroundColor);

            sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, this.SamplerState, null, null, null, this.Game.Scene.Camera.CreateTransform());

            var drawn = this.sprites.GetObjects(this.Game.Scene.Camera.Bounds);

            foreach (var sprite in drawn)
            {
                sb.Draw(sprite.Sprite.Texture,
                sprite.DestinationArea.Rectangle,
                sprite.SourceArea,
                sprite.Color,
                sprite.Angle,
                sprite.Center,
                SpriteEffects.None,
                sprite.Order);
            }

            sb.End();
        }

        #endregion
        
        private void Scene_ComponentAttached(object sender, ComponentEventArgs e)
        {
            if (e.Component is SpriteRenderer)
            {
                this.sprites.Add(e.Component as SpriteRenderer);
            }
        }

        private void Scene_ComponentDetached(object sender, ComponentEventArgs e)
        {
            if(e.Component is SpriteRenderer)
            {
                this.sprites.Remove(e.Component as SpriteRenderer);
            }
        }
    }
}
