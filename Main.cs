﻿using System.Collections.Generic;
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
        public static SpriteSortMode spriteSortMode = SpriteSortMode.Deferred;
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
        public static Keys keyLeft = Keys.A;
        public static Keys keyRight = Keys.D;
        public static Keys keyUp = Keys.W;
        public static Keys keyDown = Keys.S;
        public static Keys keyJump = Keys.Space;

        //Info
        public static double deltaTime = 1f;
        public static int fps = 144;
        public const int consistentUpdates = 60;

        //Optimization
        public const float physicsBackstepSpace = 0.1f;

        //Object Lists
        public const int maxPlayerCount = 1;
        public static Player[] players = new Player[maxPlayerCount];
        public const int maxProjectileCount = 750;
        public static Projectile[] projectiles = new Projectile[maxProjectileCount];
        public const int maxPlatformCount = 1000;
        public static Platform[] platforms = new Platform[maxPlatformCount];
        public const int maxObjectCount = maxPlayerCount + maxProjectileCount + maxPlatformCount;
        public static GameObject[] objects = new GameObject[maxObjectCount];

        //Other
        public static Random random;
        public static Vector2 mouseScreen;
        public static Vector2 mouseWorld;

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

            if (players[0] == null)
            {
                Player.NewPlayer();
            }

            mouseScreen = mousestate.Position.ToVector2();
            mouseWorld = mouseScreen + screenPosition;

            timeSinceConsistent += deltaTime;
            while (timeSinceConsistent >= 1f / (float)consistentUpdates)
            {
                timeSinceConsistent -= 1f / (float)consistentUpdates;
                ConsistentUpdate(gameTime);
            }

            for (int i = 0; i < objects.Length; i++)
            {
                GameObject gameObject = objects[i];
                if (gameObject != null)
                {
                    if (gameObject.active)
                    {
                        gameObject.Update(gameTime);
                    }
                }
            }

            base.Update(gameTime);
        }

        public void ConsistentUpdate(GameTime gameTime)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (i < objects.Length)
                {
                    GameObject gameObject = objects[i];
                    if (gameObject != null)
                    {
                        gameObject.ConsistentUpdate(gameTime);
                    }
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
            DrawPlatforms(gameTime, spriteBatch, 0f);
            DrawProjectiles(gameTime, spriteBatch, 0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawPlayers(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            for (int i = 0; i < players.Length; i++)
            {
                Player player = players[i];
                if (player != null && player.active)
                {
                    player.Draw(gameTime, spriteBatch, layerDepth);
                }
            }
        }

        public void DrawPlatforms(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                Platform platform = platforms[i];
                if (platform != null && platform.active)
                {
                    platform.Draw(gameTime, spriteBatch, layerDepth);
                }
            }
        }

        public void DrawProjectiles(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            for (int i = 0; i < projectiles.Length; i++)
            {
                Projectile projectile = projectiles[i];
                if (projectile != null && projectile.active)
                {
                    projectile.Draw(gameTime, spriteBatch, layerDepth);
                }
            }
        }
    }
}
