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
            this.renderers = new QuadTree<Renderer>(bounds);
            this.SamplerState = SamplerState.PointClamp;
        }

        public SamplerState SamplerState { get; set; }

        #region Updating sprite source, destination, color and angle

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            var components = this.Game.Scene.GetComponents<SpriteRenderer>();
            
            foreach (var sprite in components)
            {
                this.UpdateSprite(sprite, time);

                //Updates the quad tree after a potential bounds modification.
                //TODO : improve -> move only modified bounds with flag
                this.renderers.Move(sprite);
            }

            var emitters = this.Game.Scene.GetComponents<ParticleEmitter>();
            
            foreach (var emitter in emitters)
            {
                this.UpdateEmitter(emitter, time);

                //Updates the quad tree after a potential bounds modification.
                //TODO : improve -> move only modified bounds with flag
                this.renderers.Move(emitter);
            }
        }

        private void UpdateEmitter(ParticleEmitter emitter, Microsoft.Xna.Framework.GameTime time)
        {
            var dt = time.ElapsedGameTime.Milliseconds;

            for (int i = 0; i < emitter.Particles.Count;)
            {
                var particle = emitter.Particles[i];

                particle.Lifetime += time.ElapsedGameTime;

                if (particle.Lifetime >= particle.TotalLifetime)
                {
                    emitter.Particles.Remove(particle);
                }
                else
                {
                    particle.PositionVelocity += particle.PositionAcceleration * dt;
                    particle.Position += particle.PositionVelocity * dt;
                    particle.Rotation += particle.RotationVelocity * dt;
                    particle.Scale += particle.ScaleVelocity * dt;
                    particle.Color = particle.Color + particle.ColorVelocity  * dt;

                    var width = emitter.SourceArea.Width * particle.Scale;
                    var height = emitter.SourceArea.Width * particle.Scale;
                    particle.DestinationArea = new Rectangle(
                        (int)(particle.Position.X - width / 2), 
                        (int)(particle.Position.Y - height / 2), 
                        (int)(width), 
                        (int)(height));

                    i++;
                }
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

                sprite.Color = transform.Color;
                sprite.Angle = transform.Rotation;
                sprite.DestinationArea = new Frame(transform.Position.X, transform.Position.Y, width, height);
            }
        }

        #endregion

        #region Rendering

        private QuadTree<Renderer> renderers;

        public bool IsVisible { get { return this.IsEnabled; } }

        public int DrawOrder { get { return 0; } }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            var resources = this.Game.Scene.ResourceManager.GetResources<Sprite>();

            foreach (var sprite in resources)
            {
                sprite.LoadContent(content);
            }

            var components = this.Game.Scene.GetComponents<Renderer>();
            
            foreach (var sprite in components)
            {
                this.renderers.Add(sprite);
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

            var drawn = this.renderers.GetObjects(this.Game.Scene.Camera.Bounds);

            foreach (var renderer in drawn)
            {
                var behaviors = renderer.Owner.GetComponents<Behavior>();

                foreach (var behavior in behaviors)
                {
                    behavior.PreRender();
                }

                if (renderer is SpriteRenderer)
                {
                    var sprite = renderer as SpriteRenderer;

                    sb.Draw(sprite.Sprite.Texture,
                    sprite.DestinationArea.Rectangle,
                    sprite.SourceArea,
                    sprite.Color,
                    sprite.Angle,
                    sprite.Center,
                    SpriteEffects.None,
                    sprite.Order);
                }
                else if (renderer is ParticleEmitter)
                {
                    var particleEmitter = renderer as ParticleEmitter;
                    
                    foreach (var particle in particleEmitter.Particles)
                    {
                        sb.Draw(particleEmitter.Sprite.Texture,
                        particle.DestinationArea,
                        particleEmitter.SourceArea,
                        new Color(particle.Color),
                        particle.Rotation,
                        new Vector2(particleEmitter.SourceArea.Width,particleEmitter.SourceArea.Height),
                        SpriteEffects.None,
                        particleEmitter.Order);
                    }

                }
            }

            sb.End();
        }

        #endregion
        
        private void Scene_ComponentAttached(object sender, ComponentEventArgs e)
        {
            if (e.Component is Renderer)
            {
                this.renderers.Add(e.Component as SpriteRenderer);
            }
        }

        private void Scene_ComponentDetached(object sender, ComponentEventArgs e)
        {
            if(e.Component is Renderer)
            {
                this.renderers.Remove(e.Component as SpriteRenderer);
            }
        }
    }
}
