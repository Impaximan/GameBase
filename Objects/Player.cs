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

            int index = Main.objects.Count;
            Main.objects.Add(player);
            player.objectsIndex = index;

            index = Main.players.Count;
            Main.players.Add(player);
            player.playersIndex = index;

            return player;
        }

        public void KillPlayer()
        {
            for (int i = objectsIndex; i < Main.objects.Count; i++)
            {
                Main.objects[i].objectsIndex--;
            }
            Main.objects.RemoveAt(objectsIndex + 1);

            for (int i = playersIndex; i < Main.players.Count; i++)
            {
                Main.players[i].playersIndex--;
            }
            Main.players.RemoveAt(playersIndex + 1);
        }
    }
}
