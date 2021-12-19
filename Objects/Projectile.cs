using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using GameBase.Extensions;

namespace GameBase.Objects
{
    public class Projectile : GameObject
    {
        public int projectilesIndex;
        public AlignmentKey damageAlignment = AlignmentKey.Pacifist;
        public int damage;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public static Projectile NewProjectile(Vector2 position, Vector2 velocity, int damage, AlignmentKey damageAlignment, GameObject source, float knockback = 0f)
        {
            Projectile projectile = new Projectile();
            int index = Main.objects.Count;
            Main.objects.Add(projectile);
            projectile.objectsIndex = index;

            index = Main.projectiles.Count;
            Main.projectiles.Add(projectile);
            projectile.projectilesIndex = index;
            return projectile;
        }

        public void KillProjectile()
        {
            for (int i = objectsIndex; i < Main.objects.Count; i++)
            {
                Main.objects[i].objectsIndex--;
            }
            Main.objects.RemoveAt(objectsIndex);

            for (int i = projectilesIndex; i < Main.projectiles.Count; i++)
            {
                Main.projectiles[i].projectilesIndex--;
            }
            Main.projectiles.RemoveAt(projectilesIndex);
        }
    }
}

