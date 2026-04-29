using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Channels;

namespace Time_and_Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState;
        Texture2D bombTexture, pliersTexture, explodesTexture, winTexture;
        Rectangle window, rectBomb, resetRect, pliersRect, explodesRect, wiresRect, winRect;
        SpriteFont TimeFont;
        SoundEffect explosion;
        SoundEffectInstance explosionInstance;
        float seconds;
        bool exploded, safe;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Lesson 4 - Sound and Time";
            IsMouseVisible = false;
            
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
            safe = false;
            explodesRect = new Rectangle(-800,-500,2400,1500); 
            pliersRect = new Rectangle(0, 0, 35,35);
            wiresRect = new Rectangle(597,163,90,60);
            winRect = new Rectangle(0, 0, 800, 500);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            TimeFont = Content.Load<SpriteFont>("TimeFont");
            explosion = Content.Load<SoundEffect>("explosion");
            explosionInstance = explosion.CreateInstance();
            pliersTexture = Content.Load<Texture2D>("pliers");
            explodesTexture = Content.Load<Texture2D>("Exploded");
            winTexture = Content.Load<Texture2D>("You WIn");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            pliersRect.Location = mouseState.Position;
            this.Window.Title = mouseState.Position.ToString();
            if (mouseState.LeftButton == ButtonState.Pressed && resetRect.Contains(mouseState.Position))
                seconds = 0f;
            if (mouseState.LeftButton == ButtonState.Pressed && wiresRect.Contains(mouseState.Position))
            { seconds = 0f; safe = true; }
            if (safe && seconds >9)
            {
                Exit();
            }
            // TODO: Add your update logic here
            if (!exploded)
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds > 10 && !exploded)
            {
                exploded = true;
                //seconds = 0;
                explosionInstance.Play();
            }
            if (exploded)
            {
                if (explosionInstance.State == SoundState.Stopped)
                {/* Exit();*/ }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(bombTexture, rectBomb, Color.White);
           
             _spriteBatch.DrawString(TimeFont, seconds.ToString("00.0"), new Vector2(270,200 ), Color.Black);
            _spriteBatch.DrawString(TimeFont, ("!Cut Both Wires! "), new Vector2 (80,20), Color.Black);
            if (exploded)
            {
                _spriteBatch.Draw(explodesTexture, explodesRect, Color.White);
            }
            _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
            if (safe && !exploded)
            {
                _spriteBatch.Draw(winTexture, winRect, Color.White);
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
