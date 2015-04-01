namespace Sparkle.Engine.Core.Systems
{
    /// <summary>
    /// The system that updates all sprites
    /// </summary>
    public class Graphics : System, Base.ILoadable, Base.IDrawable, Base.IUpdateable
    {
        public bool IsVisible { get { return this.IsEnabled; } }

        public int DrawOrder { get { return 0; } }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            // TODO : load textures of all sprites
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            // TODO : unload textures of all sprites
        }
        
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            // TODO : draw all sprites at their destination
        }

        public void Update(Microsoft.Xna.Framework.GameTime time)
        {
            // TODO : update sprite sources and destinations
        }
    }
}
