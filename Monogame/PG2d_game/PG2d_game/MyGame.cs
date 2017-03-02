using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace PG2d_game
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MyGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D chrono_assets;
        Texture2D chrono_pipeline;
        Texture2D magusSprite;


        Vector2 position;


        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            position = Vector2.Zero;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            chrono_pipeline = Content.Load<Texture2D>("chrono_pipeline");
            chrono_assets = Texture2D.FromStream(GraphicsDevice, File.OpenRead("Assets/chrono_content.png"));
            magusSprite = Content.Load<Texture2D>("magus_small");


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A))
            {
                position.X -= 100 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.D))
            {
                position.X += 100 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



            spriteBatch.Begin();
            // TODO: Add your drawing code here
            spriteBatch.Draw(chrono_pipeline, Vector2.Zero, Color.White);
            spriteBatch.Draw(chrono_assets, new Vector2(GraphicsDevice.Viewport.Width - chrono_assets.Width, 0), Color.White);

            spriteBatch.Draw(magusSprite, position);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
