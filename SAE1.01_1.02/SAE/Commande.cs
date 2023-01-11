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
    internal class Commande : GameScreen
    {
        private Game1 _myGame;


        //titre
        private string _titre;
        private SpriteFont _policeTitre;
        private Vector2 _positionTitre;

        //avancer
        private string _avancer;
        private SpriteFont _policeAvancer;
        private Vector2 _positionAvancer;
        //reculer
        private string _reculer;
        private SpriteFont _policeReculer;
        private Vector2 _positionReculer;
        //droite
        private string _droite;
        private SpriteFont _policeDroite;
        private Vector2 _positionDroite;
        //gauche
        private string _gauche;
        private SpriteFont _policeGauche;
        private Vector2 _positionGauche;
        //retour
        private string _retour;
        private SpriteFont _policeRetour;
        private Vector2 _positionRetour;
        //quitter
        private string _quitter;
        private SpriteFont _policeQuitter;
        private Vector2 _positionQuitter;
        //texture
        private Texture2D _textureFond;
        private Texture2D _textureH;
        private Texture2D _textureB;
        private Texture2D _textureD;
        private Texture2D _textureG;
        private Texture2D _textureRetour;
        private Texture2D _textureEsc;
        //acces
        private MouseState _mouseState;
        private KeyboardState _keyboardState;

        //transition
        private readonly ScreenManager _screenManager;
        private Game1 _game1;
        public SpriteBatch SpriteBatch { get; set; }

        private GraphicsDeviceManager _graphics;

        public Commande(Game1 game) : base(game)
        {
            _myGame = game;
        }


        public override void Initialize()
        {
            //titre
            _titre = "Haunted Manor";
            _policeTitre = Content.Load<SpriteFont>("Titre");
            _positionTitre = new Vector2(250, 110);

            //avancer
            _avancer = "Avancer : ";
            _policeAvancer = Content.Load<SpriteFont>("Font");
            _positionAvancer = new Vector2(150, 230);
            //reculer
            _reculer = "Reculer : ";
            _policeReculer = Content.Load<SpriteFont>("Font");
            _positionReculer = new Vector2(150, 280);
            //Droite
            _droite = "Droite : ";
            _policeDroite = Content.Load<SpriteFont>("Font");
            _positionDroite = new Vector2(150, 330);
            //Gauche
            _gauche = "Gauche : ";
            _policeGauche = Content.Load<SpriteFont>("Font");
            _positionGauche = new Vector2(150, 380); 
            //Retour
            _retour = "Retour : ";
            _policeRetour = Content.Load<SpriteFont>("Font");
            _positionRetour = new Vector2(150, 430);
            //Quitter
            _quitter = "Quitter : ";
            _policeQuitter = Content.Load<SpriteFont>("Font");
            _positionQuitter = new Vector2(150, 480);
        }
        public override void LoadContent()
        {
            _textureFond = Content.Load<Texture2D>("accueil");
            _textureH = Content.Load<Texture2D>("flechesH");
            _textureB = Content.Load<Texture2D>("flecheBas");
            _textureD = Content.Load<Texture2D>("flechesD");
            _textureG = Content.Load<Texture2D>("flechesG");
            _textureRetour = Content.Load<Texture2D>("retour");
            _textureEsc = Content.Load<Texture2D>("esc");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Initialize(); 
            }
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Blue); // on utilise la reference vers Game1 pour changer le graphisme
            
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            //texte
            _myGame.SpriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _myGame.SpriteBatch.DrawString(_policeAvancer, $"{_avancer}", _positionAvancer, Color.White);
            _myGame.SpriteBatch.DrawString(_policeReculer, $"{_reculer}", _positionReculer, Color.White);
            _myGame.SpriteBatch.DrawString(_policeDroite, $"{_droite}", _positionDroite, Color.White);
            _myGame.SpriteBatch.DrawString(_policeGauche, $"{_gauche}", _positionGauche, Color.White);
            _myGame.SpriteBatch.DrawString(_policeRetour, $"{_retour}", _positionRetour, Color.White);
            _myGame.SpriteBatch.DrawString(_policeQuitter, $"{_quitter}", _positionQuitter, Color.White);

            //texture
            _myGame.SpriteBatch.Draw(_textureH, new Rectangle(290, 230, 50, 50), Color.White);
            _myGame.SpriteBatch.Draw(_textureB, new Rectangle(280, 280, 50, 50), Color.White);
            _myGame.SpriteBatch.Draw(_textureD, new Rectangle(290, 330, 50, 50), Color.White);
            _myGame.SpriteBatch.Draw(_textureG, new Rectangle(280, 380, 50, 50), Color.White);
            _myGame.SpriteBatch.Draw(_textureRetour, new Rectangle(290, 430, 50, 45), Color.White);
            _myGame.SpriteBatch.Draw(_textureEsc, new Rectangle(280, 480, 50, 50), Color.White);
            _myGame.SpriteBatch.End();

        }
    }
}
