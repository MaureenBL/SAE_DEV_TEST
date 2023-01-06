﻿using System;
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




namespace SAE
{
    internal class Jouer2 : GameScreen
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //TAILLE FENETRE
        public const int TAILLE_FENETRE_L = 1600;
        public const int TAILLE_FENETRE_H = 900;
        //MAP
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        //Collisions map
        private TiledMapTileLayer mapLayer;
        //acces
        private KeyboardState _keyboardState;
        //camera
        private OrthographicCamera _camera;
        private Vector2 _cameraPosition;
        private Vector2 _cameraOrigin;

        //PERSONNAGE - GEORGE
        private AnimatedSprite _perso;
        private Vector2 _positionPerso;
        private Vector2 _sensPersoHorizontal;
        private Vector2 _sensPersoVertical;
        private int _vitessePerso;
        private int _nbVie;
        private int _nbDebattage;
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
        private int _vitesseBat;
        private int _vitesseGhost;
        private int _vitesseSkeleton;
        //zone
        public int[,] _zoneFantome;

        private Game1 _myGame;
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
        // défini dans Game1
        public Jouer2(Game1 game) : base(game)
        {
            _myGame = game;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            //vitesse des monstres
            _vitesseBat = 0;
            _vitesseGhost = 0;
            _vitesseSkeleton = 100;
            _nbVie = 3;

            //FENETRE
            _graphics.PreferredBackBufferWidth = TAILLE_FENETRE_L;
            _graphics.PreferredBackBufferHeight = TAILLE_FENETRE_H;
            _graphics.ApplyChanges();
            //camera
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            _camera = new OrthographicCamera(viewportadapter);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _myGame.SpriteBatch = new SpriteBatch(GraphicsDevice);
            SpriteSheet persoTexture = Content.Load<SpriteSheet>("george.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(persoTexture);
            SpriteSheet batTexture = Content.Load<SpriteSheet>("bat.sf", new JsonContentLoader());
            _bat = new AnimatedSprite(batTexture);
            SpriteSheet skeletonTexture = Content.Load<SpriteSheet>("Squelette.sf", new JsonContentLoader());
            _skeleton = new AnimatedSprite(skeletonTexture);
            SpriteSheet ghostTexture = Content.Load<SpriteSheet>("Fantome.sf", new JsonContentLoader());
            _ghost = new AnimatedSprite(ghostTexture);
            //Chargement de la map
            _tiledMap = Content.Load<TiledMap>("map/mapGenerale");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            //couche collision de la map
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("murs");


            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();

            // TODO: Add your update logic here
            
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            /*if(CollisionJoueur((int)_skeletonPosition.X, (int)_skeletonPosition.Y, SKELETON_LARGEUR, SKELETON_HAUTEUR) && Keyboard.GetState().IsKeyDown(Keys.Space))
             {
                 _nbDebattage += 1;
             }*/

            //SQUELETTE
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
                  //attendre 5 secondes
                  _skeletonOrientationX = -1;
                  //attendre 5 secondes
              }*/

            //FANTOME
            do
            {
                _ghost.Play("fantomeInvoque");
                _ghostPosition = _positionPerso;
            }
            while (/*CollisionJoueur(avec la zone) &&*/_nbDebattage < 25);


            _skeletonPosition.X += _skeletonOrientationX * _vitesseSkeleton * deltaTime;
            _skeletonPosition.Y += _skeletonOrientationY * _vitesseSkeleton * deltaTime;
            //ANIMATION
            //Personnage
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                _perso.Play("gBas");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                _perso.Play("gHaut");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                _perso.Play("gDroite");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                _perso.Play("gGauche");
            }
            //Squelette
            if (_vitesseSkeleton != 0)
            {
                _skeleton.Play("squeletteEnMarche");
            }
            else
            {
                _skeleton.Play("squeletteEnPose");
            }
            if (CollisionJoueur((int)_skeletonPosition.X, (int)_skeletonPosition.Y, SKELETON_LARGEUR, SKELETON_HAUTEUR))
            {
                _nbVie--;
                _skeleton.Play("squeletteAttaque");
            }


            //Chauve-souris
            if (_batOrientationY == 1)
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
            }
            //Fantome
            if (_ghostOrientationX != 0 || _ghostOrientationY != 0)
            {
                _ghost.Play("fantomeEnVol");
            }
            //_positionPerso.X += _sensPersoHorizontal * _vitessePerso * deltaTime;
            //_positionPerso.Y += _sensPersoVertical * _vitessePerso * deltaTime;

            _tiledMapRenderer.Update(gameTime);

            //Déplacement
            
            _keyboardState = Keyboard.GetState();
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                //animation droite
                _positionPerso += _sensPersoHorizontal * _vitessePerso * deltaTime;
            }
            //flèche gauche
            if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                //animation gauche
                _positionPerso -= _sensPersoHorizontal * _vitessePerso * deltaTime;
            }
            //flèche haut
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _positionPerso -= _sensPersoVertical * _vitessePerso * deltaTime;
            }
            //flèche bas
            if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                //animation bas
                _positionPerso += _sensPersoVertical * _vitessePerso * deltaTime;
            }
            //Camera
            _camera.LookAt(_positionPerso);
            //_cameraPosition = _positionPerso;
            const float movementSpeed = 200;
            _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());

            _bat.Update(deltaTime);
            _skeleton.Update(deltaTime);
            _perso.Update(deltaTime);
            _ghost.Update(deltaTime);

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.LightYellow); // on utilise la reference vers Game1 pour changer le graphisme
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //_tiledMapRenderer.Draw();
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());
            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix);
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.Draw(_skeleton, _skeletonPosition);
            _spriteBatch.Draw(_bat, _batPosition);
            _spriteBatch.Draw(_ghost, _ghostPosition);
            _spriteBatch.End();
        }
        public bool CollisionJoueur(int xObjet, int yObjet, int largeurObjet, int hauteurObjet)
        {
            Rectangle rectJoueur = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, LARGEUR_PERSO, HAUTEUR_PERSO);
            Rectangle rectObjet = new Rectangle(xObjet, yObjet, largeurObjet, hauteurObjet);
            return rectJoueur.Intersects(rectObjet);
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
        private Vector2 GetMovementDirection()
        {
            var movementDirection = Vector2.Zero;
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Down))
            {
                movementDirection += Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                movementDirection -= Vector2.UnitY;
            }
            if (state.IsKeyDown(Keys.Left))
            {
                movementDirection -= Vector2.UnitX;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                movementDirection += Vector2.UnitX;
            }
            return movementDirection;
        }
    }
}
