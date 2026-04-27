using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState;
        Texture2D bombTexture, pliersTexture;
        Rectangle window, rectBomb, resetRect, pliersRect;
        SpriteFont TimeFont;
        SoundEffect explosion;
        float seconds;
        bool exploded;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Lesson 4 - Sound and Time";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0,0, 800,500);
            _graphics.PreferredBackBufferWidth = window.Width;
                _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            rectBomb = new Rectangle(50, 50, 700, 400);
            resetRect = new Rectangle(253, 132,10 ,16 );
            seconds = 0f;
            exploded = false;
            pliersRect = new Rectangle(0, 0, 0,0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            TimeFont = Content.Load<SpriteFont>("TimeFont");
            explosion = Content.Load<SoundEffect>("explosion");
            pliersTexture = Content.Load<Texture2D>("pliers");
        }//dfghviure

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            this.Window.Title = mouseState.Position.ToString();
            if (mouseState.LeftButton == ButtonState.Pressed && resetRect.Contains(mouseState.Position))
                seconds = 0f;
            // TODO: Add your update logic here
            if (!exploded)
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > 10)
            {
                exploded = true;
                seconds = 0;
                explosion.Play();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(bombTexture, rectBomb, Color.White);
             _spriteBatch.DrawString(TimeFont, seconds.ToString("00.0"), new Vector2(270,200 ), Color.Black); 


            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
