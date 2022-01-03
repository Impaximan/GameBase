using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using GameBase.Extensions;
using System.Diagnostics;

namespace GameBase.Objects
{
    public class Player : GameObject
    {
        public int playersIndex;
        public bool ctrlRight;
        public bool ctrlLeft;
        public bool ctrlUp;
        public bool ctrlDown;
        public bool ctrlJump;
        public bool grounded;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            spriteBatch.Draw(Main.Glow_2, Center() - Main.screenPosition, null, Color.White, 0f, Main.Glow_2.Size() / 2, 1f, SpriteEffects.None, layerDepth);
        }

        public override void Update(GameTime gameTime)
        {
            width = 200;
            height = 200;

            UpdateControls(gameTime);
            grounded = false;
            UpdateMovement(gameTime);
            if (position.Y > Main.screenHeight - 150)
            {
                position.Y = Main.screenHeight - 150;
                velocity.Y = 0f;
                grounded = true;
            }
        }

        public override void ConsistentUpdate(GameTime gameTime)
        {
            velocity.Y += 1f;
            if (!ctrlJump && velocity.Y < 0f)
            {
                velocity.Y += 1.5f;
            }
            if (!ctrlRight && !ctrlLeft)
            {
                velocity.X *= 0.9f;
            }
        }

        bool canJump = false;
        public void UpdateControls(GameTime gameTime)
        {
            ctrlRight = Main.keyboardstate.IsKeyDown(Main.keyRight);
            ctrlLeft = Main.keyboardstate.IsKeyDown(Main.keyLeft);
            ctrlUp = Main.keyboardstate.IsKeyDown(Main.keyUp);
            ctrlDown = Main.keyboardstate.IsKeyDown(Main.keyDown);
            ctrlJump = Main.keyboardstate.IsKeyDown(Main.keyJump);

            float speed = 15f;

            if (ctrlRight)
            {
                velocity.X = speed;
            }

            if (ctrlLeft)
            {
                velocity.X = -speed;
            }


            if (ctrlJump && grounded)
            {
                velocity.Y = -35f;
            }
        }

        public void UpdateMovement(GameTime gameTime)
        {
            position.X += velocity.X * (float)Main.deltaTime * Main.consistentUpdates;
            if (collideWithSolids && velocity != Vector2.Zero)
            {
                for (int i = 0; i < Main.objects.Length; i++)
                {
                    GameObject gObject = Main.objects[i];
                    if (gObject != null)
                    {
                        if (gObject.solid && gObject.Colliding(this))
                        {
                            if (velocity.X == 0)
                            {

                            }
                            else
                            {
                                while (gObject.Colliding(this))
                                {
                                    position.X += Main.physicsBackstepSpace * Math.Sign(velocity.X) * -1f;
                                }
                                velocity.X = 0f;
                            }
                        }
                    }
                }
            }

            position.Y += velocity.Y * (float)Main.deltaTime * Main.consistentUpdates;
            if (collideWithSolids && velocity != Vector2.Zero)
            {
                for (int i = 0; i < Main.objects.Length; i++)
                {
                    GameObject gObject = Main.objects[i];
                    if (gObject != null)
                    {
                        if (gObject.solid && gObject.Colliding(this))
                        {
                            if (velocity.Y == 0)
                            {

                            }
                            else
                            {
                                while (gObject.Colliding(this))
                                {
                                    position.Y += Main.physicsBackstepSpace * Math.Sign(velocity.Y) * -1f;
                                }
                                if (velocity.Y > 0)
                                {
                                    grounded = true;
                                }
                                velocity.Y = 0f;
                            }
                        }
                    }
                }
            }
        }

        public static Player NewPlayer()
        {
            Player player = new Player();
            player.active = true;
            player.collideWithSolids = true;
            player.solid = false;

            int index = 0;
            while (Main.objects[index] != null)
            {
                index++;
                if (index >= Main.objects.Length)
                {
                    return null;
                }
            }
            Main.objects[index] = player;
            player.objectsIndex = index;

            index = 0;
            while (Main.players[index] != null)
            {
                index++;
                if (index >= Main.players.Length)
                {
                    return null;
                }
            }
            Main.players[index] = player;
            player.playersIndex = index;

            return player;
        }

        public void KillPlayer()
        {
            active = false;
            Main.players[playersIndex] = null;
            Main.objects[objectsIndex] = null;
        }
    }
}
