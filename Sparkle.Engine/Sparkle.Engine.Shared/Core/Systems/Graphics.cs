namespace Sparkle.Engine.Core.Systems
{
    public class Graphics : System, Base.ILoadable, Base.IDrawable
    {
        public bool IsVisible { get { return this.IsEnabled; } }

        public int DrawOrder { get { return 0; } }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {

        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {

        }
        
        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb)
        {
            
        }
    }
}
