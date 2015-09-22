using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class ForestWall : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                return 'B';
            }
        }

        public override string Name
        {
            get { return "ForestWall"; }
        }
        public ForestWall(Vector2i position)
            : base(position)
        {
        }
    }
}
