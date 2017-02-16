using UnityEngine;
using System.Collections;
using System;

namespace Tower
{
    public class Door : Obj
    {
        public EKey KeyType = EKey.Yellow;

        public override bool Trigger(Game game)
        {
            return game.UseKey(KeyType);
        }
    }
}

