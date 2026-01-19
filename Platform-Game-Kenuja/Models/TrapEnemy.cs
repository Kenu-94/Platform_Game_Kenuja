using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Platform_Game_Kenuja.Models
{
    public class TrapEnemy : Enemy
    {
        public TrapEnemy(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            // blijft stil: dit is een valstrik !
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.Red,   // rood om gevaar aan te duiden
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                0f
            );
        }
    }

}
