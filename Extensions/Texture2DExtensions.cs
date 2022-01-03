using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace GameBase.Extensions
{
    public static class Texture2DExtensions
    {
        public static Vector2 Size(this Texture2D t1)
        {
            return new Vector2(t1.Width, t1.Height);
        }
    }
}

