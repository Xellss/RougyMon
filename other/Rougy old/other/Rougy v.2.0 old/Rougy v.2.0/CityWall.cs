using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class CityWall : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                return '#';
            }
        }

        public override string Name
        {
            get { return "CityWall"; }
        }
        public CityWall(Vector2i position)
            : base(position)
        {
        }
    }
}
