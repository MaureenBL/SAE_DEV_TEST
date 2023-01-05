using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace SAE
{
   
    public class Game1 : Game
    {

        //PERSONNAGE - GEORGE
        private AnimatedSprite _perso;
        private Vector2 _positionPerso;
        private Vector2 _sensPersoHorizontal;
        private Vector2 _sensPersoVertical;
        private int _vitessePerso;
        private int _nbVie;
        public const int LARGEUR_PERSO = 200;
        public const int HAUTEUR_PERSO = 154;

        //TAILLE FENETRE
        public const int TAILLE_FENETRE_L = 950;
        public const int TAILLE_FENETRE_H = 700;


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
        private Accueil _accueilTrans;
        public SpriteBatch SpriteBatch { get; set; }


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _screenManager = new ScreenManager();
            Components.Add(_screenManager);
        }

       
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Haunted Manor"; //titre de la fenêtre

            //FENETRE
            _graphics.PreferredBackBufferWidth = TAILLE_FENETRE_L;
            _graphics.PreferredBackBufferHeight = TAILLE_FENETRE_H;
            _graphics.ApplyChanges();

            //PERSO
         /*   _vitessePerso = 100;
            _sensPersoHorizontal = Vector2.Normalize(new Vector2(1, 0));
            _sensPersoVertical = Vector2.Normalize(new Vector2(0, 1));
            _nbVie = 3;
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

            //propriétés des monstres
            //_vitesseBat = 0;
            //_vitesseGhost = 0;
            //_vitesseSkeleton = 100;
         */
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureFond = Content.Load<Texture2D>("accueil");

            //TRANSITION
            _commandesTrans = new Commande(this); // en leur donnant une référence au Game
            _reglesTrans = new Regle(this);
            _jouerTrans = new Jouer2(this);
            _accueilTrans = new Accueil(this);

            //GEORGE            
            /*     SpriteSheet spriteSheet = Content.Load<SpriteSheet>("george.sf", new JsonContentLoader()); //NE MARCHE PAS
                _perso = new AnimatedSprite(spriteSheet);*/

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //ACCUEIL

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _screenManager.LoadScreen(_reglesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _screenManager.LoadScreen(_jouerTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _screenManager.LoadScreen(_accueilTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _screenManager.LoadScreen(_commandesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
            }
            

      /*      _mouseState = Mouse.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_mouseState.X >= _positionCommande.X && _mouseState.Y >= _positionCommande.Y && _mouseState.X <= _positionCommande.X + TAILLE_COMMANDE && _mouseState.Y <= _positionCommande.Y + TAILLE_COMMANDE)
                {
                    _screenManager.LoadScreen(_commandesTrans, new FadeTransition(GraphicsDevice,Color.LightGoldenrodYellow));
                }
                else if (_mouseState.X >= _positionRegle.X && _mouseState.Y >= _positionRegle.Y && _mouseState.X <= _positionRegle.X + TAILLE_REGLE && _mouseState.Y <= _positionRegle.Y + TAILLE_REGLE)
                {
                    _screenManager.LoadScreen(_reglesTrans, new FadeTransition(GraphicsDevice,Color.LightGoldenrodYellow));
                }
                else if (_mouseState.X >= _positionJouer.X && _mouseState.Y >= _positionJouer.Y && _mouseState.X <= _positionJouer.X + TAILLE_JOUER && _mouseState.Y <= _positionJouer.Y + TAILLE_JOUER)
                {
                    _screenManager.LoadScreen(_jouerTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
                }
                else if(_mouseState.X >= _positionQuitter.X && _mouseState.Y >= _positionQuitter.Y && _mouseState.X <= _positionQuitter.X + TAILLE_QUITTER && _mouseState.Y <= _positionQuitter.Y + TAILLE_QUITTER)
                {
                    Exit();
                } 
            } */


          //GEORGE
            //  _perso.Play("gBas"); // une des animations définies dans « george.sf »

         /*
            //Déplacement
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();
            //flèche droite
            if(_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                //animation droite
                _positionPerso += _sensPersoHorizontal * _vitessePerso * deltaTime;
            }
            //flèche gauche
            if(_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                //animation gauche
                _positionPerso -= _sensPersoHorizontal * _vitessePerso * deltaTime;
            }
            //flèche haut
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _vitesseSkeleton = 100;
            }
            //flèche bas
            if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                //animation bas
                _positionPerso -= _sensPersoVertical * _vitessePerso * deltaTime;
            }
            */
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Pink);

            // TODO: Add your drawing code here
          /*  _spriteBatch.Begin();
            //ACCUEIL
            _spriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            _spriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _spriteBatch.DrawString(_policeJouer, $"{_jouer}", _positionJouer, Color.White);
            _spriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
            _spriteBatch.DrawString(_policeCommande, $"{_commande}", _positionCommande, Color.White); 
            _spriteBatch.DrawString(_policeQuitter, $"{_quitter}", _positionQuitter, Color.White); 

            //GEORGE

            //     _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End(); */


            base.Draw(gameTime);
        }


        /* public bool Collision(int xObjetA, int yObjetA, int largeurObjetA, int hauteurObjetA, int xObjetB, int yObjetB, int largeurObjetB, int largeurObjetB)
         {
             Rectangle rectObjetA = new Rectangle(xObjetA, yObjetA, largeurObjetA, hauteurObjetA,);
             Rectangle rectObjetB = new Rectangle(xObjetB, yObjetB, largeurObjetB, largeurObjetB);
             return rectObjetA.Intersects(rectObjetB);
         }*/

    }
}