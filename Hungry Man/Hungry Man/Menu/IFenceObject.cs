using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hungry_Man.Menu
{
    interface IFenceObject
    {
        float LayerDepth { get; set; }
        void Update(GameTime time, Vector2 margin);
        void Draw(SpriteBatch sprite, GameTime time, Vector2 margin);
    }
}
