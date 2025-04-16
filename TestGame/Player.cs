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

    // Pohyb (horizontální)
    var keyState = Keyboard.GetState();
    if (keyState.IsKeyDown(Keys.A))
        Velocity.X = -MoveSpeed;
    else if (keyState.IsKeyDown(Keys.D))
        Velocity.X = MoveSpeed;
    else
        Velocity.X = 0;

    // Skákání
    if ((keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Space)) && isOnGround)
    {
        Velocity.Y = -JumpStrength;
        isOnGround = false;
    }

    // Gravitace
    if (!isOnGround) // Pouze pokud nejsme na zemi!
    {
        Velocity.Y += Gravity * elapsed;
    }
    else
    {
        Velocity.Y = 0; // Zajistíme, že na zemi nebude žádná vertikální rychlost
    }

    // Navrhovaná nová pozice
    Vector2 newPosition = Position;
    newPosition.X += Velocity.X * elapsed;
    newPosition.Y += Velocity.Y * elapsed;

    Rectangle newBounds = new Rectangle((int)newPosition.X, (int)newPosition.Y, Size, Size);
    bool wasOnGround = isOnGround;
    isOnGround = false;

    // Kolize s platformami
    foreach (var tile in tiles)
    {
        Rectangle tileRect = new Rectangle((int)tile.position.X, (int)tile.position.Y, SpriteSize * Scale, SpriteSize * Scale);

        if (newBounds.Intersects(tileRect))
        {
            // Vypočítáme průniky
            float overlapLeft = newBounds.Right - tileRect.Left;
            float overlapRight = tileRect.Right - newBounds.Left;
            float overlapTop = newBounds.Bottom - tileRect.Top;
            float overlapBottom = tileRect.Bottom - newBounds.Top;

            // Najdeme nejmenší průnik
            float minOverlap = Math.Min(Math.Min(overlapLeft, overlapRight), Math.Min(overlapTop, overlapBottom));

            // Kolize shora (stojíme na platformě)
            if (minOverlap == overlapTop && Velocity.Y >= 0)
            {
                newPosition.Y = tileRect.Top - Size;
                isOnGround = true;
                Velocity.Y = 0;
                
                // Malý posun dolů, aby se lépe "chytil" k platformě
                if (wasOnGround && Math.Abs(Velocity.Y) < 100)
                {
                    newPosition.Y = tileRect.Top - Size + 1; // +1 pomáhá proti "houpání"
                }
            }
            else if (minOverlap == overlapBottom && Velocity.Y <= 0)
            {
                // Kolize zdola (narážíme do stropu)
                newPosition.Y = tileRect.Bottom;
                Velocity.Y = 0;
            }
            else if (minOverlap == overlapLeft)
            {
                // Kolize zprava
                newPosition.X = tileRect.Left - Size;
                Velocity.X = 0;
            }
            else if (minOverlap == overlapRight)
            {
                // Kolize zleva
                newPosition.X = tileRect.Right;
                Velocity.X = 0;
            }
        }
    }

    // Aplikujeme novou pozici
    Position = newPosition;

    // Hranice okna
    Position.X = MathHelper.Clamp(Position.X, 0, windowWidth - Size);
    Position.Y = MathHelper.Clamp(Position.Y, -100, windowHeight - Size); // -100 aby mohl vypadnout nahoru

    // Pokud jsme pod spodní hranicí, jsme na zemi
    if (Position.Y >= windowHeight - Size)
    {
        isOnGround = true;
        Velocity.Y = 0;
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
