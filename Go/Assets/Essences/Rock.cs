using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Essences
{
    public class Rock
    {
        int _number;

        public int row { get; set; }

        public int col { get; set; }

        public int type { get; set; }

        public Rock(int number)
        {
            _number = number;
            type = 0;
        }


    }
}
