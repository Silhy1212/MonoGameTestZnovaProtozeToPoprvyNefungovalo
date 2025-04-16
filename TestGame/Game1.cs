using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestGame;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    Texture2D playerSheet;
    Vector2 playerPosition = new Vector2(0, 0);
    private static int playerScale = 4;
    public static int playerHeight = 8 * playerScale;
    private float playerSpeed = 0.2f;
    Rectangle sourceRect = new Rectangle(0, 0, 8, 8); 

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        playerSheet = Content.Load<Texture2D>("spritesheet");


        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        int windowHeight = GraphicsDevice.Viewport.Height;
        float velocity = 0.5f;
        if (playerPosition.Y  >= windowHeight - playerHeight )
        {
            playerPosition.Y = windowHeight - playerHeight; // Zastavíme hráče na spodní hranici okna
            velocity = 0f; // Zastavíme vertikální pohyb (gravitace)
        }
        KeyboardState keyState = Keyboard.GetState();
        if (keyState.IsKeyDown(Keys.A))
            playerPosition.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;;
        if (keyState.IsKeyDown(Keys.D))
            playerPosition.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;;
        playerPosition.Y += velocity * gameTime.ElapsedGameTime.Milliseconds;
        base.Update(gameTime);
        
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        _spriteBatch.Draw(playerSheet, playerPosition, sourceRect, Color.White, 0f, Vector2.Zero, playerScale, SpriteEffects.None, 0f);
        _spriteBatch.End();


        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
