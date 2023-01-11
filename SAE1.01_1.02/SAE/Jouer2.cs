using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace SAE
{
    internal class Jouer2 : GameScreen
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //TAILLE FENETRE
        public const int TAILLE_FENETRE_L = 1120;
        public const int TAILLE_FENETRE_H = 800;
        //MAP
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        //Collisions map
        private TiledMapTileLayer mapLayer;
        //acces
        private KeyboardState _keyboardState;
        //camera
        private OrthographicCamera _camera;


        //PERSONNAGE - GEORGE
        private AnimatedSprite _perso;
        private Vector2 _positionPerso;
        private int _sensPersoHorizontal;
        private int _sensPersoVertical;
        private int _vitessePerso;
        private int _nbVie;
        private int _nbDebattage;
        public const int LARGEUR_PERSO = 14;
        public const int HAUTEUR_PERSO = 14;
        //MONSTRES
        //animation
        private AnimatedSprite _bat;
        private AnimatedSprite _ghost;
        private AnimatedSprite _skeleton;

        //position
        private Vector2[] _batPosition;
        private Vector2[] _batPositionIni;
        private Vector2 _ghostPosition;
        private Vector2 _skeletonPosition;
        //orientation
        private int[] _batOrientationX;
        private int[] _batOrientationY;
        private int _skeletonOrientationX;
        private int _skeletonOrientationY;
        //dimentions
        public const int BAT_LARGEUR = 48;
        public const int BAT_HAUTEUR = 64;
        public const int GHOST_LARGEUR = 64;
        public const int GHOST_HAUTEUR = 64;
        public const int SKELETON_LARGEUR = 64;
        public const int SKELETON_HAUTEUR = 64;
        //vitesse
        private int[] _batVitesse;
        private int _ghostVitesse;
        private int _skeletonVitesse;
        public const int VITESSE_PERSO = 100;
        //zone
        public Rectangle _skeletonZone;
        public Rectangle[] _batZone;

        //commportement
        private bool _ghostAttaque;
        private bool[] _batAttaque;
        private bool _skeletonAttaque;
        private bool espaceEtat;

        //Score
        private int _score;
        private SpriteFont _police;
        private Vector2 _positionScore;
        //Regle
        private string _end;
        private SpriteFont _policeEnd;
        private Vector2 _positionEnd;


        //Vie
        private int _vie;
        private SpriteFont _policeVie;
        private Vector2 _positionVie;

        //CLE
        private Texture2D _textureCle;
        private Rectangle[] _rectCle;
        private bool _cle;

        //Game Over
        private string _gameOver;
        private SpriteFont _policeGameOver;
        private Vector2 _positionGameOver;
        //retour
        private string _rejouer;
        private SpriteFont _policeRejouer;
        private Vector2 _positionRejouer;
        //quitter
        private string _quitter;
        private SpriteFont _policeQuitter;
        private Vector2 _positionQuitter;

        private Texture2D _textureRejouer;
        private Texture2D _textureEsc;
        private Texture2D _textureFin;

        private SoundEffect _sound;

        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Jouer2(Game1 game) : base(game)
        {

            _myGame = game;
        }

        public override void Initialize()
        {

            //FENETRE
            /*  _graphics.PreferredBackBufferWidth = TAILLE_FENETRE_L;
              _graphics.PreferredBackBufferHeight = TAILLE_FENETRE_H;
              _graphics.ApplyChanges();*/



            //game over
            _gameOver = "Game Over";
            _policeGameOver = Content.Load<SpriteFont>("Titre");
            _positionGameOver = new Vector2(350, 210);
            //Retour
            _rejouer = "Rejouer ";
            _policeRejouer = Content.Load<SpriteFont>("End");
            _positionRejouer = new Vector2(210, 420);
            //Quitter
            _quitter = "Quitter ";
            _policeQuitter = Content.Load<SpriteFont>("End");
            _positionQuitter = new Vector2(610, 420);

            //Score
            _score = 0;
            _police = Content.Load<SpriteFont>("Font");
            _positionScore = new Vector2(15, 10);
            _end = " Bravo tu as toutes les clefs ! \n Clique sur : ";
            _policeEnd = Content.Load<SpriteFont>("regles");
            _positionEnd = new Vector2(350, 250);

            //Cle
            _cle = true;
            _rectCle = new Rectangle[5];
            _rectCle = new Rectangle[]
            {
                new Rectangle(45, 670, 25, 25),
                new Rectangle(770, 570, 25, 25),
                new Rectangle(380, 120, 25, 25),
                new Rectangle(660, 245, 25, 25),
                new Rectangle(1050, 20, 25, 25),
                new Rectangle(755, 400, 25, 25)
            };


            //Perso
            _sensPersoHorizontal = 0;
            _sensPersoVertical = 0;
            _vitessePerso = VITESSE_PERSO;


            //Vie
            _vie = 3;
            _policeVie = Content.Load<SpriteFont>("Font");
            _positionVie = new Vector2(15, 50);
            _positionPerso = new Vector2(400, TAILLE_FENETRE_H - 2 * HAUTEUR_PERSO);


            //vitesse des monstres
            _ghostVitesse = 0;
            _skeletonVitesse = 25;

           /* _graphics.PreferredBackBufferWidth = TAILLE_FENETRE_L;
            _graphics.PreferredBackBufferHeight = TAILLE_FENETRE_H;
            _graphics.ApplyChanges();*/
            //camera
            var viewportadapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 800, 550);
            _camera = new OrthographicCamera(viewportadapter);


            _positionPerso = new Vector2(420, 670);
            _skeletonPosition = new Vector2(600, 400);
            _ghostPosition = new Vector2(-GHOST_LARGEUR, -GHOST_HAUTEUR);
            _batPosition = new Vector2[3];
            _batPositionIni = new Vector2[3];
            _batZone = new Rectangle[3];
            _batOrientationX = new int[3];
            _batOrientationY = new int[3];
            _batVitesse = new int[3];
            _batAttaque = new bool[3];
            _batPosition[0] = new Vector2(175, 575);
            _batPosition[1] = new Vector2(390, 130);
            _batPosition[2] = new Vector2(950, 520);
            for (int i = 0; i < _batPosition.Length; i++)
            {
                _batVitesse[i] = 50;
                _batAttaque[i] = false;
                _batPositionIni[i] = _batPosition[i];
                _batZone[i] = new Rectangle((int)(_batPositionIni[i].X + (BAT_LARGEUR / 2) - 100), (int)(_batPositionIni[i].Y - (BAT_HAUTEUR / 2) - 25), 200, 200);
            }
            _ghostAttaque = false;
            _skeletonVitesse = 50;

            _positionPerso = new Vector2(400, 770);
            base.Initialize();
        }
        public override void LoadContent()
        {
            //TEXTURE CLE
            _myGame.SpriteBatch = new SpriteBatch(GraphicsDevice);
            _textureCle = Content.Load<Texture2D>("cle");

            //SoundEffect
            _sound = Content.Load<SoundEffect>("keySound");

            //Chargement de la map
            _tiledMap = Content.Load<TiledMap>("map/mapGenerale");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //Couche collision de la map
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("murs");
            SpriteSheet persoTexture = Content.Load<SpriteSheet>("george.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(persoTexture);
            _textureRejouer = Content.Load<Texture2D>("G");
            SpriteSheet batTexture = Content.Load<SpriteSheet>("bat.sf", new JsonContentLoader());
            _bat = new AnimatedSprite(batTexture);
            SpriteSheet skeletonTexture = Content.Load<SpriteSheet>("Squelette.sf", new JsonContentLoader());
            _skeleton = new AnimatedSprite(skeletonTexture);
            SpriteSheet ghostTexture = Content.Load<SpriteSheet>("Fantome.sf", new JsonContentLoader());
            _ghost = new AnimatedSprite(ghostTexture);

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * _vitessePerso; // Vitesse de déplacement du sprite
            _keyboardState = Keyboard.GetState();


            //Déplacement et Animation + Collision

            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Up) || _keyboardState.IsKeyDown(Keys.Down) || _keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.Right))
            {
                if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
                {
                    _perso.Play("gDroite");
                    //  _sensPersoHorizontal = 1;
                    ushort tx1 = (ushort)((_positionPerso.X + LARGEUR_PERSO) / _tiledMap.TileWidth);
                    ushort ty1 = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                    ushort tx2 = (ushort)((_positionPerso.X + LARGEUR_PERSO) / _tiledMap.TileWidth);
                    ushort ty2 = (ushort)((_positionPerso.Y + (HAUTEUR_PERSO / 2)) / _tiledMap.TileHeight);
                    ushort tx3 = (ushort)((_positionPerso.X + LARGEUR_PERSO) / _tiledMap.TileWidth);
                    ushort ty3 = (ushort)((_positionPerso.Y + HAUTEUR_PERSO) / _tiledMap.TileHeight);
                    //animation = "walkNorth";
                    if (!IsCollision(tx1, ty1) && !IsCollision(tx2, ty2) && !IsCollision(tx3, ty3))
                        _positionPerso.X += walkSpeed;

                }
                else if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))//flèche gauche
                {
                    _perso.Play("gGauche");
                    //   _sensPersoHorizontal = -1;
                    ushort tx1 = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ushort ty1 = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                    ushort tx2 = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ushort ty2 = (ushort)((_positionPerso.Y + (HAUTEUR_PERSO / 2)) / _tiledMap.TileHeight);
                    ushort tx3 = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ushort ty3 = (ushort)((_positionPerso.Y + HAUTEUR_PERSO) / _tiledMap.TileHeight);
                    //animation = "walkNorth";
                    if (!IsCollision(tx1, ty1) && !IsCollision(tx2, ty2) && !IsCollision(tx3, ty3))
                        _positionPerso.X -= walkSpeed;

                }
                //flèche haut
                else if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
                {
                    _perso.Play("gHaut");
                    //  _sensPersoVertical = -1;
                    ushort tx1 = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ushort ty1 = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                    ushort tx2 = (ushort)((_positionPerso.X + LARGEUR_PERSO) / _tiledMap.TileWidth);
                    ushort ty2 = (ushort)(_positionPerso.Y / _tiledMap.TileHeight);
                    //animation = "walkNorth";
                    if (!IsCollision(tx1, ty1) && !IsCollision(tx2, ty2))
                        _positionPerso.Y -= walkSpeed;

                }
                //flèche bas
                else if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
                {
                    _perso.Play("gBas");
                    //  _sensPersoVertical = 1;
                    //  _sensPersoHorizontal = 0;
                    //  _sensPersoVertical = 1;

                    ushort tx1 = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                    ushort ty1 = (ushort)((_positionPerso.Y + HAUTEUR_PERSO) / _tiledMap.TileHeight);
                    ushort tx2 = (ushort)((_positionPerso.X + LARGEUR_PERSO) / _tiledMap.TileWidth);
                    ushort ty2 = (ushort)((_positionPerso.Y + HAUTEUR_PERSO) / _tiledMap.TileHeight);
                    //animation = "walkNorth";
                    if (!IsCollision(tx1, ty1) && !IsCollision(tx2, ty2))
                        _positionPerso.Y += walkSpeed;

                }
            }

            /*else
            {
            if (_sensPersoHorizontal==1)
                {
                    _perso.Play("gDroiteImo");
                }
            else if (_sensPersoHorizontal == -1)
                {
                    _perso.Play("gGaucheImo");
                }
            else if(_sensPersoVertical==1)
                {
                    _perso.Play("gBasImo");
                }
            else if (_sensPersoVertical == -1)
                {
                    _perso.Play("gHautImo");
                }
            /* if (_keyboardState.IsKeyDown(Keys.Up) || _keyboardState.IsKeyDown(Keys.Down) || _keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.Right))
             {
                 if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))//flèche gauche
                 {
                     _perso.Play("gGauche");
                     _sensPersoHorizontal = -1;
                     _sensPersoVertical = 0;
                 }
                 //flèche haut
                 else if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
                 {
                     _perso.Play("gHaut");
                     _sensPersoVertical = -1;
                     _sensPersoHorizontal = 0;
                 }
                 //flèche bas
                 else if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
                 {
                     _perso.Play("gBas");
                     _sensPersoVertical = 1;
                     _sensPersoHorizontal = 0;
                 }
                 else if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
                 {
                     _perso.Play("gDroite");
                     _sensPersoVertical = 0;
                     _sensPersoHorizontal = 1;
                 }
             }
             else
             {
                 if (_sensPersoHorizontal == 1)
                 {
                     _perso.Play("gDroiteImo");
                 }
                 else if (_sensPersoHorizontal == -1)
                 {
                     _perso.Play("gGaucheImo");
                 }
                 else if (_sensPersoVertical == 1)
                 {
                     _perso.Play("gBasImo");
                 }
                 else if (_sensPersoVertical == -1)
                 {
                     _perso.Play("gHautImo");
                 }
                 _sensPersoVertical = 0;
                 _sensPersoHorizontal = 0;
             }
                 _positionPerso.X += _sensPersoHorizontal * _vitessePerso * deltaTime;
                 _positionPerso.Y += _sensPersoVertical * _vitessePerso * deltaTime;*/
            
                _positionPerso.X += _sensPersoHorizontal * _vitessePerso * deltaTime;
                _positionPerso.Y += _sensPersoVertical * _vitessePerso * deltaTime;


            //pour collision clé

            //Fantome
            //le fantome attaque
            if ((Keyboard.GetState().IsKeyDown(Keys.Add)) && _ghostAttaque == false)
                //Fantome
                //le fantome attaque
                if ((Keyboard.GetState().IsKeyDown(Keys.Add)) && _ghostAttaque == false)
                {
                    _ghostAttaque = true;
                    //supprimer la clé
                }

            //Le héros se défend
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && espaceEtat == false && _ghostAttaque == true)
            {
                espaceEtat = true;
                _nbDebattage++;  
            }

            if (!(_keyboardState.IsKeyDown(Keys.Space)))
            {
                espaceEtat = false;
            }

            if (_nbDebattage >= 10 && _ghostAttaque == true)
            {
                _vitessePerso = VITESSE_PERSO;
                float t1 = deltaTime;
                _ghost.Play("fantomeMort");
                _ghostAttaque = false;
                _nbDebattage = 0;
            }

            //le fantome est en train d'attaquer
            if (_ghostAttaque == true)
            {
                _ghost.Play("fantomeInvoque");
                _ghostPosition = _positionPerso;
                _vitessePerso = 0;
            }

            //Chauve-souris
            for (int i = 0; i < _batZone.Length; i++)
            {
                if (CollisionJoueur(_batZone[i]))
                {
                    _batAttaque[i] = true;
                    _bat.Play("batVolFace");
                }
                else
                {
                    _batAttaque[i] = false;
                }
                if (_batAttaque[i] == true)
                {
                    if (_batPosition[i].X < _positionPerso.X)
                    {
                        _batOrientationX[i] = 1;
                    }
                    else if (_batPosition[i].X > _positionPerso.X)
                    {
                        _batOrientationX[i] = -1;
                    }
                    else
                    {
                        _batOrientationX[i] = 0;
                    }
                    if (_batPosition[i].Y < _positionPerso.Y)
                    {
                        _batOrientationY[i] = 1;
                    }
                    else if (_batPosition[i].Y > _positionPerso.Y)
                    {
                        _batOrientationY[i] = -1;
                    }
                    else
                    {
                        _batOrientationY[i] = 0;
                    }
                }
                else
                {
                    if (_batPosition[i].X < _batPositionIni[i].X)
                    {
                        _batOrientationX[i] = 1;
                    }
                    else if (_batPosition[i].X > _batPositionIni[i].X)
                    {
                        _batOrientationX[i] = -1;
                    }
                    else
                    {
                        _batOrientationX[i] = 0;
                    }
                    if (_batPosition[i].Y < _batPositionIni[i].Y)
                    {
                        _batOrientationY[i] = 1;
                    }
                    else if (_batPosition[i].Y > _batPositionIni[i].Y)
                    {
                        _batOrientationY[i] = -1;
                    }
                    else
                    {
                        _batOrientationY[i] = 0;
                    }
                }
                _batPosition[i] += new Vector2((int)_batOrientationX[i] * _batVitesse[i] * deltaTime, (int)_batOrientationY[i] * _batVitesse[i] * deltaTime);
            }


            //Squelette
            _skeletonZone = new Rectangle((int)(_skeletonPosition.X - (SKELETON_LARGEUR / 2)), (int)(_skeletonPosition.Y - (SKELETON_HAUTEUR / 2)), 150, 150);
            if (CollisionJoueur(_skeletonZone))
            {
                _skeletonAttaque = true;
                _skeleton.Play("squeletteEnMarche");
            }
            else
            {
                _skeletonAttaque = false;
                _skeleton.Play("squeletteEnPose");
            }
            if (_skeletonAttaque == true)
            {
                if (_skeletonPosition.X < _positionPerso.X)
                {
                    _skeletonOrientationX = 1;

                }
                else if (_skeletonPosition.X > _positionPerso.X)
                {
                    _skeletonOrientationX = -1;
                }
                else
                {
                    _skeletonOrientationX = 0;
                }
                if (_skeletonPosition.Y < _positionPerso.Y)
                {
                    _skeletonOrientationY = 1;
                }
                else if (_skeletonPosition.Y > _positionPerso.Y)
                {
                    _skeletonOrientationY = -1;
                }
                else
                {
                    _skeletonOrientationY = 0;
                }
            }
            else
            {
                _skeletonOrientationX = 0;
                _skeletonOrientationY = 0;
            }
            _skeletonPosition += new Vector2(_skeletonOrientationX * _skeletonVitesse * deltaTime, _skeletonOrientationY * _skeletonVitesse * deltaTime);
            _ghost.Update(gameTime);
            _bat.Update(gameTime);
            _skeleton.Update(deltaTime);
            _tiledMapRenderer.Update(gameTime);
            _perso.Update(gameTime);
            _ghost.Update(gameTime);

            //Camera
            _camera.LookAt(_positionPerso);

            //Camera
            /*_camera.LookAt(_positionPerso);
            //_cameraPosition = _positionPerso;
            const float movementSpeed = 200;
            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());

            */ 



            //SCORE
            for (int i = 0; i < _rectCle.Length; i++)
            {
                if (CollisionJoueur(_rectCle[i]))
                {
                    _rectCle[i] = new Rectangle(0, 0, 0, 0);
                    _sound.Play();
                    _score += 1;
                }

            }
        }
    

         
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black); // on utilise la reference vers Game1 pour changer le graphisme
                                                       //_tiledMapRenderer.Draw();
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            var transformMatrix = _camera.GetViewMatrix();
            _myGame.SpriteBatch.Begin(transformMatrix: transformMatrix);
            _myGame.SpriteBatch.Draw(_perso, _positionPerso);
            for (int i = 0; i < _rectCle.Length; i++)
            {
                if (_cle == true)
                {
                    _myGame.SpriteBatch.Draw(_textureCle, _rectCle[i], Color.White);
                }
            }
            for (int i = 0; i < _batPosition.Length; i++)
            {
                _myGame.SpriteBatch.Draw(_skeleton, _skeletonPosition);
                _myGame.SpriteBatch.Draw(_bat, _batPosition[i]);
            }
            _myGame.SpriteBatch.Draw(_ghost, _ghostPosition);
            _myGame.SpriteBatch.End();


            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.DrawString(_police, $"Score : {_score}", _positionScore, Color.White);
            _myGame.SpriteBatch.DrawString(_policeVie, $"Vies : {_vie}", _positionVie, Color.White);

            //Affichage vie
            if (_vie == 0)
            {
                _myGame.SpriteBatch.DrawString(_policeGameOver, $"{_gameOver}", _positionGameOver, Color.White);
                _myGame.SpriteBatch.DrawString(_policeRejouer, $"{_rejouer}", _positionRejouer, Color.White);
                _myGame.SpriteBatch.DrawString(_policeQuitter, $"{_quitter}", _positionQuitter, Color.White);

                _myGame.SpriteBatch.Draw(_textureRejouer, new Rectangle(350, 420, 50, 45), Color.White);
                _myGame.SpriteBatch.Draw(_textureEsc, new Rectangle(750, 420, 50, 50), Color.White);
            }

            //affichage score
            if (_score == 5)
            {
                _myGame.SpriteBatch.DrawString(_policeEnd, $"{_end}", _positionEnd, Color.White);
                _myGame.SpriteBatch.Draw(_textureFin, new Rectangle(510, 285, 50, 50), Color.White);
            }
            _myGame.SpriteBatch.End();


        }
         public bool CollisionJoueur(Rectangle objet)
         {
             Rectangle rectJoueur = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, LARGEUR_PERSO, HAUTEUR_PERSO);

             return rectJoueur.Intersects(objet);
         }

            /*public bool CollisionCle()
            {
                Rectangle rectJoueur = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, LARGEUR_PERSO, HAUTEUR_PERSO);
                for(int i = 0; i<_rectCle.Length; i++)
                {
                    Rectangle rectCle = _rectCle[i];

                }
                return rectJoueur.Intersects(rectCle[i]);
            }*/
            //méthode détection de collision avec la map
            private bool IsCollision(ushort x, ushort y)
            {

                TiledMapTile? tile;
                if (mapLayer.TryGetTile(x, y, out tile) == false)
                {
                    return false;
                }
                if (!tile.Value.IsBlank)
                {


                    return true;
                }
                return false;
            }
    }
}

