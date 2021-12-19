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
    public class ProjectileType
    {
        public virtual void InitializeStats(Projectile projectile)
        {
            projectile.texture = Main.Glow_2;
            projectile.drawScale = 0.01f;
        }

        public virtual bool PreAI(Projectile projectile)
        {
            return true;
        }

        public virtual void AI(Projectile projectile)
        {
            if (projectile.timeAlive >= 3f)
            {
                projectile.KillProjectile();
            }
        }

        public virtual void PostAI(Projectile projectile, bool preAI)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth, Projectile projectile)
        {
            spriteBatch.Draw(projectile.texture, projectile.position - Main.screenPosition, null, Color.White, projectile.rotation, Vector2.Zero, projectile.drawScale, SpriteEffects.None, layerDepth);
        }
    }
}