using System;

namespace CrooksAndCastles
{
#if WINDOWS || XBOX
    static class GameMain
    {
        static void Main(string[] args)
        {
            using (CrooksAndCastles game = new CrooksAndCastles())
            {
                game.Run();
            }
        }
    }
#endif
}

