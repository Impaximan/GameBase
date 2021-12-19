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
    public class Projectile : GameObject
    {
        public int projectilesIndex;
        public AlignmentKey damageAlignment = AlignmentKey.Pacifist;
        public float damage;
        public ProjectileType type;
        public GameObject source;
        public float knockback;
        public string displayName = "Nameless Projectile";
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
            position.X += velocity.X * (float)Main.deltaTime;
            if (collideWithSolids)
            {
                foreach (GameObject gObject in Main.objects)
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

            position.Y += velocity.Y * (float)Main.deltaTime;
            if (collideWithSolids)
            {
                foreach (GameObject gObject in Main.objects)
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

        public static Projectile NewProjectile(ProjectileType type, Vector2 position, Vector2 velocity, float damage, AlignmentKey damageAlignment, GameObject source = null, float knockback = 0f)
        {
            Projectile projectile = new Projectile();
            int index = Main.objects.Count;
            projectile.type = type;
            projectile.position = position;
            projectile.velocity = velocity;
            projectile.damage = damage;
            projectile.damageAlignment = damageAlignment;
            projectile.source = source;
            projectile.knockback = knockback;

            Main.objects.Add(projectile);
            projectile.objectsIndex = index;

            index = Main.projectiles.Count;
            Main.projectiles.Add(projectile);
            projectile.projectilesIndex = index;

            projectile.Initialize();

            Debug.WriteLine("Spawned projectile with display name: " + projectile.displayName);
            Debug.WriteLine("There is now " + Main.projectiles.Count + " projectile(s) in Main.projectiles");
            return projectile;
        }

        public static Projectile FindClosest(Vector2 position)
        {
            bool foundSomething = false;
            float distance = -1f;

            Projectile proj = new Projectile();
            foreach (Projectile projectile in Main.projectiles)
            {
                if (projectile.Distance(position) < distance || distance == -1f)
                {
                    foundSomething = true;
                    distance = projectile.Distance(position);
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(Vector2 position, AlignmentKey alignment)
        {
            bool foundSomething = false;
            float distance = -1f;

            Projectile proj = new Projectile();
            foreach (Projectile projectile in Main.projectiles)
            {
                if ((projectile.Distance(position) <= distance || distance == -1f) && projectile.damageAlignment == alignment)
                {
                    foundSomething = true;
                    distance = projectile.Distance(position);
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(Vector2 position, float maxDistance)
        {
            bool foundSomething = false;
            float distance = maxDistance;

            Projectile proj = new Projectile();
            foreach (Projectile projectile in Main.projectiles)
            {
                if (projectile.Distance(position) <= distance)
                {
                    foundSomething = true;
                    distance = projectile.Distance(position);
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(Vector2 position, float maxDistance, AlignmentKey alignment)
        {
            bool foundSomething = false;
            float distance = maxDistance;

            Projectile proj = new Projectile();
            foreach (Projectile projectile in Main.projectiles)
            {
                if ((projectile.Distance(position) <= distance) && projectile.damageAlignment == alignment)
                {
                    foundSomething = true;
                    distance = projectile.Distance(position);
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public void Initialize()
        {
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

        public void KillProjectile()
        {
            for (int i = objectsIndex; i < Main.objects.Count; i++)
            {
                Main.objects[i].objectsIndex--;
            }
            Main.objects.RemoveAt(objectsIndex + 1);

            for (int i = projectilesIndex; i < Main.projectiles.Count; i++)
            {
                Main.projectiles[i].projectilesIndex--;
            }
            Main.projectiles.RemoveAt(projectilesIndex + 1);
        }
    }
}