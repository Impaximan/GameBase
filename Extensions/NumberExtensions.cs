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
    public static class NumberExtensions
    {
        public static Vector2 ToRotationVector2(this double n1, float length = 1)
        {
            Vector2 North = new Vector2(0, -1);
            var RotationMatrix = Matrix.CreateRotationZ((float)n1);
            return Vector2.Transform(North, RotationMatrix) * length;
        }

        public static Vector2 ToRotationVector2(this float n1, float length = 1)
        {
            Vector2 North = new Vector2(0, -1);
            var RotationMatrix = Matrix.CreateRotationZ((float)n1);
            return Vector2.Transform(North, RotationMatrix) * length;
        }

        public static float RotateAngle(this float n1, float radians)
        {
            float n2 = n1 + radians;
            return MathHelper.WrapAngle(n2);
        }
    }
}
