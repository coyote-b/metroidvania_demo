using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace MetroidvaniaDemo.Managers
{
    static class SpritesheetManager
    {
        internal static Dictionary<string, Animation> GetAnimations(string filepath)
        {
            var animations = new Dictionary<string, Animation>();

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filepath);
            using XmlReader reader = XmlReader.Create(stream);
            reader.ReadToFollowing("sprites");

            do
            {
                reader.MoveToFirstAttribute();
                string filename = reader.Value;
                Animation animation = new Animation(Shared.game.Content.Load<Texture2D>(filename));

                reader.ReadToFollowing("animation");
                string name = reader.GetAttribute("title");
                animation.Delay = int.Parse(reader.GetAttribute("delay"));

                List<Rectangle> frames = new List<Rectangle>();
                reader.ReadToFollowing("cut");

                do
                {
                    int width = int.Parse(reader.GetAttribute("w"));
                    int x = int.Parse(reader.GetAttribute("x"));
                    int y = int.Parse(reader.GetAttribute("y"));
                    int height = int.Parse(reader.GetAttribute("h"));

                    Rectangle frame = new Rectangle(x, y, width, height);
                    frames.Add(frame);
                } while (reader.ReadToNextSibling("cut"));

                animation.Frames = frames;

                animations.Add(name, animation);
            } while (reader.ReadToFollowing("sprites"));

            return animations;
        }
    }
}
