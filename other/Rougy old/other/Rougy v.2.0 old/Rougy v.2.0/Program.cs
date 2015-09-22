using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
            Player player = new Player(new Vector2i(2, 5));
            MapCity.DrawMap(); 
            while (Player.Health > 0)
            {
                player.KeyInput();
                Console.Write("Gold: " + Gold.GoldCounter + " Keys: " + Key.KeyCounter + " Health: " + Player.Health);
                Ork.OrkOneKI();
                Ork.OrkTwoKI();
                Skeleton.skeletonOneKI();
                Skeleton.skeletonTwoKI();
                SkeletonKing.skeletonKingKI();
                GameOver gameOver = new GameOver();

                player.KeyInput();
                
                PortalToGraveyard.PortalIconChange();

            }
        }
    }
}
