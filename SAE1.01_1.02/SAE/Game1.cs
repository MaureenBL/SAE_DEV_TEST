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
        //TAILLE FENETRE
        public const int TAILLE_FENETRE_L = 950;
        public const int TAILLE_FENETRE_H = 700;

        //ECRAN ACCUEIL
        //fond ecran
        private Texture2D _textureEntree;       

        //accueil
        private string _accueil;
        private SpriteFont _policeAccueil;
        private Vector2 _positionAccueil;

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
        private Song _endSong;

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
            Window.Title = "Haunted Manor"; //titre de la fenêtre

            //FENETRE
            _graphics.PreferredBackBufferWidth = TAILLE_FENETRE_L;
            _graphics.PreferredBackBufferHeight = TAILLE_FENETRE_H;
            _graphics.ApplyChanges();
            
            //Accueil
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
            _commandesTrans = new Commande(this); 
            _reglesTrans = new Regle(this);
            _jouerTrans = new Jouer2(this);
            _jeuTrans = new Jeu(this);
            _accueilTrans = new Accueil(this);
            _finTrans = new Fin(this);

            //MUSIQUE
            _song = Content.Load<Song>("SongAccueil");
            _gameSong = Content.Load<Song>("GameSong");
            _endSong = Content.Load<Song>("SongEnd");

            MediaPlayer.Play(_song);
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

         
            //Contrôle touche du clavier
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
                MediaPlayer.Volume = 0.5f;
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
                _screenManager.LoadScreen(_accueilTrans, new FadeTransition(GraphicsDevice, Color.LightGray));
                MediaPlayer.Play(_song);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                _screenManager.LoadScreen(_commandesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));                
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                _screenManager.LoadScreen(_finTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));
                MediaPlayer.Stop();
                MediaPlayer.Volume = 0.1f;
                MediaPlayer.Play(_endSong);
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

            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureEntree, new Rectangle(420, 350, 150, 100), Color.White);
            _spriteBatch.DrawString(_policeAccueil, $"{_accueil}", _positionAccueil, Color.White);            
            _spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}