using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using GameBase.Extensions;
using GameBase.Objects;
using System.Diagnostics;

namespace GameBase
{
    public class Main : Game
    {
        //Graphics
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static SpriteSortMode spriteSortMode = SpriteSortMode.FrontToBack;
        public static BlendState blendState = BlendState.AlphaBlend;
        public static SamplerState samplerState = null;
        public static DepthStencilState depthStencilState = DepthStencilState.None;
        public static RasterizerState rasterizerState = null;
        public static Matrix? gameViewMatrix = null;
        public static float screenWidth;
        public static float screenHeight;
        public static Vector2 screenPosition = Vector2.Zero;
        public static bool DisplayFPS = true;

        //Controls
        public static KeyboardState keyboardstate;
        public static GamePadState gamepadstate;
        public static MouseState mousestate;
        public static Vector2 cursorPosition = Vector2.Zero;

        //Info
        public static double deltaTime = 1f;
        public static int fps = 144;
        public const int consistentUpdates = 60;

        //Optimization
        public const float physicsBackstepSpace = 0.1f;

        //Object Lists
        public static List<GameObject> objects = new List<GameObject>();
        public const int maxPlayerCount = 1;
        public static List<Player> players = new List<Player>();
        public const int maxProjectileCount = 500;
        public static List<Projectile> projectiles = new List<Projectile>();

        //Other
        public Random random;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds((double)(1f / 144f));
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            screenWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            screenHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            timeSinceConsistent = 0f;
            random = new Random();

        }

        public static Texture2D Glow_2;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Glow_2 = Content.Load<Texture2D>("Glow_2");
        }

        double timeSinceConsistent = 0f;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            fps = (int)(1f / (float)gameTime.ElapsedGameTime.TotalSeconds);

            gamepadstate = GamePad.GetState(PlayerIndex.One);
            keyboardstate = Keyboard.GetState();
            mousestate = Mouse.GetState();

            timeSinceConsistent += deltaTime;
            while (timeSinceConsistent >= 1f / (float)consistentUpdates)
            {
                timeSinceConsistent -= 1f / (float)consistentUpdates;
                ConsistentUpdate(gameTime);
            }

            foreach (GameObject gameObject in objects)
            {
                gameObject.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public void ConsistentUpdate(GameTime gameTime)
        {
            Projectile.NewProjectile(new ProjectileType(), new Vector2(screenWidth / 2, screenHeight / 2), new Vector2(random.Next(-50, 50), random.Next(-50, 50)), 0f, AlignmentKey.Pacifist);
            for (int i = 0; i < objects.Count; i++)
            {
                if (i < objects.Count)
                {
                    GameObject gameObject = objects[i];
                    gameObject.ConsistentUpdate(gameTime);
                }
                else
                {
                    break;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(spriteSortMode, blendState, null, depthStencilState, null, null, gameViewMatrix);
            DrawPlayers(gameTime, spriteBatch, 0f);
            DrawProjectiles(gameTime, spriteBatch, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawPlayers(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            foreach (Player player in players)
            {
                player.Draw(gameTime, spriteBatch, layerDepth);
            }
        }

        public void DrawProjectiles(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(gameTime, spriteBatch, layerDepth);
            }
        }
    }
}
