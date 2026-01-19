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

        public Rectangle BoundingBox =>   //Collisions!
            new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)(Texture.Width * baseScale),
                (int)(Texture.Height * baseScale)
            );

        public void Update(GameTime gameTime)
        {
            animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 3f;
            //Deze methode verhoogt de animatietimer op basis van de verstreken tijd, zodat de animatie even snel blijft lopen ongeacht de FPS. (Frames Per Second)
            //ElapsedGametime: Time sinds het laatste frame!
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            float scale = baseScale + (float)Math.Sin(animationTimer) * pulseStrength;
            //Deze lijn zorgt ervoor dat de coin rustig groter en kleiner wordt, alsof hij ademt.

            spriteBatch.Draw(
                Texture, //Plaatje je tekent
                Position, //Positie op het scherm
                null, //Hele afbeelding
                Color.White, //Normale kleur
                0f, //Rotatie: Niet draaien
                Vector2.Zero, //Draaipunt Links boven
                scale, 
                SpriteEffects.None,//Gn spiegeling
                0f //Wat voor/achter ligt
            );
        }
    }
}
