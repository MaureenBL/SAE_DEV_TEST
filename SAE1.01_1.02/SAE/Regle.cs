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
        private string _quitter;
        private Vector2 _positionQuitter;
        private Rectangle _rectQuitter;
        //Score
        private Vector2 _positionScorePartie;

        private SpriteFont _police;



        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Regle(Game1 game) : base(game)
        {
            _myGame = game;
        }


        public override void Initialize()
        {

            _finPartie = false;
            //GAME OVER 
            _gameOver = "GAME OVER";
            _positionGameOver = new Vector2((TAILLE_FENETRE_L / 2) - TAILLE_POLICE * _gameOver.Length / 2, TAILLE_FENETRE_H * 1 / 4);
            
            //REJOUER 
            _rejouer = "REJOUER";
            _positionRejouer = new Vector2((TAILLE_FENETRE_L * 1 / 4) - TAILLE_POLICE * _rejouer.Length / 2, TAILLE_FENETRE_H * 3 / 4);
            _rectRejouer = new Rectangle((int)_positionRejouer.X, (int)_positionRejouer.Y, TAILLE_POLICE * _rejouer.Length, TAILLE_POLICE * 2);
            //QUITTER 
            _quitter = "QUITTER";
            _positionQuitter = new Vector2((TAILLE_FENETRE_L * 3 / 4) - TAILLE_POLICE * _quitter.Length / 2, TAILLE_FENETRE_H * 3 / 4);
            _rectQuitter = new Rectangle((int)_positionQuitter.X, (int)_positionQuitter.Y, TAILLE_POLICE * _quitter.Length, TAILLE_POLICE * 2);
            //SCORE FIN DE PARTIE
            _positionScorePartie = new Vector2((TAILLE_FENETRE_L / 2) - 145, TAILLE_FENETRE_H / 2);



            base.Initialize();
        }

        public override void LoadContent()
        {
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
            else if (_mouseState.LeftButton == ButtonState.Pressed && _rectQuitter.Contains(_mouseState.Position))
            {
                //_screenManager.LoadScreen(_commandesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow));

            }


        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Red); // on utilise la reference vers
                                                     // Game1 pour changer le graphisme
            _spriteBatch.Begin();

            if (_finPartie)
            {
                GraphicsDevice.Clear(Color.Red * 0.7f);

                _spriteBatch.DrawString(_police, _gameOver, _positionGameOver, Color.White);
                _spriteBatch.DrawString(_police, _rejouer, _positionRejouer, Color.White);
                _spriteBatch.DrawString(_police, _quitter, _positionQuitter, Color.White);
               // _spriteBatch.DrawString(_police, $"Score de la partie :{_score}", _positionScorePartie, Color.White);


            }

            _spriteBatch.End();
        }
    }
}
