using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

namespace SAE
{
    public class Game1 : Game
    {

        //PERSONNAGE - GEORGE
        private AnimatedSprite _perso;
        private Vector2 _positionPerso;
        private int _sensPerso;
        private int _vitessePerso;
        public const int LARGEUR_PERSO = 200;
        public const int HAUTEUR_PERSO = 154;


        //MONSTRES
        //animation
        private AnimatedSprite _bat;
        private AnimatedSprite _ghost;
        private AnimatedSprite _skeleton;
        //position
        private Vector2 _batPosition;
        private Vector2 _ghostPosition;
        private Vector2 _skeletonPosition;
        //orientation
        private int _batOrientation;
        private int _ghostOrientation;
        private int _skeletonOrientation;
        //dimentions
        public const int BAT_LARGEUR = 48;
        public const int BAT_HAUTEUR = 64;
        public const int GHOST_LARGEUR = 64;
        public const int GHOST_HAUTEUR = 64;
        public const int SKELETON_LARGEUR = 64;
        public const int SKELETON_HAUTEUR = 64;



        //TAILLE FENETRE
        public const int TAILLE_FENETRE_L = 950;
        public const int TAILLE_FENETRE_H = 700;



        //ECRAN ACCUEIL
        //fond ecran
        private Texture2D _textureFond;
        private Vector2 _positionFond;
        //jouer
        private string _jouer;
        private SpriteFont _policeJouer;
        private Vector2 _positionJouer;
        //règle
        private string _regle;
        private SpriteFont _policeRegle;
        private Vector2 _positionRegle;
        //commande
        private string _commande;
        private SpriteFont _policeCommande;
        private Vector2 _positionCommande;


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
            Window.Title = "Haunted Manor"; //titre de la fenêtre

            //FENETRE
            _graphics.PreferredBackBufferWidth = TAILLE_FENETRE_L;
            _graphics.PreferredBackBufferHeight = TAILLE_FENETRE_H;
            _graphics.ApplyChanges();

            //ACCUEIL
            _positionFond = new Vector2(700, 900);
            //jouer
            _jouer = "JOUER";
            _policeJouer = Content.Load<SpriteFont>("Font");
            _positionJouer = new Vector2(400, 300);
            //régle
            _regle = "Regles du jeu";
            _policeRegle = Content.Load<SpriteFont>("Font");
            _positionRegle = new Vector2(600, 550);
            //commande
            _commande = "Commandes";
            _policeCommande = Content.Load<SpriteFont>("Font");
            _positionCommande = new Vector2(150, 550); 

            base.Initialize();
            //beredsferd
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureFond = Content.Load<Texture2D>("accueil");

            //GEORGE            
            /*     SpriteSheet spriteSheet = Content.Load<SpriteSheet>("george.sf", new JsonContentLoader()); //NE MARCHE PAS
                _perso = new AnimatedSprite(spriteSheet);*/

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
         /*   if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //GEORGE
          //  _perso.Play("gBas"); // une des animations définies dans « george.sf »
            
           //SQUELETTE
            if(Voir())
            {
                //Foncer sur le héros
                
            }
            else
            {
                //Roder sur la map en faisant un trait
            }

           //FANTOME
            if(Collision entre joueur et zone de spawn)
            {
                
            }
         */
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            //ACCUEIL
            _spriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            _spriteBatch.DrawString(_policeJouer, $"{_jouer}", _positionJouer, Color.White);
            _spriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
            _spriteBatch.DrawString(_policeCommande, $"{_commande}", _positionCommande, Color.White); 

            //GEORGE

            //     _spriteBatch.Draw(_perso, _positionPerso);
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