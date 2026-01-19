using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Game_Kenuja.Models
{
    
        public class FastEnemy : Enemy
        {
            public float LeftBound, RightBound;
            private bool movingLeft = true;
            private float speed = 150f; // sneller dan normale patrol

            public FastEnemy(Texture2D texture, Vector2 startPos, float left, float right)
            {
                Texture = texture;
                Position = startPos;
                LeftBound = left;
                RightBound = right;
            }

            public override void Update(GameTime gameTime)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (movingLeft)
                    Position.X -= speed * dt;
                else
                    Position.X += speed * dt;

                if (Position.X <= LeftBound) movingLeft = false;
                if (Position.X >= RightBound) movingLeft = true;
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(Texture, Position, null, Color.Orange, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }
        }

    }

