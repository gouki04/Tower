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
        public int Row
        {
            get {
                return (int)Math.Floor(transform.localPosition.y);
            }
            set {
                transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
            }
        }

        public int Column
        {
            get {
                return (int)Math.Floor(transform.localPosition.x);
            }
            set {
                transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
            }
        }

        public Attributes Attrs
        {
            get {
                return mAllAttrs;
            }
        }

        [SerializeField]
        protected Attributes mAllAttrs = new Attributes();

        public Dictionary<string, bool> Switches
        {
            get {
                return m_Switched;
            }
        }
        protected Dictionary<string, bool> m_Switched = new Dictionary<string, bool>();

        public void OpenSwitch(string name)
        {
            m_Switched[name] = true;
        }

        public void CloseSwitch(string name)
        {
            m_Switched.Remove(name);
        }

        public bool CheckSwitch(string name)
        {
            bool ret;
            if (m_Switched.TryGetValue(name, out ret)) {
                return ret;
            } else {
                return false;
            }
        }
    }
}
