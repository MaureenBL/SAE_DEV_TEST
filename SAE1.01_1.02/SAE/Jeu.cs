using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;


namespace SAE
{
    internal class Jeu : GameScreen
    {
        private Game1 _myGame;

        //Regle
        private string _rappel;
        private SpriteFont _policeRappel;
        private Vector2 _positionRappel;

        //titre
        private string _titre;
        private SpriteFont _policeTitre;
        private Vector2 _positionTitre;

        //texture
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

            _rappel = "Souviens toi, tu dois arriver au bout sans mourir. \n" +
                "Pour cela, c'est facile, il faut que trouver 5 clefs qui sont un peu partout \n" +
                "Je te rappel que des monstres t'attaqueront pour ne pas que tu gagnes \n" +
                "\n Rappel des commandes : pour bouger utilise les fleches directionnelles." +
                "\nIl te faut 5 clefs ! Bon courage ! Je crois en toi et surtout ne meurs pas ! \n" +
                "\n Pour commencer clique sur : ";
            _policeRappel = Content.Load<SpriteFont>("regles");
            _positionRappel = new Vector2(50, 250);
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
            _myGame.GraphicsDevice.Clear(Color.Yellow); 

            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            _myGame.SpriteBatch.Draw(_texturePlay, new Rectangle(410, 500, 50, 50), Color.White);
            _myGame.SpriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _myGame.SpriteBatch.DrawString(_policeRappel, $"{_rappel}", _positionRappel, Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}
