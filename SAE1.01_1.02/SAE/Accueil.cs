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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Vector2 _positionFond;
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
        //acces
        private MouseState _mouseState;
        private KeyboardState _keyboardState;

        //transition
        private readonly ScreenManager _screenManager;
        private Commande _commandesTrans;
        private Regle _reglesTrans;
        private Jouer2 _jouerTrans;
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

            //ACCUEIL
            _positionFond = new Vector2(700, 900);
            //titre
            _titre = "Haunted Manor";
            _policeTitre = Content.Load<SpriteFont>("Titre");
            _positionTitre = new Vector2(265, 150);
            //jouer
            _jouer = "JOUER";
            _policeJouer = Content.Load<SpriteFont>("Font");
            _positionJouer = new Vector2(400, 330);
            //régle
            _regle = "Regles du jeu";
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureFond = Content.Load<Texture2D>("accueil");

            //TRANSITION

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            //ACCUEIL
            _mouseState = Mouse.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_mouseState.X >= _positionCommande.X && _mouseState.Y >= _positionCommande.Y && _mouseState.X <= _positionCommande.X + TAILLE_COMMANDE && _mouseState.Y <= _positionCommande.Y + TAILLE_COMMANDE)
                {
                    _screenManager.LoadScreen(_commandesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
                }
                else if (_mouseState.X >= _positionRegle.X && _mouseState.Y >= _positionRegle.Y && _mouseState.X <= _positionRegle.X + TAILLE_REGLE && _mouseState.Y <= _positionRegle.Y + TAILLE_REGLE)
                {
                    _screenManager.LoadScreen(_reglesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
                }
                else if (_mouseState.X >= _positionJouer.X && _mouseState.Y >= _positionJouer.Y && _mouseState.X <= _positionJouer.X + TAILLE_JOUER && _mouseState.Y <= _positionJouer.Y + TAILLE_JOUER)
                {
                    _screenManager.LoadScreen(_jouerTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
                }
                else if (_mouseState.X >= _positionQuitter.X && _mouseState.Y >= _positionQuitter.Y && _mouseState.X <= _positionQuitter.X + TAILLE_QUITTER && _mouseState.Y <= _positionQuitter.Y + TAILLE_QUITTER)
                {
                   
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
             // on utilise la reference vers
                                                     // Game1 pour changer le graphisme
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //ACCUEIL
            _spriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            _spriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _spriteBatch.DrawString(_policeJouer, $"{_jouer}", _positionJouer, Color.White);
            _spriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
            _spriteBatch.DrawString(_policeCommande, $"{_commande}", _positionCommande, Color.White);
            _spriteBatch.DrawString(_policeQuitter, $"{_quitter}", _positionQuitter, Color.White);

            //GEORGE

            //     _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();

        }
    }
}
