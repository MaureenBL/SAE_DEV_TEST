using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Microsoft.Xna.Framework.Media;

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
        private Texture2D _textureEntree;
        private Vector2 _positionFond;
       
        //acces
        private MouseState _mouseState;
        private KeyboardState _keyboardState;

        //quitter
        private string _accueil;
        private SpriteFont _policeAccueil;
        private Vector2 _positionAccueil;
       // public const int TAILLE_QUITTER = 100;


        //transition
        private readonly ScreenManager _screenManager;
        private Commande _commandesTrans;
        private Regle _reglesTrans;
        private Jouer2 _jouerTrans;
        private Jeu _jeuTrans;
        private Fin _finTrans;
        private Accueil _accueilTrans;
        public SpriteBatch SpriteBatch { get; set; }

        //musique / son
        private Song _song;
        private Song _gameSong;

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


              

               //propriétés des monstres
               //_vitesseBat = 0;
               //_vitesseGhost = 0;
               //_vitesseSkeleton = 100;
            */

            //accueil
            _accueil = "Pour commencer clique sur :";
            _policeAccueil = Content.Load<SpriteFont>("Font");
            _positionAccueil = new Vector2(300, 250);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureEntree = Content.Load<Texture2D>("entree");

            //TRANSITION
            _commandesTrans = new Commande(this); // en leur donnant une référence au Game
            _reglesTrans = new Regle(this);
            _jouerTrans = new Jouer2(this);
            _jeuTrans = new Jeu(this);
            _accueilTrans = new Accueil(this);
            _finTrans = new Fin(this);

            //GEORGE            
            /*     SpriteSheet spriteSheet = Content.Load<SpriteSheet>("george.sf", new JsonContentLoader()); //NE MARCHE PAS
                _perso = new AnimatedSprite(spriteSheet);*/

            //MUSIQUE
            _song = Content.Load<Song>("SongAccueil");
            _gameSong = Content.Load<Song>("GameSong");

            MediaPlayer.Play(_song);
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.2f;
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

    /*        KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space))
            {
               
               // _screenManager.LoadScreen(_reglesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow)); // NE MARCHE PAS 
            }*/
            //ACCUEIL

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                _screenManager.LoadScreen(_reglesTrans, new FadeTransition(GraphicsDevice, Color.LightGray));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.J))
            {
                _screenManager.LoadScreen(_jeuTrans, new FadeTransition(GraphicsDevice, Color.LightGray));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                _screenManager.LoadScreen(_jouerTrans, new FadeTransition(GraphicsDevice, Color.LightGray));
                MediaPlayer.Stop();
                MediaPlayer.Play(_gameSong);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                _screenManager.LoadScreen(_accueilTrans, new FadeTransition(GraphicsDevice, Color.LightGray));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                _screenManager.LoadScreen(_accueilTrans, new FadeTransition(GraphicsDevice, Color.LightGray));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                _screenManager.LoadScreen(_jeuTrans, new FadeTransition(GraphicsDevice, Color.LightGray));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                _screenManager.LoadScreen(_commandesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                _screenManager.LoadScreen(_finTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
                      
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //ACCUEIL
            _spriteBatch.Draw(_textureEntree, new Rectangle(420, 350, 150, 100), Color.White);
            _spriteBatch.DrawString(_policeAccueil, $"{_accueil}", _positionAccueil, Color.White);
            /*  _spriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
              _spriteBatch.DrawString(_policeJouer, $"{_jouer}", _positionJouer, Color.White);
              _spriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
              _spriteBatch.DrawString(_policeCommande, $"{_commande}", _positionCommande, Color.White); 
              _spriteBatch.DrawString(_policeQuitter, $"{_quitter}", _positionQuitter, Color.White); 

              //GEORGE

              //     _spriteBatch.Draw(_perso, _positionPerso);*/
            _spriteBatch.End(); 


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