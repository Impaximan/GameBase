using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using GameBase.Extensions;
using GameBase.Objects;
using GameBase.Utils;

namespace GameBase
{
    public abstract class GameObject
    {
        public int width = 1;
        public int height = 1;
        public bool solid = false;
        public bool collideWithSolids = true;
        public Vector2 position = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        public int objectsIndex;
        public bool active = true;
        public int extraUpdates = 0;
        public float rotation;
        public float drawScale = 1f;

        public Rectangle GetRect()
        {
            return new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public Vector2 Size()
        {
            return new Vector2((float)width / 2f, (float)height / 2f);
        }
        public Vector2 Center()
        {
            return position + Size();
        }

        public float Distance(Vector2 position)
        {
            return Vector2.Distance(Center(), position);
        }

        public bool Colliding(GameObject other)
        {
            return other.GetRect().Intersects(GetRect());
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void ConsistentUpdate(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, float layerDepth)
        {

        }
    }
}
