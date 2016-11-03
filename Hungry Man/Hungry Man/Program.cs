using System;

namespace Hungry_Man
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HungryMan game = new HungryMan())
            {
                game.Run();
            }
        }
    }
#endif
}

