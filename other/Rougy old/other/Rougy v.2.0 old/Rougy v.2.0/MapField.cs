using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class MapField
    {
        public Objects Content;

        public Vector2i Position { get; private set; }

        public MapField(Vector2i postion)
        {
            Position = postion;
        }
    }
}
