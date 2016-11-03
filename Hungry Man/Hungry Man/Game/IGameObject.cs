using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Hungry_Man
{
    interface IGameObject
    {
        Vector2 position { get; set; }
        bool alive { get; set; }
        Rectangle rectangle { get; set; }

        void Update(GameTime time, List<IGameObject>[,] mainArray);

        void Draw(SpriteBatch sprite, GameTime time);
    }
}
