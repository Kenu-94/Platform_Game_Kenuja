using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platform_Game_Kenuja.Models
{
    public abstract class Enemy
    {
        public Texture2D Texture;
        public Vector2 Position;
        public float Scale = 0.2f; //Grootte van de sprite op het scherm -> 20% kleiner
        public float Speed;


        //Hoe ver een vijand kan bewegen!
        public float LeftBound;
        public float RightBound;

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle BoundingBox =>
            new Rectangle((int)Position.X, (int)Position.Y,
            (int)(Texture.Width * Scale),
            (int)(Texture.Height * Scale));
    };

        
    }

