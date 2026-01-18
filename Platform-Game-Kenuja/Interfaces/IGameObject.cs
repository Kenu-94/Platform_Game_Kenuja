using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_Game_Kenuja.Interfaces
{
    public interface IGameObject
    {
        Rectangle BoundingBox { get; }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }


}
