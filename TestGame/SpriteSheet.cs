using Microsoft.Xna.Framework;

namespace TestGame;

public static class SpriteSheet
{
    public static readonly int SpriteSize = 8;

    // Hráč – první sprite (0,0)
    public static readonly Rectangle Player = new Rectangle(0, 0, SpriteSize, SpriteSize);

    // Platformy – 2. řádek (y = 8)
    public static readonly Rectangle PlatformTopLeft = new Rectangle(0, 8, SpriteSize, SpriteSize);
    public static readonly Rectangle PlatformTopMid = new Rectangle(8, 8, SpriteSize, SpriteSize);
    public static readonly Rectangle PlatformTopRight = new Rectangle(16, 8, SpriteSize, SpriteSize);

    public static readonly Rectangle PlatformMidLeft = new Rectangle(0, 16, SpriteSize, SpriteSize);
    public static readonly Rectangle PlatformMidMid = new Rectangle(8, 16, SpriteSize, SpriteSize);
    public static readonly Rectangle PlatformMidRight = new Rectangle(16, 16, SpriteSize, SpriteSize);

    public static readonly Rectangle PlatformBottomLeft = new Rectangle(0, 24, SpriteSize, SpriteSize);
    public static readonly Rectangle PlatformBottomMid = new Rectangle(8, 24, SpriteSize, SpriteSize);
    public static readonly Rectangle PlatformBottomRight = new Rectangle(16, 24, SpriteSize, SpriteSize);

    // Můžeš přidat další podle potřeby – např. pozadí, dekorace atd.
}
public enum TileType
{
    Empty = 0,
    PlatformTopLeft = 1,
    PlatformTopMid = 2,
    PlatformTopRight = 3,
    PlatformMidLeft = 4,
    PlatformMidMid = 5,
    PlatformMidRight = 6,
    PlatformBottomLeft = 7,
    PlatformBottomMid = 8,
    PlatformBottomRight = 9,
    // Můžeš přidat další typy...
}