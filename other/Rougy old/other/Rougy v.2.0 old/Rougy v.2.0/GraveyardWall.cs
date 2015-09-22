using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class GraveyardWall : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                return (char)204;
            }
        }

        public override string Name
        {
            get { return "GraveyardWall"; }
        }
        public GraveyardWall(Vector2i position)
            : base(position)
        {
        }
    }
}
