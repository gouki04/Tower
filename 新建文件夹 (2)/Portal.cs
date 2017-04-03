using UnityEngine;
using System.Collections;

namespace Tower
{
    public class PortalOld : Obj
    {
        public Rotorz.Tile.TileSystem TargetFloor = null;
        public Rotorz.Tile.TileIndex TargetPos;

        public override bool Trigger(Game game)
        {
            game.Transport(TargetFloor, TargetPos);
            return false;
        }
    }
}

