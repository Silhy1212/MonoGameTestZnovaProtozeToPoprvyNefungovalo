using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TestGame
{
    public class Player
    {
        public Vector2 Position;
        private Vector2 Velocity;

        private const int SpriteSize = 8;
        private const int Scale = 4;
        public const int Size = SpriteSize * Scale;

        private const float Gravity = 2000f;
        private const float JumpStrength = 700f;
        private const float MoveSpeed = 200f;

        private bool isOnGround = false;

        public Player(Vector2 startPosition)
        {
            Position = startPosition;
        }

        public void Update(GameTime gameTime, int windowWidth, int windowHeight, List<(Vector2 position, Rectangle sourceRect)> tiles)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Movement (horizontal)
            var keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.A))
                Velocity.X = -MoveSpeed;
            else if (keyState.IsKeyDown(Keys.D))
                Velocity.X = MoveSpeed;
            else
                Velocity.X = 0;

            if ((keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Space)) && isOnGround)
            {
                Velocity.Y = -JumpStrength;
                isOnGround = false;
            }

            // Gravity (falling)
            Velocity.Y += Gravity * elapsed;

            // Apply vertical movement first (so it takes priority for collisions)
            Position.Y += Velocity.Y * elapsed;

            // Collision detection (only vertical movement here)
            Rectangle playerBounds = GetBounds();
            isOnGround = false;

            // Handle vertical collisions first
            foreach (var tile in tiles)
            {
                Rectangle tileRect = new Rectangle((int)tile.position.X, (int)tile.position.Y, SpriteSize * Scale, SpriteSize * Scale);

                // Bottom collision (Player falling)
                if (playerBounds.Bottom >= tileRect.Top && playerBounds.Bottom <= tileRect.Top + 10 &&
                    playerBounds.Right > tileRect.Left && playerBounds.Left < tileRect.Right && Velocity.Y >= 0)
                {
                    Position.Y = tileRect.Top - Size; // Stop player on platform
                    Velocity.Y = 0; // Stop downward velocity
                    isOnGround = true;
                }

                // Top collision (Player jumping up)
                if (playerBounds.Top <= tileRect.Bottom && playerBounds.Top >= tileRect.Bottom - 10 &&
                    playerBounds.Right > tileRect.Left && playerBounds.Left < tileRect.Right && Velocity.Y <= 0)
                {
                    Position.Y = tileRect.Bottom; // Stop player on platform top
                    Velocity.Y = 0;
                }
            }

            // Apply horizontal movement after vertical movement to avoid problems
            Position.X += Velocity.X * elapsed;

            // Horizontal collision detection (correct stopping logic)
            foreach (var tile in tiles)
            {
                Rectangle tileRect = new Rectangle((int)tile.position.X, (int)tile.position.Y, SpriteSize * Scale, SpriteSize * Scale);

                // Left collision (Player hitting from left)
                if (playerBounds.Right > tileRect.Left && playerBounds.Left < tileRect.Left &&
                    playerBounds.Bottom > tileRect.Top && playerBounds.Top < tileRect.Bottom)
                {
                    Position.X = tileRect.Left - Size; // Stop player on the left side of the platform
                    Velocity.X = 0;
                }

                // Right collision (Player hitting from right)
                if (playerBounds.Left < tileRect.Right && playerBounds.Right > tileRect.Right &&
                    playerBounds.Bottom > tileRect.Top && playerBounds.Top < tileRect.Bottom)
                {
                    Position.X = tileRect.Right; // Stop player on the right side of the platform
                    Velocity.X = 0;
                }
            }

            // Collision with window boundaries (not to go out of bounds)
            if (Position.X < 0) Position.X = 0;
            if (Position.X > windowWidth - Size) Position.X = windowWidth - Size;

            if (Position.Y > windowHeight - Size)
            {
                Position.Y = windowHeight - Size;
                Velocity.Y = 0;
                isOnGround = true; // When player is on the ground, they should stop falling
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, Position, SpriteSheet.Player, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }

        private Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Size, Size);
        }
    }
}
