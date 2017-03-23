using System;
using UnityEngine;

namespace Tower
{
    public class SpiritTalk : Talk
    {
        public void MoveTo(Game game, string pos)
        {
            var pos_list = pos.Split(',');
            if (pos_list.Length == 2) {
                var row = int.Parse(pos_list[0]);
                var col = int.Parse(pos_list[1]);

                game.MoveTile(Pos.row, Pos.column, row, col);
            }
        }
    }
}