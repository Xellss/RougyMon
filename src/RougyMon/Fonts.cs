using Microsoft.Xna.Framework.Graphics;

namespace RougyMon
{
    public static class Fonts
    {
        public static SpriteFont Arial;
        public static SpriteFont ComicSans;
        static Fonts()
        {
            Arial = Managers.Content.Load<SpriteFont>("Fonts/Arial");
            ComicSans = Managers.Content.Load<SpriteFont>("Fonts/ComicSansMS");
        }
    }
}
