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
    public class Player : GameObject
    {
        public int playersIndex;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public static Player NewPlayer()
        {
            Player player = new Player();
            player.active = true;

            int index = 0;
            while (Main.objects[index] != null)
            {
                index++;
                if (index >= Main.objects.Length)
                {
                    return null;
                }
            }
            Main.objects[index] = player;
            player.objectsIndex = index;

            index = 0;
            while (Main.players[index] != null)
            {
                index++;
                if (index >= Main.players.Length)
                {
                    return null;
                }
            }
            Main.players[index] = player;
            player.playersIndex = index;

            return player;
        }

        public void KillPlayer()
        {
            active = false;
            Main.players[playersIndex] = null;
            Main.objects[objectsIndex] = null;
        }
    }
}
