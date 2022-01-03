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
                                velocity.Y = 0f;
                            }
                        }
                    }
                }
            }
        }

        public static Projectile NewProjectile(ProjectileType type, Vector2 position, Vector2 velocity, float damage, AlignmentKey damageAlignment, GameObject source = null, float knockback = 0f)
        {
            Projectile projectile = new Projectile();
            int oIndex = 0;
            while (Main.objects[oIndex] != null)
            {
                oIndex++;
                if (oIndex >= Main.objects.Length)
                {
                    return null;
                }
            }

            projectile.type = type;
            projectile.position = position;
            projectile.velocity = velocity;
            projectile.damage = damage;
            projectile.damageAlignment = damageAlignment;
            projectile.source = source;
            projectile.knockback = knockback;

            int index = 0;
            while (Main.projectiles[index] != null)
            {
                index++;
                if (index >= Main.projectiles.Length)
                {
                    return null;
                }
            }

            Main.objects[oIndex] = projectile;
            projectile.objectsIndex = oIndex;

            Main.projectiles[index] = projectile;
            projectile.projectilesIndex = index;

            projectile.Initialize();
            return projectile;
        }

        public static Projectile FindClosest(Vector2 position)
        {
            bool foundSomething = false;
            float distance = -1f;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if (projectile.Distance(position) < distance || distance == -1f)
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
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
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if ((projectile.Distance(position) <= distance || distance == -1f) && projectile.damageAlignment == alignment)
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
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
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if (projectile.Distance(position) <= distance)
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
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
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if ((projectile.Distance(position) <= distance) && projectile.damageAlignment == alignment)
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(Projectile otherThan, Vector2 position)
        {
            bool foundSomething = false;
            float distance = -1f;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if ((projectile.Distance(position) < distance || distance == -1f) && projectile != otherThan)
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(Projectile otherThan, Vector2 position, AlignmentKey alignment)
        {
            bool foundSomething = false;
            float distance = -1f;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if ((projectile.Distance(position) <= distance || distance == -1f) && projectile.damageAlignment == alignment && projectile != otherThan)
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(Projectile otherThan, Vector2 position, float maxDistance)
        {
            bool foundSomething = false;
            float distance = maxDistance;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if (projectile.Distance(position) <= distance && projectile != otherThan)
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(Projectile otherThan, Vector2 position, float maxDistance, AlignmentKey alignment)
        {
            bool foundSomething = false;
            float distance = maxDistance;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if ((projectile.Distance(position) <= distance) && projectile.damageAlignment == alignment && projectile != otherThan)
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(List<Projectile> otherThan, Vector2 position)
        {
            bool foundSomething = false;
            float distance = -1f;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if ((projectile.Distance(position) < distance || distance == -1f) && !otherThan.Contains(projectile))
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(List<Projectile> otherThan, Vector2 position, AlignmentKey alignment)
        {
            bool foundSomething = false;
            float distance = -1f;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if ((projectile.Distance(position) <= distance || distance == -1f) && projectile.damageAlignment == alignment && !otherThan.Contains(projectile))
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(List<Projectile> otherThan, Vector2 position, float maxDistance)
        {
            bool foundSomething = false;
            float distance = maxDistance;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if (projectile.Distance(position) <= distance && !otherThan.Contains(projectile))
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
                }
            }

            if (foundSomething)
            {
                return proj;
            }
            return null;
        }

        public static Projectile FindClosest(List<Projectile> otherThan, Vector2 position, float maxDistance, AlignmentKey alignment)
        {
            bool foundSomething = false;
            float distance = maxDistance;

            Projectile proj = new Projectile();
            for (int i = 0; i < Main.projectiles.Length; i++)
            {
                Projectile projectile = Main.projectiles[i];
                if (projectile != null && projectile.active)
                {
                    if ((projectile.Distance(position) <= distance) && projectile.damageAlignment == alignment && !otherThan.Contains(projectile))
                    {
                        foundSomething = true;
                        distance = projectile.Distance(position);
                        proj = projectile;
                    }
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
            active = true;
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
            active = false;
            Main.projectiles[projectilesIndex] = null;
            Main.objects[objectsIndex] = null;
        }
    }
}