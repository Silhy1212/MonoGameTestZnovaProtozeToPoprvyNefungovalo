using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TestGame
{
    public static class LevelGenerator
    {
        public static void GenerateLevel(int[,] tileGrid, List<(Vector2 position, Rectangle sourceRect)> tiles)
        {
            int gridWidth = tileGrid.GetLength(1);
            int gridHeight = tileGrid.GetLength(0);
            int tileSize = SpriteSheet.SpriteSize * 4; // 8 * 4 = 32 pixelů

            for (int y = 0; y < gridHeight; y++)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    Vector2 position = new Vector2(x * tileSize, y * tileSize);
                    
                    switch ((TileType)tileGrid[y, x])
                    {
                        case TileType.PlatformTopLeft:
                            tiles.Add((position, SpriteSheet.PlatformTopLeft));
                            break;
                        case TileType.PlatformTopMid:
                            tiles.Add((position, SpriteSheet.PlatformTopMid));
                            break;
                        case TileType.PlatformTopRight:
                            tiles.Add((position, SpriteSheet.PlatformTopRight));
                            break;
                        case TileType.PlatformMidLeft:
                            tiles.Add((position, SpriteSheet.PlatformMidLeft));
                            break;
                        case TileType.PlatformMidMid:
                            tiles.Add((position, SpriteSheet.PlatformMidMid));
                            break;
                        case TileType.PlatformMidRight:
                            tiles.Add((position, SpriteSheet.PlatformMidRight));
                            break;
                        case TileType.PlatformBottomLeft:
                            tiles.Add((position, SpriteSheet.PlatformBottomLeft));
                            break;
                        case TileType.PlatformBottomMid:
                            tiles.Add((position, SpriteSheet.PlatformBottomMid));
                            break;
                        case TileType.PlatformBottomRight:
                            tiles.Add((position, SpriteSheet.PlatformBottomRight));
                            break;
                        
                        // Přidej další případy podle potřeby
                    }
                }
            }
        }
    }
}