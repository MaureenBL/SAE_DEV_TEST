using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace SAE
{
    internal class Jouer
    {
        public class Game2 : Game
        {
            private GraphicsDeviceManager _graphics;
            private SpriteBatch _spriteBatch;
            //PERSONNAGE - GEORGE
            private AnimatedSprite _perso;
            private Vector2 _positionPerso;
            private int _sensPersoHorizontal;
            private int _sensPersoVertical;
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
            public int[,] zoneFantome;

            public Game2()
            {
                _graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                IsMouseVisible = true;
            }

            protected override void Initialize()
            {
                // TODO: Add your initialization logic here
                //propriétés des monstres
                _vitesseBat = 0;
                _vitesseGhost = 0;
                _vitesseSkeleton = 100;
                base.Initialize();
            }

            protected override void LoadContent()
            {
                _spriteBatch = new SpriteBatch(GraphicsDevice);
                SpriteSheet persoTexture = Content.Load<SpriteSheet>("george.sf", new JsonContentLoader());
                _perso = new AnimatedSprite(persoTexture);
                // TODO: use this.Content to load your game content here
            }

            protected override void Update(GameTime gameTime)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


                /*if(CollisionJoueur(avec la zone) && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    _nbDebattage += 1;
                }

                //SQUELETTE
                 if(VoirJoueur())
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
                  }

                //FANTOME
                 do
                  {
                    ghost.Play("fantomeInvoque");
                    _ghostPosition = _positionPerso;
                  }
                 //while(CollisionJoueur(avec la zone) && _nbDebattage < 25)
                 
                 //if

            _skeletonPosition.X += _skeletonOrientationX * _vitesseSkeleton * deltaTime;
            _skeletonPosition.Y += _skeletonOrientationY * _vitesseSkeleton * deltaTime;*/
                //ANIMATION
                //Personnage
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    _perso.Play("gBas");
                    _perso.Update(deltaTime);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    _perso.Play("gHaut");
                    _perso.Update(deltaTime);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    _perso.Play("gDroite");
                    _perso.Update(deltaTime);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    _perso.Play("gGauche");
                    _perso.Update(deltaTime);
                }
                //Squelette

                _positionPerso.X += _sensPersoHorizontal * _vitessePerso * deltaTime;
                _positionPerso.Y += _sensPersoVertical * _vitessePerso * deltaTime;

                base.Update(gameTime);
            }

            protected override void Draw(GameTime gameTime)
            {
                
                GraphicsDevice.Clear(Color.CornflowerBlue);
                _spriteBatch.Begin();
                _spriteBatch.Draw(_perso, _positionPerso);
                _spriteBatch.End();
                // TODO: Add your drawing code here

                base.Draw(gameTime);
            }
            /*public bool CollisionJoueur(int xObjet, int yObjet, int largeurObjet, int hauteurObjet)
            {
             Rectangle rectJoueur = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, LARGEUR_PERSO, HAUTEUR_PERSO);
             Rectangle rectObjet = new Rectangle(xObjet, yObjet, largeurObjet, hauteurObjet);
             return rectJoueur.Intersects(rectObjet);
            }*/
           /* public bool VoirJoueur(int xMonstre, int yMonstre)
            {
                Rectangle rectJoueur = new Rectangle((int)_positionPerso.X, (int)_positionPerso.Y, LARGEUR_PERSO, HAUTEUR_PERSO);
                if()
                Rectangle rectVision = new Rectangle(xMonstre, yMonstre, 200, 50);
                return rectJoueur.Intersects(rectObjet);
            }*/
        }
    }
}

