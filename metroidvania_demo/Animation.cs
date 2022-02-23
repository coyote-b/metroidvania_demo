using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetroidvaniaDemo
{
    public class Animation
    {
        public int FrameIndex { get; set; }
        public Texture2D SpriteSheet { get; private set; }
        public int FrameCount { get; set; }
        public Vector2 Dimension { get { return new Vector2(SpriteSheet.Width / FrameCount, SpriteSheet.Height); } }
        public int Delay { get; set; }
        public bool IsLooping { get; set; }
        public bool Flip { get; set; }
        public Vector2 Origin { get { return new Vector2(Dimension.X / 2, Dimension.Y / 2); } }

        public Animation(Texture2D spriteSheet, int frameCount, bool flip)
        {
            SpriteSheet = spriteSheet;
            FrameCount = frameCount;
            Flip = flip;
            Delay = 3;
            IsLooping = true;
        }
    }
}
