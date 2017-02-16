using System;
using UnityEngine;

namespace Tower
{
    public class Obj : MonoBehaviour
    {
        public virtual bool Trigger(Game game)
        {
            return false;
        }
    }
}
