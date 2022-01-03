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

        }

        public virtual bool PreAI(Projectile projectile)
        {
            return true;
        }

        public virtual void AI(Projectile projectile)
        {
        }

        public virtual void PostAI(Projectile projectile, bool preAI)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth, Projectile projectile)
        {
            spriteBatch.Draw(projectile.texture, projectile.Center() - Main.screenPosition, null, Color.White, projectile.rotation, projectile.texture.Size() / 2, projectile.drawScale, SpriteEffects.None, layerDepth);
        }
    }
}