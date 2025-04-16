using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D playerSheet;

    private Player player;
   
    private List<(Vector2 position, Rectangle sourceRect)> tiles = new();


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        playerSheet = Content.Load<Texture2D>("spritesheet");

        player = new Player(new Vector2(0, 0));
        int tilePositionX = 100;
        tiles.Add((new Vector2(tilePositionX, 300), SpriteSheet.PlatformTopLeft));
        tilePositionX += 8 * 4;
        tiles.Add((new Vector2(tilePositionX, 300), SpriteSheet.PlatformTopMid));
        tilePositionX += 8 * 4;

        tiles.Add((new Vector2(tilePositionX, 300), SpriteSheet.PlatformTopRight));


        // Platformy
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        int windowWidth = GraphicsDevice.Viewport.Width;
        int windowHeight = GraphicsDevice.Viewport.Height;

        player.Update(gameTime, windowWidth, windowHeight);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        player.Draw(_spriteBatch, playerSheet);

        foreach (var tile in tiles)
        {
            _spriteBatch.Draw(playerSheet, tile.position, tile.sourceRect, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
        }


        _spriteBatch.End();

        base.Draw(gameTime);
    }
}