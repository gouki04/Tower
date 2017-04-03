using System;
using UnityEngine;
using Rotorz.Tile;

namespace Tower
{
    public class Obj : MonoBehaviour
    {
        public TileIndex Pos { get; set; }

        public virtual bool Trigger(Game game)
        {
            return false;
        }
    }
}
