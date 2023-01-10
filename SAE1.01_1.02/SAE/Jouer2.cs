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
        private Vector2 _ghostPosition;
        private Vector2 _skeletonPosition;
        //orientation
        private int _batOrientationX;
        private int _batOrientationY;
        private int _ghostOrientationX;
        private int _ghostOrientationY;
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
        private int _batVitesse;
        private int _ghostVitesse;
        private int _skeletonVitesse;
        public const int VITESSE_PERSO = 100;
        //zone
        public Vector2[] _ghostZone;
        public Rectangle[] _batZone;
        
        //commportement
        private bool _ghostAttaque;
        private bool espaceEtat;

        //Score
        private int _score;
        private SpriteFont _police;
        private Vector2 _positionScore;
        //Regle
        private string _regle;
        private SpriteFont _policeRegle;
        private Vector2 _positionRegle;


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


        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Jouer2(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {
            _cle = true;

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
            _regle = " Bravo tu as toutes les clefs ! \n Clique sur : ";
            _policeRegle = Content.Load<SpriteFont>("regles");
            _positionRegle = new Vector2(350, 250);

            //Cle
            _rectCle = new Rectangle[5];
            _rectCle = new Rectangle[] {new Rectangle(45, 670, 25, 25),new Rectangle(770, 570, 25, 25),new Rectangle(380, 120, 25, 25),new Rectangle(660, 245, 25, 25),new Rectangle(950, 20, 25, 25) };

            
            //Fausse Cle;


            //Perso
            _sensPersoHorizontal = 0;
            _sensPersoVertical = 0;
            _vitessePerso = VITESSE_PERSO;


            //Vie
            _vie = 3;
            _policeVie = Content.Load<SpriteFont>("Font");
            _positionVie = new Vector2(15, 50);
            _positionPerso = new Vector2(400, TAILLE_FENETRE_H - 2 * HAUTEUR_PERSO);


            // TODO: Add your initialization logic here
            //vitesse des monstres
            _batVitesse = 50;
            _ghostVitesse = 0;
            _skeletonVitesse = 25;
            //FENETRE
           /* _graphics.PreferredBackBufferWidth = TAILLE_FENETRE_L;
            _graphics.PreferredBackBufferHeight = TAILLE_FENETRE_H;
            _graphics.ApplyChanges();*/
            //camera
            /*var viewportadapter = new BoxingViewportAdapter(Game.Window, GraphicsDevice, 800, 480);
            _camera = new OrthographicCamera(viewportadapter);*/

            _ghostAttaque = false;
            _positionPerso = new Vector2(420, 670);
            _ghostPosition = new Vector2(-GHOST_LARGEUR, -GHOST_HAUTEUR);
            _batPosition = new Vector2[3];
            _batZone = new Rectangle[3];
            _batPosition[0] = new Vector2(175, 575);
            _batPosition[1] = new Vector2(530, 100);
            _batPosition[2] = new Vector2(950, 520);

            for(int i=0; i<_batPosition.Length; i++)
            {
                _batZone[i] = new Rectangle((int)(_batPosition[i].X - 25), (int)(_batPosition[i].Y - 25), 50, 50);
            }
            _positionPerso = new Vector2(400, 770);
            base.Initialize();
        }
        public override void LoadContent()
        {
            //TEXTURE CLE
            _myGame.SpriteBatch = new SpriteBatch(GraphicsDevice);
            _textureCle = Content.Load<Texture2D>("cle");

            //Chargement de la map
            _tiledMap = Content.Load<TiledMap>("map/mapGenerale");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //Couche collision de la map
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("murs");
            SpriteSheet persoTexture = Content.Load<SpriteSheet>("george.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(persoTexture);
            _textureRejouer = Content.Load<Texture2D>("G");
            _textureEsc = Content.Load<Texture2D>("esc");
            _textureFin = Content.Load<Texture2D>("F");

            SpriteSheet batTexture = Content.Load<SpriteSheet>("bat.sf", new JsonContentLoader());
            _bat = new AnimatedSprite(batTexture);
            /*SpriteSheet skeletonTexture = Content.Load<SpriteSheet>("Squelette.sf", new JsonContentLoader());
            _skeleton = new AnimatedSprite(skeletonTexture);*/
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
            if(_keyboardState.IsKeyDown(Keys.Up) || _keyboardState.IsKeyDown(Keys.Down) || _keyboardState.IsKeyDown(Keys.Left) || _keyboardState.IsKeyDown(Keys.Right))
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
            _sensPersoVertical = 0;
             _sensPersoHorizontal = 0;
            }*/
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


            //pour collision clé
            

            //COMPORTEMENT

            //Fantome
            //le fantome attaque
            if ((Keyboard.GetState().IsKeyDown(Keys.Add)) && _ghostAttaque==false)
                {
                    _ghostAttaque = true;
                   //effacer la zone dans le tableau
                }

                //Le héros se défend
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && espaceEtat == false && _ghostAttaque==true)
                {
                    espaceEtat = true;
                    _nbDebattage++;
                }

                if (!(_keyboardState.IsKeyDown(Keys.Space)))
                {
                    espaceEtat = false;
                }

                if(_nbDebattage>=10 && _ghostAttaque==true)
                {
                    _vitessePerso = VITESSE_PERSO;
                    float t1 = deltaTime;
                    _ghost.Play("fantomeMort");
                    _ghostAttaque = false;
                    _nbDebattage = 0;
                }

            //le fantome est en train d'attaquer
            if (_ghostAttaque==true)
                {
                    _ghost.Play("fantomeInvoque");
                    _ghostPosition = _positionPerso;
                    _vitessePerso = 0;
                }


                //Squelette
                /* if(VoirJoueur())
                 {
                      if(_skeletonPosition.X < _positionPerso.X)
                      {
                          _skeletonOrientationX = 1;
                      }
                      else
                      {
                          _skeletonOrientationX = 1;
                      }
                      if(_skeletonPosition.Y < _positionPerso.Y)
                      {
                          _skeletonOrientationY = 1;
                      }
                      else
                      {
                          _skeletonOrientationY = -1;
                      }
                  _vitesseSkeleton = 250;
                 }
                 else
                 {
                      _vitesseSkeleton = 100;
                      _skeletonOrientationY = 0;
                      _skeletonOrientationX = 1;
                    if(Collision avec un mur de gauche)
                        _skeletonOrientationX = 1;
                    if(Collision avec un bur de droite)
                        _skeletonOrientationX = -1;
                  }*/
            //Chauve-souris
            /*for(int i=0; i<_batZone.Length; i++)
            {
                if (CollisionJoueur(_batZone[i]))
                {
                    _bat.Play("batVolFace");
                    _bat.Update(gameTime);
                    _batVitesse = 250;
                    if (_batPosition[i].X < _positionPerso.X)
                    {
                        _batOrientationX = 1;
                        _batPosition[i].X += _batOrientationX * _batVitesse * deltaTime;
                    }
                    else
                    {
                        //_batOrientationX = 1;
                        //_batPosition[i].X += _batOrientationX * _batVitesse * deltaTime;
                    }

                    if (_batPosition[i].Y < _positionPerso.Y)
                    {
                        //_batOrientationY = 1;
                        //_batPosition[i].Y += _batOrientationY * _batVitesse * deltaTime;
                    }
                    else
                    {
                        //_batOrientationY = -1;
                        //_batPosition[i].Y += _batOrientationY * _batVitesse * deltaTime;
                    }
                }
                else
                {
                    _bat.Play("batVolDos");
                    _batVitesse = 0;
                    _batPosition[0] = new Vector2(175, 575);
                    _batPosition[1] = new Vector2(530, 100);
                    _batPosition[2] = new Vector2(950, 520);
                }
            }*/



            _skeletonPosition.X += _skeletonOrientationX * _skeletonVitesse * deltaTime;
                _skeletonPosition.Y += _skeletonOrientationY * _skeletonVitesse * deltaTime;

                //ANIMATION
                //Personnage
                /* //Squelette
                if (_skeletonVitesse != 0)
                {
                    _skeleton.Play("squeletteEnMarche");
                }
                else
                {
                    _skeleton.Play("squeletteEnPose");
                }
                if (CollisionJoueur((int)_skeletonPosition.X, (int)_skeletonPosition.Y, SKELETON_LARGEUR, SKELETON_HAUTEUR))
                {
                    _vie--;
                    _skeleton.Play("squeletteAttaque");
                }*/

            //Chauve-souris
            /* if (_batOrientationY == 1)
             {
                 _bat.Play("batVolFace");
             }
             else if (_batOrientationY == -1)
             {
                 _bat.Play("batVolDos");
             }
             else
             {
                 _bat.Play("batVolFace");
             }*/
            _tiledMapRenderer.Update(gameTime);
                _perso.Update(gameTime);
                _ghost.Update(gameTime);

            //Camera
            //  _camera.LookAt(_positionPerso);        

            //Camera
            /*_camera.LookAt(_positionPerso);
            //_cameraPosition = _positionPerso;
            const float movementSpeed = 200;
            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());

         //   _bat.Update(deltaTime);
           // _skeleton.Update(deltaTime);
            //_ghost.Update(deltaTime);
            _perso.Update(deltaTime);
            */
            
            
            
            //SCORE
            for (int i =0; i < _rectCle.Length; i++)
            {
                Rectangle rectJoueur = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, LARGEUR_PERSO, HAUTEUR_PERSO);
                if (rectJoueur.Intersects(_rectCle[i]))
                {
                    _cle = false;
                    _score += 1;                    
                }
            }

            //Vie
            if (CollisionJoueur()) // collision entre le joueur et les monstres
            {
                _vie -= 1;
            }

            if (_vie == 0)
                {
                    this.Initialize();
                }

                /*if (_score == 5)
                {
                    this.Initialize();
                }*/

            }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.DarkGoldenrod); // on utilise la reference vers Game1 pour changer le graphisme
            _tiledMapRenderer.Draw();
              /* _tiledMapRenderer.Draw(_camera.GetViewMatrix());
               var transformMatrix = _camera.GetViewMatrix();
               _myGame.SpriteBatch.Begin(transformMatrix: transformMatrix);
            _myGame.SpriteBatch.End();*/


            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_perso, _positionPerso);
            /* _myGame.SpriteBatch.Draw(_textureCle, _positionCle1, Color.White); // 1: piece violette - en bas a gauche
            _myGame.SpriteBatch.Draw(_textureCle, _positionCle2, Color.White); // 2: piece rouge - bas
            _myGame.SpriteBatch.Draw(_textureCle, _positionCle3, Color.White); // 3: piece bleu - milieu
            _myGame.SpriteBatch.Draw(_textureCle, _positionCle4, Color.White); // 4: piece verte - haut / angle
            _myGame.SpriteBatch.Draw(_textureCle, _positionCle5, Color.White); // 5: piece rouge - angle en haut à droite */
            _myGame.SpriteBatch.DrawString(_police, $"Score : {_score}", _positionScore, Color.White);
            _myGame.SpriteBatch.DrawString(_policeVie, $"Vies : {_vie}", _positionVie, Color.White);

            for(int i = 0; i<_rectCle.Length; i++)
            {
                if(_cle == true)
                {
                    _myGame.SpriteBatch.Draw(_textureCle, _rectCle[i], Color.White); 
                }
            }

            //Affichage clé 1
