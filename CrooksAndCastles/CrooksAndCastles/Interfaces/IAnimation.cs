using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CrooksAndCastles.Interfaces
{
    public interface IAnimation
    {
        void playCharapterAnimation(GameTime gameTime);
        void ChangeAsset(ContentManager content, string asset, int numberOfFrames);
    }
}