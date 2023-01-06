using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Screens;

namespace SAE
{
    internal class Accueil : GameScreen
    {
        //ECRAN ACCUEIL
        //titre
        private string _titre;
        private SpriteFont _policeTitre;
        private Vector2 _positionTitre;
        //fond ecran
        private Texture2D _textureFond;
        private Texture2D _textureJouer;
        private Texture2D _textureRegle;
        private Texture2D _textureCommande;
        //jouer
        private string _jouer;
        private SpriteFont _policeJouer;
        private Vector2 _positionJouer;
        public const int TAILLE_JOUER = 100;
        //règle
        private string _regle;
        private SpriteFont _policeRegle;
        private Vector2 _positionRegle;
        public const int TAILLE_REGLE = 200;
        //commande
        private string _commande;
        private SpriteFont _policeCommande;
        private Vector2 _positionCommande;
        public const int TAILLE_COMMANDE = 200;
        //quitter
        private string _quitter;
        private SpriteFont _policeQuitter;
        private Vector2 _positionQuitter;
        public const int TAILLE_QUITTER = 100;

        //musique / son
        private Song _song;


        //transition
        public SpriteBatch SpriteBatch { get; set; }


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Accueil(Game1 game) : base(game)
        {
            _myGame = game;
        }


        public override void Initialize()
        {            
            //titre
            _titre = "Haunted Manor";
            _policeTitre = Content.Load<SpriteFont>("Titre");
            _positionTitre = new Vector2(265, 150);
            //jouer
            _jouer = "Jouer";
            _policeJouer = Content.Load<SpriteFont>("Font");
            _positionJouer = new Vector2(400, 330);
            //régle
            _regle = "Regles du j eu";
            _policeRegle = Content.Load<SpriteFont>("Font");
            _positionRegle = new Vector2(600, 550);
            //commande
            _commande = "Commandes";
            _policeCommande = Content.Load<SpriteFont>("Font");
            _positionCommande = new Vector2(150, 550);
            //quitter
            _quitter = "Quitter";
            _policeQuitter = Content.Load<SpriteFont>("quitter");
            _positionQuitter = new Vector2(870, 675);
        }

        public override void LoadContent()
        {
           //TEXTURE
            _myGame.SpriteBatch = new SpriteBatch(GraphicsDevice);
            _textureFond = Content.Load<Texture2D>("accueil");
            _textureJouer = Content.Load<Texture2D>("J");
            _textureRegle = Content.Load<Texture2D>("R");
            _textureCommande = Content.Load<Texture2D>("C");

            //MUSIQUE
            _song = Content.Load<Song>("SongAccueil");

            base.LoadContent();
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _myGame.SpriteBatch.Begin();
            //ACCUEIL
            _myGame.SpriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            _myGame.SpriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _myGame.SpriteBatch.DrawString(_policeJouer, $"{_jouer}", _positionJouer, Color.White);
            _myGame.SpriteBatch.Draw(_textureJouer, new Rectangle(430, 380, 50, 50), Color.White);
            _myGame.SpriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
            _myGame.SpriteBatch.Draw(_textureRegle, new Rectangle(680, 600, 50, 50), Color.White);
            _myGame.SpriteBatch.DrawString(_policeCommande, $"{_commande}", _positionCommande, Color.White);
            _myGame.SpriteBatch.Draw(_textureCommande, new Rectangle(200, 600, 50, 50), Color.White);
            _myGame.SpriteBatch.DrawString(_policeQuitter, $"{_quitter}", _positionQuitter, Color.White);

            //GEORGE

            //     _spriteBatch.Draw(_perso, _positionPerso);
            _myGame.SpriteBatch.End();

        }
    }
}
