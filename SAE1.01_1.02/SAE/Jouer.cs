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

            public Game2()
            {
                _graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                IsMouseVisible = true;
            }

            protected override void Initialize()
            {
                // TODO: Add your initialization logic here

                base.Initialize();
            }

            protected override void LoadContent()
            {
                _spriteBatch = new SpriteBatch(GraphicsDevice);

                // TODO: use this.Content to load your game content here
            }

            protected override async void Update(GameTime gameTime)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                //SQUELETTE
                /* if(Voir())
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

                /* //FANTOME
                  if(Collision entre joueur et zone de spawn)
                  {

                  }*/

                _skeletonPosition.X += _skeletonOrientationX * _vitesseSkeleton * deltaTime;
            _skeletonPosition.Y += _skeletonOrientationY * _vitesseSkeleton * deltaTime;
                base.Update(gameTime);
            }

            protected override void Draw(GameTime gameTime)
            {
                _spriteBatch.End();
                GraphicsDevice.Clear(Color.CornflowerBlue);
                var chauveSourisTexture = Content.Load<SpriteSheet>("bat.sf", new JsonContentLoader());
                var chauveSouris = new AnimatedSprite(chauveSourisTexture);
                chauveSouris.Play("batVolFace");
                // TODO: Add your drawing code here

                base.Draw(gameTime);
            }
        }
    }
}

