using System;
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

    private Vector2 playerPosition = new Vector2(0, 0);
    private Vector2 velocity = Vector2.Zero;

    private static int playerScale = 4;
    private static int spriteSize = 8;
    private static int playerSize = spriteSize * playerScale;

    private float moveSpeed = 200f;      
    private float gravity = 2000f;      
    private float jumpStrength = 700f;   
    private bool isOnGround = false;
    
    List<Platform> platforms = new List<Platform>();


    private Rectangle sourceRect = new Rectangle(0, 0, spriteSize, spriteSize);

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
        platforms.Add(new Platform(new Vector2(100, 300)));
        platforms.Add(new Platform(new Vector2(124, 300)));
        platforms.Add(new Platform(new Vector2(148, 300)));

    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState keyState = Keyboard.GetState();

        velocity.Y += gravity * elapsed; 

        if (keyState.IsKeyDown(Keys.A))
            velocity.X = -moveSpeed;
        else if (keyState.IsKeyDown(Keys.D))
            velocity.X = moveSpeed;
        else
            velocity.X = 0;

        if ((keyState.IsKeyDown(Keys.W) || keyState.IsKeyDown(Keys.Space)) && isOnGround)
        {
            velocity.Y = -jumpStrength;
            isOnGround = false;
        }

        playerPosition += velocity * elapsed;

        int windowWidth = GraphicsDevice.Viewport.Width;
        int windowHeight = GraphicsDevice.Viewport.Height;

        if (playerPosition.X < 0)
            playerPosition.X = 0;
        if (playerPosition.X > windowWidth - playerSize)
            playerPosition.X = windowWidth - playerSize;

        if (playerPosition.Y >= windowHeight - playerSize)
        {
            playerPosition.Y = windowHeight - playerSize;
            velocity.Y = 0; 
            isOnGround = true;
        }

      
        Console.WriteLine("Pádová rychlost (velocity.Y): " + velocity.Y);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        _spriteBatch.Draw(playerSheet, playerPosition, sourceRect, Color.White, 0f, Vector2.Zero, playerScale, SpriteEffects.None, 0f);
        foreach (var platform in platforms)
        {
            platform.Draw(_spriteBatch, playerSheet);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
