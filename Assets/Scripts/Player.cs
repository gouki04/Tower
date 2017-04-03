using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tower
{
    public enum PlayerAttr
    {
        Hp = 1,
        Atk,
        Def,
        Gold,
        Exp,
        Lv,
        Key_Yellow,
        Key_Blue,
        Key_Red,
    }

    public class Player : MonoBehaviour
    {
        public Attributes Attrs
        {
            get {
                return mAllAttrs;
            }
        }

        [SerializeField]
        protected Attributes mAllAttrs = new Attributes();
    }
}
