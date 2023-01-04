using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE
{
    public class Game1 : Game
    {

        //PERSONNAGE
        private Texture2D _texturePerso;
        private Vector2 _positionPerso;
        private int _sensPerso;
        private int _vitessePerso;


        //MONSTRES

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Test";
            base.Initialize();
            //beredsferd
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
           
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.Red);
            base.Draw(gameTime);
        }
        public bool Collision(int xObjetA, int yObjetA, int largeurObjetA, int hauteurObjetA, int xObjetB, int yObjetB, int largeurObjetB, int largeurObjetB)
        {
            Rectangle rectOb = new Rectangle(xObjetA, yObjetA, LARGEUR_objetA, HAUTEUR_objetB);
            Rectangle rectPereNoel = new Rectangle((int)_positionPereNoel.X, (int)_positionPereNoel.Y, LARGEUR_PERE_NOEL, HAUTEUR_PERE_NOEL);
            return rectCadeau.Intersects(rectPereNoel);
        }
    }
}