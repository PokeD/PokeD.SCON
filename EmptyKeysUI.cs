using System;

using EmptyKeys.UserInterface;
using EmptyKeys.UserInterface.Debug;
using EmptyKeys.UserInterface.Generated;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Media;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using PokeD.SCON.UILibrary;

namespace PokeD.SCON
{
    public class EmptyKeysUI : Game
    {
        public static Point DefaultResolution => new Point(1024, 768);

        GraphicsDeviceManager Graphics { get; }

        BasicUI BasicUI { get; set; }
        BasicUIViewModel ViewModel { get; set; }
        DebugViewModel DebugViewMode { get; set; }

        SCONClient SCONClient { get; set; }

        public EmptyKeysUI(Action<Game> platformCode, bool fullscreen = false)
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreparingDeviceSettings += Graphics_PreparingDeviceSettings;
            Graphics.DeviceCreated += Graphics_DeviceCreated;
            Graphics.IsFullScreen = fullscreen;
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            platformCode?.Invoke(this);
        }
        private void Graphics_DeviceCreated(object sender, EventArgs e)
        {
            var Engine = new MonoGameEngine(GraphicsDevice, Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
        }
        private void Graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            Graphics.PreferredBackBufferWidth = DefaultResolution.X;
            Graphics.PreferredBackBufferHeight = DefaultResolution.Y;

            //Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Graphics.SynchronizeWithVerticalRetrace = true;
            //Graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
        }

        public void OnResize(object sender, EventArgs e)
        {
            if (Graphics.GraphicsDevice.Viewport.Width < DefaultResolution.X || Graphics.GraphicsDevice.Viewport.Height < DefaultResolution.Y)
            {
                Resize(DefaultResolution);
                return;
            }

            BasicUI?.Resize(Graphics.GraphicsDevice.Viewport.Width, Graphics.GraphicsDevice.Viewport.Height);
        }
        public void Resize(Point size)
        {
            if (size.X < DefaultResolution.X || size.Y < DefaultResolution.Y)
                return;

            Graphics.PreferredBackBufferWidth = size.X;
            Graphics.PreferredBackBufferHeight = size.Y;

            Graphics.ApplyChanges();
        }


        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            IsMouseVisible = true;

            FontManager.DefaultFont = Engine.Instance.Renderer.CreateFont(Content.Load<SpriteFont>("Segoe_UI_10_Regular"));

            BasicUI = new BasicUI(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            ViewModel = new BasicUIViewModel();
            BasicUI.DataContext = ViewModel;
            DebugViewMode = new DebugViewModel(BasicUI);

            FontManager.Instance.LoadFonts(Content);
            ImageManager.Instance.LoadImages(Content);
            SoundManager.Instance.LoadSounds(Content);

            BasicUI.InputBindings.Add(new KeyBinding(new RelayCommand(o => Exit()), KeyCode.Escape, ModifierKeys.None));

            SCONClient = new SCONClient(ViewModel);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            SCONClient.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            SCONClient.Update();

            DebugViewMode.Update();
            BasicUI.UpdateInput(gameTime.ElapsedGameTime.TotalMilliseconds);

            ViewModel.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            BasicUI.UpdateLayout(gameTime.ElapsedGameTime.TotalMilliseconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            BasicUI.Draw(gameTime.ElapsedGameTime.TotalMilliseconds);
            DebugViewMode.Draw();

            base.Draw(gameTime);
        }
    }
}
