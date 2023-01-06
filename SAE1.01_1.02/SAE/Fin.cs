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

        //retour
        private string _rejouer;
        private SpriteFont _policeRejouer;
        private Vector2 _positionRejouer;
        //quitter
        private string _quitter;
        private SpriteFont _policeQuitter;
        private Vector2 _positionQuitter;

        //titre
        private string _titre;
        private SpriteFont _policeTitre;
        private Vector2 _positionTitre;

        private Texture2D _textureFin;

        private Texture2D _textureRejouer;
        private Texture2D _textureEsc;
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
            _positionCongr = new Vector2(345, 270);
            base.Initialize();

            //Retour
            _rejouer = "Rejouer ";
            _policeRejouer = Content.Load<SpriteFont>("End");
            _positionRejouer = new Vector2(110, 620);
            //Quitter
            _quitter = "Quitter ";
            _policeQuitter = Content.Load<SpriteFont>("End");
            _positionQuitter = new Vector2(710, 620);
        }

        public override void LoadContent()
        {
            _myGame.SpriteBatch = new SpriteBatch(GraphicsDevice);
            _textureFin = Content.Load<Texture2D>("fin");

            _textureRejouer = Content.Load<Texture2D>("G");
            _textureEsc = Content.Load<Texture2D>("esc");
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

            _myGame.SpriteBatch.DrawString(_policeRejouer, $"{_rejouer}", _positionRejouer, Color.White);
            _myGame.SpriteBatch.DrawString(_policeQuitter, $"{_quitter}", _positionQuitter, Color.White);

            _myGame.SpriteBatch.Draw(_textureRejouer, new Rectangle(250, 620, 50, 45), Color.White);
            _myGame.SpriteBatch.Draw(_textureEsc, new Rectangle(650, 620, 50, 50), Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}
