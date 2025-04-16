using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame;

public class Platform
{
    public Vector2 Position;
    public static int Size = 32; // base ze v px
    public Rectangle SourceRect;
    public static int Scale = 1;

    public Platform(Vector2 position)
    {
        Position = position;
        SourceRect = new Rectangle(0, 8, Size, Size); 
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture)
    {
        spriteBatch.Draw(texture, Position, SourceRect, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
    }

    public Rectangle GetBounds()
    {
        return new Rectangle(
            (int)Position.X,
            (int)Position.Y,
            Size * Scale,
            Size * Scale
        );
    }
}