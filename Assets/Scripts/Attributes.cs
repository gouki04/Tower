using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    [System.Serializable]
    public class Attributes : Dictionary<PlayerAttr, int>, ISerializationCallbackReceiver
    {
        #region serialzation

        [SerializeField]
        protected List<PlayerAttr> mKeys = new List<PlayerAttr>();

        [SerializeField]
        protected List<int> mValues = new List<int>();

        public void OnBeforeSerialize()
        {
            mKeys.Clear();
            mValues.Clear();

            mKeys.Capacity = this.Count;
            mValues.Capacity = this.Count;

            foreach (var kvp in this) {
                mKeys.Add(kvp.Key);
                mValues.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            var count = Mathf.Min(mKeys.Count, mValues.Count);
            for (var i = 0; i < count; ++i) {
                this.Add(mKeys[i], mValues[i]);
            }
        }

        #endregion

        #region property

        public int Hp
        {
            get {
                return this.GetAttr(PlayerAttr.Hp);
            }
            set {
                this.SetAttr(PlayerAttr.Hp, value);
            }
        }

        public int Atk
        {
            get {
                return this.GetAttr(PlayerAttr.Atk);
            }
            set {
                this.SetAttr(PlayerAttr.Atk, value);
            }
        }

        public int Def
        {
            get {
                return this.GetAttr(PlayerAttr.Def);
            }
            set {
                this.SetAttr(PlayerAttr.Def, value);
            }
        }

        public int Gold
        {
            get {
                return this.GetAttr(PlayerAttr.Gold);
            }
            set {
                this.SetAttr(PlayerAttr.Gold, value);
            }
        }

        public int Exp
        {
            get {
                return this.GetAttr(PlayerAttr.Exp);
            }
            set {
                this.SetAttr(PlayerAttr.Exp, value);
            }
        }

        public int Lv
        {
            get {
                return this.GetAttr(PlayerAttr.Lv);
            }
            set {
                this.SetAttr(PlayerAttr.Lv, value);
            }
        }

        public int Key_Yellow
        {
            get {
                return this.GetAttr(PlayerAttr.Key_Yellow);
            }
            set {
                this.SetAttr(PlayerAttr.Key_Yellow, value);
            }
        }

        public int Key_Blue
        {
            get {
                return this.GetAttr(PlayerAttr.Key_Blue);
            }
            set {
                this.SetAttr(PlayerAttr.Key_Blue, value);
            }
        }

        public int Key_Red
        {
            get {
                return this.GetAttr(PlayerAttr.Key_Red);
            }
            set {
                this.SetAttr(PlayerAttr.Key_Red, value);
            }
        }

        #endregion

        public int AdjustAttr(PlayerAttr attr_type, int value)
        {
            int old_value;
            if (this.TryGetValue(attr_type, out old_value)) {
                this[attr_type] = old_value + value;
            } else {
                this[attr_type] = value;
            }

            return this[attr_type];
        }

        public bool ExistAttr(PlayerAttr attr_type)
        {
            int dummy;
            if (this.TryGetValue(attr_type, out dummy)) {
                return true;
            } else {
                return false;
            }
        }

        public int GetAttr(PlayerAttr attr_type)
        {
            int value;
            if (this.TryGetValue(attr_type, out value)) {
                return value;
            } else {
                return 0;
            }
        }

        public void SetAttr(PlayerAttr attr_type, int value)
        {
            this[attr_type] = value;
        }

        public bool RemoveAttr(PlayerAttr attr_type)
        {
            return this.Remove(attr_type);
        }

        public void Combine(Attributes attrs)
        {
            foreach (var attr in attrs) {
                this.AdjustAttr(attr.Key, attr.Value);
            }
        }

        public void Cost(Attributes attrs)
        {
            foreach (var attr in attrs) {
                this.AdjustAttr(attr.Key, -attr.Value);
            }
        }

        public bool Include(Attributes attrs)
        {
            foreach (var attr in attrs) {
                if (!this.ExistAttr(attr.Key)) {
                    return false;
                }

                var my_val = this.GetAttr(attr.Key);
                if (my_val < attr.Value) {
                    return false;
                }
            }

            return true;
        }
    }
}
