using UnityEngine;
using System.Collections;
using System;

namespace Tower
{
    public enum EKey
    {
        Yellow,
        Blue,
        Red,
        Normal,
    }

    public class Key : Obj
    {
        public EKey KeyType = EKey.Yellow;

        public override bool Trigger(Game game)
        {
            game.AddKey(KeyType);
            return true;
        }
    }
}

