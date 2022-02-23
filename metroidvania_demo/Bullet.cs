using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetroidvaniaDemo
{
    class Bullet : Sprite
    {
        public Bullet(Game game, Dictionary<string, Animation> animations) : base(game, animations)
        {
        }
    }
}
