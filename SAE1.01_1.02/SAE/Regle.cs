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


        //titre
        private string _titre;
        private SpriteFont _policeTitre;
        private Vector2 _positionTitre;

        //Regle
        private string _regle;
        private SpriteFont _policeRegle;
        private Vector2 _positionRegle;

        //texture
        private Texture2D _textureFond;
        private Texture2D _textureRetour;

        //retour
        private string _retour;
        private SpriteFont _policeRetour;
        private Vector2 _positionRetour;

        public SpriteBatch SpriteBatch { get; set; }
        private SpriteBatch _spriteBatch;
        private MouseState _mouseState;

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
           
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Red); 

            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureFond, new Rectangle(0, 0, 1000, 700), Color.White);
            _myGame.SpriteBatch.Draw(_textureRetour, new Rectangle(50, 630, 50, 45), Color.White);
            _myGame.SpriteBatch.DrawString(_policeTitre, $"{_titre}", _positionTitre, Color.White);
            _myGame.SpriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
            _myGame.SpriteBatch.DrawString(_policeRetour, $"{_retour}", _positionRetour, Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}
