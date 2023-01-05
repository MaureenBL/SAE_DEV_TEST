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

        //QUITTER
        private string _quitter;
        private SpriteFont _policeQuitter;
        private Vector2 _positionQuitter;
        public const int TAILLE_QUITTER = 100;

        //acces
        private MouseState _mouseState;
        private KeyboardState _keyboardState;

        //transition
        private readonly ScreenManager _screenManager;
        private Game1 _game1;
        private Regle _reglesTrans;
        public SpriteBatch SpriteBatch { get; set; }


        private GraphicsDeviceManager _graphics;

        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Commande(Game1 game) : base(game)
        {
            _myGame = game;
        }


        public override void Initialize()
        {
            
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Initialize();
               // _screenManager.LoadScreen(_reglesTrans, new FadeTransition(GraphicsDevice, Color.LightGoldenrodYellow)); // NE MARCHE PAS 
            }
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Blue); // on utilise la reference vers Game1 pour changer le graphisme
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.DrawString(_policeQuitter, $"{_quitter}", _positionQuitter, Color.White); // NE MARCHE PAS 
            _myGame.SpriteBatch.End();
        }
    }
}
