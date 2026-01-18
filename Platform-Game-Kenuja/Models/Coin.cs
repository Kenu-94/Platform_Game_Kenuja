using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platform_Game_Kenuja.Models
{
    public class Coin
    {
        public Texture2D Texture;
        public Vector2 Position;

        private float baseScale = 0.06f;      // klein en mooi voor 500x500 coin
        private float pulseStrength = 0.01f;  // zeer kleine animatie
        private float animationTimer = 0f;

        public Rectangle BoundingBox =>
            new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)(Texture.Width * baseScale),
                (int)(Texture.Height * baseScale)
            );

        public void Update(GameTime gameTime)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 3f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // kleine zachte pulse, NIET te groot maken
            float scale = baseScale + (float)Math.Sin(animationTimer) * pulseStrength;

            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
