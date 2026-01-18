using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Platform_Game_Kenuja.Models
{
    public class ExplosionParticle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Lifetime;
        public float Scale;
        public Color Color;

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position += Velocity * dt;
            Lifetime -= dt;
            Scale *= 0.95f;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            float realScale = Scale * 0.15f;

            spriteBatch.Draw(
                texture,
                Position,
                null,
                Color * Lifetime,
                0f,
                new Vector2(texture.Width / 2, texture.Height / 2),
                realScale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
