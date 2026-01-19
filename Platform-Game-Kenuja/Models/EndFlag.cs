using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platform_Game_Kenuja.Models
{
    public class EndFlag
    {
        public Texture2D Texture;
        public Vector2 Position;
        public float Scale = 0.10f;
        public bool Activated = false;

        public Rectangle BoundingBox =>
            new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)(Texture.Width * Scale),
                (int)(Texture.Height * Scale)
            );

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture == null) return;

            // Groen als hij geactiveerd is
            Color tint = Activated ? Color.LimeGreen : Color.White;

            spriteBatch.Draw(
                Texture,
                Position,
                null,
                tint,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
