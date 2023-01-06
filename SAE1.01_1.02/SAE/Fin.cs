using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Content;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;


namespace SAE
{
    internal class Fin : GameScreen
    {
        private Game1 _myGame;

        //Regle
        private string _regle;
        private SpriteFont _policeRegle;
        private Vector2 _positionRegle;
        
        //congr
        private string _congr;
        private SpriteFont _policeCongr;
        private Vector2 _positionCongr;


        //titre
        private string _titre;
        private SpriteFont _policeTitre;
        private Vector2 _positionTitre;

        private Texture2D _textureFin;
        public SpriteBatch SpriteBatch { get; set; }


        public Fin(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {

            //titre
            _titre = "~ FIN ~";
            _policeTitre = Content.Load<SpriteFont>("Titre");
            _positionTitre = new Vector2(350, 45);

            _regle = "Woaw ! Je n'en attendais pas moins de toi. Tu as eu toutes \n les clefs  et tu ne t'aies pas " +
                "fais avoir  par les monstres. ";
            _policeRegle = Content.Load<SpriteFont>("End");
            _positionRegle = new Vector2(2, 350);

            _congr = " Congratulations !! ";
            _policeCongr = Content.Load<SpriteFont>("End");
            _positionCongr = new Vector2(240, 560);
            base.Initialize();
        }

        public override void LoadContent()
        {
            _myGame.SpriteBatch = new SpriteBatch(GraphicsDevice);
            _textureFin = Content.Load<Texture2D>("fin");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Yellow); // on utilise la reference vers Game1 pour changer le graphisme

            _myGame.SpriteBatch.Begin();

            _myGame.SpriteBatch.Draw(_textureFin, new Rectangle(0, 0, 1000, 700), Color.White);
            _myGame.SpriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _myGame.SpriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.LightBlue);
            _myGame.SpriteBatch.DrawString(_policeCongr, $"{_congr}", _positionCongr, Color.LightBlue);
            _myGame.SpriteBatch.End();
        }
    }
}
