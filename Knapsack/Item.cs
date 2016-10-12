using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project7
{
    class Item
    {
        public int ID;
        public int weight;
        public int value;

        public Item(int id, int weight, int value)
        {
            this.ID = id;
            this.weight = weight;
            this.value = value;
        }        

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
