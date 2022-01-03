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
    public class Platform : GameObject
    {
        public int platformsIndex;
        public float damage;
        public PlatformType type;
        public Texture2D texture = null;
        public double timeAlive = 0f;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {
            type.Draw(gameTime, spriteBatch, layerDepth, this);
        }

        public override void Update(GameTime gameTime)
        {
            timeAlive += Main.deltaTime;
            UpdateMovement(gameTime);
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
                                    gObject.position.X += Main.physicsBackstepSpace * Math.Sign(velocity.X);
                                }
                                gObject.velocity.X = 0f;
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
                                velocity.X = 0f;
                            }
                        }
                    }
                }
            }
        }

        public static Platform NewPlatform(PlatformType type, Vector2 position, Vector2 velocity)
        {
            Platform platform = new Platform();
            int oIndex = 0;
            while (Main.objects[oIndex] != null)
            {
                oIndex++;
                if (oIndex >= Main.objects.Length)
                {
                    return null;
                }
            }

            platform.type = type;
            platform.position = position;
            platform.velocity = velocity;

            int index = 0;
            while (Main.platforms[index] != null)
            {
                index++;
                if (index >= Main.platforms.Length)
                {
                    return null;
                }
            }

            Main.objects[oIndex] = platform;
            platform.objectsIndex = oIndex;

            Main.platforms[index] = platform;
            platform.platformsIndex = index;

            platform.Initialize();
            return platform;
        }

        public void Initialize()
        {
            active = true;
            solid = true;
            collideWithSolids = false;
            type.InitializeStats(this);
        }

        public override void ConsistentUpdate(GameTime gameTime)
        {
            if (type.PreAI(this))
            {
                type.AI(this);
                type.PostAI(this, true);
            }
            else
            {
                type.PostAI(this, false);
            }
        }

        public void KillPlatform()
        {
            active = false;
            Main.platforms[platformsIndex] = null;
            Main.objects[objectsIndex] = null;
        }
    }
}
