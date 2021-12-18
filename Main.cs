using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace GameBase
{
    public class Main : Game
    {
        //Graphics
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public SpriteSortMode _spriteSortMode = SpriteSortMode.FrontToBack;
        public BlendState _blendState = BlendState.AlphaBlend;
        public SamplerState _samplerState = null;
        public DepthStencilState _depthStencilState = DepthStencilState.None;
        public RasterizerState _rasterizerState = null;
        public Matrix? _gameViewMatrix = null;

        //Controls
        public KeyboardState keyboardstate;
        public GamePadState gamepadstate;
        public MouseState mousestate;
        public Vector2 cursorPosition = Vector2.Zero;

        //Info
        public static double _deltaTime = 1f;
        public static int _fps = 144;

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
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
