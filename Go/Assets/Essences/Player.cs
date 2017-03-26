using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Essences
{
    public class Player
    {
        public bool isBlack;

        public double Score { get; set; }

        public List<Rock> rocks;

        public Player(bool isBlack, double score)
        {
            this.isBlack = isBlack;
            this.Score = score;

            rocks = new List<Rock>();
        }

        
    }
}
