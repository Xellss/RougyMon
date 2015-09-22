using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    abstract class Objects
    {
        public abstract char Icon { get; }
        public abstract string Name { get; }

        public Vector2i Position 
        {
            get { return position; }
            set 
            { 
                position = value;
                if (Map.Level == 1)
                {
                    MapCity.GetMapField(position).Content = this;
                }
                else if (Map.Level == 2)
                {
                    MapForest.GetMapField(position).Content = this;
                }
                else if (Map.Level == 3)
                {
                    MapGraveyard.GetMapField(position).Content = this;
                }
            }
        }
        private Vector2i position;
        public Objects(Vector2i position)
        {
            this.position = position;
            if (Map.Level == 1)
            {
                MapField mapField = MapCity.GetMapField(position);
                mapField.Content = this; 
            }
            else if (Map.Level == 2)
            {
                MapField mapField = MapForest.GetMapField(position);
                mapField.Content = this;
            }
            else if (Map.Level == 3)
            {
                MapField mapField = MapGraveyard.GetMapField(position);
                mapField.Content = this;
            }
        }
    }
}
