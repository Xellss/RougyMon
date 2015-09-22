using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class BlankSpace : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                return ' '; 
            }
        }

        public override string Name
        {
            get { return "BlankSpace"; }
        }
        public BlankSpace(Vector2i position)
            : base(position)
        {
        }
    }
}
