using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assets.Essences
{
    public class Game1
    {
        public List<Rock> Rocks { get; set; }

        public static int Turn { get; set; }

        public Player BlackPlayer { get; set; }

        public Player WhitePlayer { get; set; }

        public Game1()
        {
            InitRocks();

            BlackPlayer = new Player(true, 0);
            WhitePlayer = new Player(false, 0);
        }

        private void InitRocks()
        {
            Rocks = new List<Rock>();
            var row = 0;
            var col = 0;

            for (var i = 0; i < 81; i++)
            {
                var rock = new Rock(i)
                {
                    row = row,
                    col = col++
                };

                if (col > 8)
                {
                    col = 0;
                    row++;
                }

                Rocks.Add(rock);
                //UnityEngine.Debug.Log(rock.row + " + " + rock.col);
            }
        }
    }
}

