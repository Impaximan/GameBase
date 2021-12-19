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
    public static class Vector2Extensions
    {
        public static Vector2 Normalized(this Vector2 v1)
        {
            Vector2 v2 = v1;
            v2.Normalize();
            return v2;
        }

        public static double ToRotationDouble(this Vector2 v1)
        {
            return Math.Atan2(v1.Y, v1.X);
        }
        public static float ToRotation(this Vector2 v1)
        {
            return (float)Math.Atan2(v1.Y, v1.X);
        }

        public static Vector2 RotatedBy(this Vector2 v1, float radians)
        {
            float length = v1.Length();
            float rotation = v1.ToRotation();
            rotation += radians;
            rotation = MathHelper.WrapAngle(rotation);
            return rotation.ToRotationVector2(length);
        }

        public static Vector2 Rounded(this Vector2 v1)
        {
            Vector2 v2 = v1;
            v2.Round();
            return v2;
        }
    }
}
