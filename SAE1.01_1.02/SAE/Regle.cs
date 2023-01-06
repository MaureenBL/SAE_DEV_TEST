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
    internal class Regle : GameScreen
    {

        //TAILLE FENETRE
        public const int TAILLE_FENETRE_L = 950;
        public const int TAILLE_FENETRE_H = 700;

        private SpriteBatch _spriteBatch;

        private MouseState _mouseState;


        public const int TAILLE_POLICE = 24;
        private bool _finPartie;
        //game over 
        //titre
        private string _gameOver;
        private Vector2 _positionGameOver;
        //Rejouer
        private string _rejouer;
        private Vector2 _positionRejouer;
        private Rectangle _rectRejouer;
        //Rejouer
      /*  private string _quitter;
        private Vector2 _positionQuitter;
        private Rectangle _rectQuitter; */
        //Score
        private Vector2 _positionScorePartie;

        private SpriteFont _police;

        //titre
        private string _titre;
        private SpriteFont _policeTitre;
        private Vector2 _positionTitre;

        //Regle
        private string _regle;
        private SpriteFont _policeRegle;
        private Vector2 _positionRegle;

        private Texture2D _textureFond;
        private Texture2D _textureRetour;

        //retour
        private string _retour;
        private SpriteFont _policeRetour;
        private Vector2 _positionRetour;

        public SpriteBatch SpriteBatch { get; set; }

        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Regle(Game1 game) : base(game)
        {
            _myGame = game;
        }


        public override void Initialize()
        {
            //titre
            _titre = "Haunted Manor";
            _policeTitre = Content.Load<SpriteFont>("Titre");
            _positionTitre = new Vector2(265, 50);

            _finPartie = false;
            //GAME OVER 
            _gameOver = "GAME OVER";
            _positionGameOver = new Vector2((TAILLE_FENETRE_L / 2) - TAILLE_POLICE * _gameOver.Length / 2, TAILLE_FENETRE_H * 1 / 4);
            
            //REJOUER 
            _rejouer = "REJOUER";
            _positionRejouer = new Vector2((TAILLE_FENETRE_L * 1 / 4) - TAILLE_POLICE * _rejouer.Length / 2, TAILLE_FENETRE_H * 3 / 4);
            _rectRejouer = new Rectangle((int)_positionRejouer.X, (int)_positionRejouer.Y, TAILLE_POLICE * _rejouer.Length, TAILLE_POLICE * 2);
            /*  //QUITTER 
              _quitter = "QUITTER";
              _positionQuitter = new Vector2((TAILLE_FENETRE_L * 3 / 4) - TAILLE_POLICE * _quitter.Length / 2, TAILLE_FENETRE_H * 3 / 4);
              _rectQuitter = new Rectangle((int)_positionQuitter.X, (int)_positionQuitter.Y, TAILLE_POLICE * _quitter.Length, TAILLE_POLICE * 2);
              //SCORE FIN DE PARTIE
              _positionScorePartie = new Vector2((TAILLE_FENETRE_L / 2) - 145, TAILLE_FENETRE_H / 2);
  */
            //accueil
            _regle = "Le but est simple. Si tu veux pouvoir sortir d'ici, il ne faut pas mourir. \n" +
                "Ce manoir est maudit." +
                "Des monstres vont t'attaquer lorsque tu seras \n dedans. \n" +
                "Pour sortir il n y a qu'un seul moyen : \n" + 
                "Trouver toutes les clefs qui permettront d'ouvrir les portes. \n" +
                "\n Attention : Plus tu avances plus il y aura de clefs, mais aussi plus de \n monstres qui vont t'attaquer. \n" +
                "Je te souhaite bonne chance, il te faudra du courage pour sortir ici !";
            _policeRegle = Content.Load<SpriteFont>("regles");
            _positionRegle = new Vector2(50, 200);

            //Retour
            _retour = "Retour ";
            _policeRetour = Content.Load<SpriteFont>("Font");
            _positionRetour = new Vector2(115, 630);


        }

        public override void LoadContent()
        {
            _textureFond = Content.Load<Texture2D>("accueil");
            _textureRetour = Content.Load<Texture2D>("retour");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            _mouseState = Mouse.GetState();

            _finPartie = true;

            if (_mouseState.LeftButton == ButtonState.Pressed && _rectRejouer.Contains(_mouseState.Position))
            {
                Initialize();
            }
        //    else if (_mouseState.LeftButton == ButtonState.Pressed && _rectQuitter.Contains(_mouseState.Position))
            {
                //_screenManager.LoadScreen(_commandesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));

            }


        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Red); // on utilise la reference vers
                                                     // Game1 pour changer le graphisme

            // TODO: Add your drawing code here

            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            _myGame.SpriteBatch.Draw(_textureRetour, new Rectangle(50, 630, 50, 45), Color.White);
            _myGame.SpriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _myGame.SpriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
            _myGame.SpriteBatch.DrawString(_policeRetour, $"{_retour}", _positionRetour, Color.White);
            _myGame.SpriteBatch.End();
            /*
             if (_finPartie)
             {
                 GraphicsDevice.Clear(Color.Red * 0.7f);

                 _spriteBatch.DrawString(_police, _gameOver, _positionGameOver, Color.White);
                 _spriteBatch.DrawString(_police, _rejouer, _positionRejouer, Color.White);
                 _spriteBatch.DrawString(_police, _quitter, _positionQuitter, Color.White);
                 _spriteBatch.DrawString(_police, $"Score de la partie :{_score}", _positionScorePartie, Color.White);


             } 

            _spriteBatch.End(); */
        }
    }
}
