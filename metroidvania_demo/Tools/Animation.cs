using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetroidvaniaDemo
{
    // a class for sprite animations
    public class Animation
    {
        public int FrameIndex { get; set; }
        public Texture2D SpriteSheet { get; private set; }
        public List<Rectangle> Frames { get; set; }
        public int Delay { get; set; }
        public bool IsLooping { get; set; }
        public bool Flip { get; set; }
        public Vector2 Origin { get { return new Vector2(Frames[FrameIndex].Width / 2, Frames[FrameIndex].Height / 2); } }

        public Animation(Texture2D spriteSheet)
        {
            SpriteSheet = spriteSheet;
        }
    }
}
