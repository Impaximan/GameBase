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
    public class PlatformType
    {
        public virtual void InitializeStats(Platform platform)
        {

        }

        public virtual bool PreAI(Platform platform)
        {
            return true;
        }

        public virtual void AI(Platform platform)
        {
        }

        public virtual void PostAI(Platform platform, bool preAI)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth, Platform platform)
        {
            spriteBatch.Draw(platform.texture, platform.Center() - Main.screenPosition, null, Color.White, 0f, platform.texture.Size() / 2, platform.drawScale, SpriteEffects.None, layerDepth);
        }
    }
}
