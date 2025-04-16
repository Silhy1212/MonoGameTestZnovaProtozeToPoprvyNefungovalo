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

    private Matrix cameraTransform; // 🆕 Přidání kamery

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

        player = new Player(new Vector2(100, 0));

        int[,] levelGrid = new int[,]
        {
            {9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,1,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,1,2,3,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,1,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,1,2,2,2,2,2,2,3,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,9,1,0,0,0,0,0,0,0,0,0,0,0,0},
        };

        LevelGenerator.GenerateLevel(levelGrid, tiles);
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        int windowWidth = GraphicsDevice.Viewport.Width;
        int windowHeight = GraphicsDevice.Viewport.Height;

        player.Update(gameTime, windowWidth, windowHeight, tiles);

        // 🆕 Aktualizace kamery
        UpdateCamera();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // 🆕 Použití kamery při vykreslování
        _spriteBatch.Begin(transformMatrix: cameraTransform, samplerState: SamplerState.PointClamp);

        player.Draw(_spriteBatch, playerSheet);

        foreach (var tile in tiles)
        {
            _spriteBatch.Draw(playerSheet, tile.position, tile.sourceRect, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    // 🆕 Funkce na výpočet transformace kamery
    private void UpdateCamera()
    {
        // Předpoklad: velikost hráče je konstantní
        Vector2 playerCenter = player.Position + new Vector2(Player.Size / 2);
        Vector2 screenCenter = new Vector2(GraphicsDevice.Viewport.Width / 2f, GraphicsDevice.Viewport.Height / 2f);

        cameraTransform = Matrix.CreateTranslation(-playerCenter.X, -playerCenter.Y, 0f) *
                          Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0f);
    }
}
