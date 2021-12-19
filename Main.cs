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

namespace GameBase
{
    public class Main : Game
    {
        //Graphics
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static SpriteSortMode _spriteSortMode = SpriteSortMode.FrontToBack;
        public static BlendState _blendState = BlendState.AlphaBlend;
        public static SamplerState _samplerState = null;
        public static DepthStencilState _depthStencilState = DepthStencilState.None;
        public static RasterizerState _rasterizerState = null;
        public static Matrix? _gameViewMatrix = null;

        //Controls
        public static KeyboardState keyboardstate;
        public static GamePadState gamepadstate;
        public static MouseState mousestate;
        public static Vector2 cursorPosition = Vector2.Zero;

        //Info
        public static double _deltaTime = 1f;
        public static int _fps = 144;

        //Object Lists
        public const int maxPlayerCount = 1;
        public static List<GameObject> objects = new List<GameObject>();
        public static List<Player> players = new List<Player>(maxPlayerCount);

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds((double)(1f / 144f));
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            _fps = (int)(1f / (float)gameTime.ElapsedGameTime.TotalSeconds);

            gamepadstate = GamePad.GetState(PlayerIndex.One);
            keyboardstate = Keyboard.GetState();
            mousestate = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(_spriteSortMode, _blendState, null, _depthStencilState, null, null, _gameViewMatrix);
            DrawPlayers(gameTime, _spriteBatch, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void DrawPlayers(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            foreach (Player player in players)
            {
                player.Draw(spriteBatch);
            }
        }
    }
}
