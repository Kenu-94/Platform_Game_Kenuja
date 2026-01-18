using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Game_Kenuja.Models
{
    public class Platform
    {
        public Texture2D Texture;
        public Vector2 Position;
        public int Width, Height;

        public Rectangle BoundingBox =>
            new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                new Rectangle((int)Position.X, (int)Position.Y, Width, Height),
                Color.White
            );
        }
    }
}

