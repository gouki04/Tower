using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tower
{
    public class Portal : TileComponent
    {
        public TileMap TargetFloor = null;
        public int TargetRow = 0;
        public int TargetColumn = 0;

        public override IEnumerator OnTrigger(Player player)
        {
            yield return 0;
            Logic.Instance.Transport(TargetFloor, TargetRow, TargetColumn);
        }
    }
}
