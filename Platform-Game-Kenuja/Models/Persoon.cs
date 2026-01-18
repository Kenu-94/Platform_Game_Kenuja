using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Platform_Game_Kenuja.Models
{
    public class Persoon
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public bool IsJumping;

        public int Width;
        public int Height;

        private int currentFrame = 0;
        private int totalFrames = 5;  // aantal frames in je spritesheet

        private float animationTimer = 0f;
        private float animationSpeed = 0.03f;

        private SpriteEffects flip = SpriteEffects.None;

        public bool IsAttacking = false;
        private float attackTimer = 0f;
        private float attackDuration = 0.2f; // 0.2 seconden


        // heel belangrijk: zelfde scale gebruiken in draw én collision
        private float scale = 0.5f;
        

        private KeyboardState previousKeyboard;

        public Rectangle BoundingBox => new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)(Width*scale),
                (int)(Height * scale)
);

        public Rectangle AttackBox
        {
            get
            {
                int width = 50;  // bereik van mes
                int height = (int)(Height * scale);

                if (flip == SpriteEffects.None)
                {
                    // mes rechts
                    return new Rectangle(
                        (int)(Position.X + Width * scale),
                        (int)Position.Y,
                        width,
                        height
                    );
                }
                else
                {
                    // mes links
                    return new Rectangle(
                        (int)(Position.X - width),
                        (int)Position.Y,
                        width,
                        height
                    );
                }
            }
        }




        public void Update(GameTime gameTime, Platform[] platforms)
        {
            var keyboard = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            bool isMoving = false;

            // Horizontale beweging
            if (keyboard.IsKeyDown(Keys.Left))
            {
                Position.X -= 150 * dt;
                flip = SpriteEffects.FlipHorizontally;
                isMoving = true;
            }

            if (keyboard.IsKeyDown(Keys.Right))
            {
                Position.X += 150 * dt;
                flip = SpriteEffects.None;
                isMoving = true;
            }

            // springen
            bool jumpNow = keyboard.IsKeyDown(Keys.Up);
            bool jumpBefore = previousKeyboard.IsKeyDown(Keys.Up);

            if (jumpNow && !jumpBefore && !IsJumping)
            {
                Velocity.Y = -450f;
                IsJumping = true;
            }

            // mes attack
            if (keyboard.IsKeyDown(Keys.A) && !IsAttacking)
            {
                IsAttacking = true;
                attackTimer = attackDuration;
            }

            // In Persoon.Update():
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Position.Y += 1; // Crouch effect

            // zwaartekracht
            Velocity.Y += 900f * dt;

            // Save old Y before moving
            float oldY = Position.Y;

            // Apply vertical movement
            Position.Y += Velocity.Y * dt;

            // platform collision
            foreach (var p in platforms)
            {
                Rectangle pb = p.BoundingBox;
                Rectangle bb = this.BoundingBox;

                bool horizontalOverlap =
                    bb.Right > pb.Left &&
                    bb.Left < pb.Right;

                // Onderste rand vorige frame
                float oldBottom = oldY + (Height * scale);

                // Onderste rand nieuwe frame
                float newBottom = Position.Y + (Height * scale);

                bool isFalling = Velocity.Y > 0;

                if (horizontalOverlap &&
                    isFalling &&
                    oldBottom <= pb.Top &&
                    newBottom >= pb.Top)
                {
                    Position.Y = pb.Top - (Height * scale);
                    Velocity.Y = 0;
                    IsJumping = false;
                }
            }

            // Animatie
            if (IsJumping)
            {
                currentFrame = 2;
            }
            else if (isMoving)
            {
                animationTimer += dt;

                if (animationTimer > animationSpeed)
                {
                    currentFrame++;
                    if (currentFrame >= totalFrames)
                        currentFrame = 1;

                    animationTimer = 0f;
                }
            }
            else
            {
                currentFrame = 0;
            }

            // Attack timer
            if (IsAttacking)
            {
                attackTimer -= dt;
                if (attackTimer <= 0)
                    IsAttacking = false;
            }

            previousKeyboard = keyboard;
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(
                currentFrame * Width,
                0,
                Width,
                Height
            );

            spriteBatch.Draw(
                Texture,
                Position,
                sourceRect,
                Color.White,
                0f,
                Vector2.Zero,
                scale,
                flip,
                0f
            );
        }

        public void DrawKnife(SpriteBatch spriteBatch, Texture2D knifeTexture)
        {
            if (!IsAttacking)
                return; // mes alleen tonen wanneer we aanvallen

            float scaleKnife = 0.02f;

            Vector2 knifePos;

            if (flip == SpriteEffects.None)
            {
                // naar rechts
                knifePos = new Vector2(
                    Position.X + (Width * scale),
                    Position.Y + (Height * scale / 3)
                );
            }
            else
            {
                // naar links
                knifePos = new Vector2(
                    Position.X - (knifeTexture.Width * scaleKnife),
                    Position.Y + (Height * scale / 3)
                );
            }

            spriteBatch.Draw(
                knifeTexture,
                knifePos,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                scaleKnife,
                flip,
                0f
            );
        }




    }
}
