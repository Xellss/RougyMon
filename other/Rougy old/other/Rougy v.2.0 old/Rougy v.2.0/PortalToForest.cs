using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class PortalToForest : Objects
    {
        public override char Icon
        {
            get
            {
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                return 'O'; 
            }
        }

        public override string Name
        {
            get { return "Portal to Forest"; }
        }
        public PortalToForest(Vector2i position)
            : base(position)
        {
        }

        
    }
}
