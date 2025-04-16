using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TestGame;

public class Player
{
    public Vector2 Position;
    private Vector2 velocity;

    private readonly int scale = 4;
    private readonly int spriteSize = 8;
    public int Size => spriteSize * scale;

    private readonly float moveSpeed = 200f;
    private readonly float gravity = 2000f;
    private readonly float jumpStrength = 700f;

    private bool isOnGround = false;

    private Rectangle sourceRect = SpriteSheet.Player;

    public Player(Vector2 startPos)
    {
        Position = startPos;
        velocity = Vector2.Zero;
    }

    public void Update(GameTime gameTime, int windowWidth, int windowHeight)
    {
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState keyState = Keyboard.GetState();

        // Gravitace
        velocity.Y += gravity * elapsed;

        // Vodorovný pohyb
        if (keyState.IsKeyDown(Keys.A))
            velocity.X = -moveSpeed;
        else if (keyState.IsKeyDown(Keys.D))
            velocity.X = moveSpeed;
        else
            velocity.X = 0;

        // Skok
        if ((keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Space)) && isOnGround)
        {
            velocity.Y = -jumpStrength;
            isOnGround = false;
        }

        Position += velocity * elapsed;

        // Kolize s hranami okna
        if (Position.X < 0)
            Position.X = 0;
        if (Position.X > windowWidth - Size)
            Position.X = windowWidth - Size;

        if (Position.Y >= windowHeight - Size)
        {
            Position.Y = windowHeight - Size;
            velocity.Y = 0;
            isOnGround = true;
        }

        Console.WriteLine("Pádová rychlost (velocity.Y): " + velocity.Y);
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, Position, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }
}
