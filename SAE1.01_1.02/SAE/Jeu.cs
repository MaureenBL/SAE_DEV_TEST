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
    internal class Jeu : GameScreen
    {
        private Game1 _myGame;

        //Regle
        private string _regle;
        private SpriteFont _policeRegle;
        private Vector2 _positionRegle;


        //titre
        private string _titre;
        private SpriteFont _policeTitre;
        private Vector2 _positionTitre;

        private Texture2D _textureFond;
        private Texture2D _texturePlay;
        public SpriteBatch SpriteBatch { get; set; }


        public Jeu(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {

            //titre
            _titre = "Haunted Manor";
            _policeTitre = Content.Load<SpriteFont>("Titre");
            _positionTitre = new Vector2(265, 100);

            _regle = "Souviens toi, tu dois arriver au bout sans mourir. \n" +
                "Pour cela, c'est facile, il faut que trouver 5 clefs qui sont un peu partout \n" +
                "Je te rappel que des monstres t'attaqueront pour ne pas que tu gagnes \n" +
                "\n Rappel des commandes : pour bouger utilise les fleches directionnelles." +
                "\nIl te faut 5 clefs ! Bon courage ! Je crois en toi et surtout ne meurs pas ! \n" +
                "\n Pour commencer clique sur : ";
            _policeRegle = Content.Load<SpriteFont>("regles");
            _positionRegle = new Vector2(50, 250);
            base.Initialize();
        }
        
        public override void LoadContent()
        {
            _myGame.SpriteBatch = new SpriteBatch(GraphicsDevice);
            _textureFond = Content.Load<Texture2D>("accueil"); 
            _texturePlay = Content.Load<Texture2D>("P");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        { }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Yellow); // on utilise la reference vers Game1 pour changer le graphisme

            _myGame.SpriteBatch.Begin();

            _myGame.SpriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            _myGame.SpriteBatch.Draw(_texturePlay, new Rectangle(410, 500, 50, 50), Color.White);
            _myGame.SpriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _myGame.SpriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}