/*
            if(_cle == true)
            {
                _myGame.SpriteBatch.Draw(_textureCle, new Rectangle(45, 670, 25, 25), Color.White); // 1: piece violette - en bas a gauche
                _myGame.SpriteBatch.Draw(_textureCle, _positionCle2, Color.White); // 2: piece rouge - bas
                _myGame.SpriteBatch.Draw(_textureCle, _positionCle3, Color.White); // 3: piece bleu - milieu
                _myGame.SpriteBatch.Draw(_textureCle, _positionCle4, Color.White); // 4: piece verte - haut / angle
                _myGame.SpriteBatch.Draw(_textureCle, _positionCle5, Color.White); // 5: piece rouge - angle en haut à droite
            }
*/
            /*
            //Affichage clé 2
            if()
            {
                _score += 1;
                _myGame.SpriteBatch.DrawString(_police, $"Score : {_score}", _positionScore, Color.White);
            }
            else
            {
                _myGame.SpriteBatch.Draw(_textureCle, new Rectangle(770, 570, 25, 25), Color.White); // 2: piece rouge - bas
            }
            //Affichage clé 3
            if()
            {
                _score += 1;
                _myGame.SpriteBatch.DrawString(_police, $"Score : {_score}", _positionScore, Color.White);
            }
            else
            {
                _myGame.SpriteBatch.Draw(_textureCle, new Rectangle(380, 120, 25, 25), Color.White); // 3: piece bleu - milieu
            }
            //Affichage clé 4
            if()
            {
                _score += 1;
                _myGame.SpriteBatch.DrawString(_police, $"Score : {_score}", _positionScore, Color.White);
            }
            else
            {
                _myGame.SpriteBatch.Draw(_textureCle, new Rectangle(660, 245, 25, 25), Color.White); // 4: piece verte - haut / angle
            }
            //Affichage clé 5
            if()
            {
                _score += 1;
                _myGame.SpriteBatch.DrawString(_police, $"Score : {_score}", _positionScore, Color.White);
            }
            else
            {
                _myGame.SpriteBatch.Draw(_textureCle, new Rectangle(950, 20, 25, 25), Color.White); // 5: piece rouge - angle en haut à droite
            }*/


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
            if(_score == 5)
            {
                _myGame.SpriteBatch.DrawString(_policeRegle, $"{_regle}", _positionRegle, Color.White);
                _myGame.SpriteBatch.Draw(_textureFin, new Rectangle(510, 285, 50, 50), Color.White);
            }

            //_myGame.SpriteBatch.Draw(_skeleton, _skeletonPosition);
            for(int i=0; i<_batPosition.Length; i++)
            {
            _myGame.SpriteBatch.Draw(_bat, _batPosition[i]);
            }
            _myGame.SpriteBatch.Draw(_ghost, _ghostPosition);
            _myGame.SpriteBatch.End();
        }
       public bool CollisionJoueur(Rectangle objet)
        {
            Rectangle rectJoueur = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, LARGEUR_PERSO, HAUTEUR_PERSO);
            return rectJoueur.Intersects(objet);
        }

        public bool CollisionJoueur()
        {

            Rectangle rectJoueur = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, LARGEUR_PERSO, HAUTEUR_PERSO);
            //Rectangle rectangleBat = new Rectangle((int)_batPosition.X, (int)_batPosition.Y, BAT_LARGEUR, BAT_HAUTEUR);            
            Rectangle rectangleSkeleton = new Rectangle((int)_skeletonPosition.X, (int)_skeletonPosition.Y, SKELETON_LARGEUR, SKELETON_HAUTEUR);
            return rectJoueur.Intersects(rectangleBat) || rectJoueur.Intersects(rectangleSkeleton);
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
    

    }

    //méthode détection de collision avec la map
    /*private bool IsCollision(ushort x, ushort y)
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
    }*/


    //méthode détection de collision avec la map
    /*  private bool IsCollision(ushort x, ushort y)
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
      }*/
}

