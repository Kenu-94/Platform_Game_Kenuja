using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Platform_Game_Kenuja.Models
{
    public abstract class Enemy
    {
        public Texture2D Texture;
        public Vector2 Position;
        public float Scale = 0.2f;


        // public float Scale = 0.20f; // kleiner enemy formaat
        public float Speed;

        public float LeftBound;
        public float RightBound;

        //public bool MovingLeft = true;

        // HIT EFFECT
        //public bool IsHit = false;
        //public float hitTimer = 0f;

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public Rectangle BoundingBox =>
            new Rectangle((int)Position.X, (int)Position.Y,
            (int)(Texture.Width * Scale),
            (int)(Texture.Height * Scale));
    };

        //public void Update(GameTime gameTime)
        //{
        //    float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    // -----------------------------------
        //    // BEWEGEN
        //    // -----------------------------------

        //    if (MovingLeft)
        //        Position.X -= Speed * dt;
        //    else
        //        Position.X += Speed * dt;

        //    // Omdraaien bij grenzen
        //    if (Position.X <= LeftBound)
        //        MovingLeft = false;

        //    if (Position.X >= RightBound)
        //        MovingLeft = true;

        //    // -----------------------------------
        //    // HIT TIMER (knipper effect)
        //    // -----------------------------------
        //    if (IsHit)
        //    {
        //        hitTimer -= dt;
        //        if (hitTimer <= 0)
        //            IsHit = false;
        //    }
        //}

        //public void Draw(SpriteBatch spriteBatch)
        //{
        //    Color tint = Color.White;

        //    if (IsHit)
        //        tint = Color.Red; // rood knipper

        //    spriteBatch.Draw(
        //        Texture,
        //        Position,
        //        null,
        //        tint,
        //        0f,
        //        Vector2.Zero,
        //        Scale,
        //        SpriteEffects.None,
        //        0f
        //    );
        //}
    }

