using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class PortalToGraveyard : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                return PortalIcon;
            }
        }

        public override string Name
        {
            get 
            { 
                return PortalName; 
            }
        }
        static char PortalIcon;
        static string PortalName;
        public static void PortalIconChange()
        {
            if (Key.KeyCounter == 0)
            {
                PortalIcon = 'X';
            }
            else if (Key.KeyCounter > 0)
            {
                PortalIcon = 'O';
            }
        }
        private static void PortalNameChange()
        {
            if (Key.KeyCounter == 0)
            {
                PortalName = "Portal to Graveyard /Closed";
            }
            else if (Key.KeyCounter > 0)
            {
                PortalName = "Portal to Graveyard /Open";
            }
        }
        public PortalToGraveyard(Vector2i position)
            : base(position)
        {
            PortalIconChange();
            PortalNameChange();
        }
        
    }
}
