using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Triforce : Objects
    {
        public override char Icon
        {
            get { return (char)127; }
        }

        public override string Name
        {
            get { return "Triforce"; }
        }
        public static bool HaveTriforce = false;
        public Triforce(Vector2i position)
            : base(position)
        {

        }
    }
}
