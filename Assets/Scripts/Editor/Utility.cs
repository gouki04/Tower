using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Tower.Editor
{
    public class Utility
    {
        protected static PlayerAttr mAttrType = PlayerAttr.Hp;
        protected static int mAttrValue = 0;

        public static bool AttributesField(Attributes attrs)
        {
            var changed = false;
            var attr_type_arr = Enum.GetValues(typeof(PlayerAttr));
            foreach (PlayerAttr attr_type in attr_type_arr) {
                if (attrs.ExistAttr(attr_type)) {
                    GUILayout.BeginHorizontal();
                    GUI.changed = false;
                    attrs.SetAttr(attr_type, EditorGUILayout.IntField(attr_type.ToString(), attrs[attr_type]));
                    if (GUI.changed) {
                        changed = true;
                    }

                    GUI.color = Color.red;
                    if (GUILayout.Button("X", GUILayout.Width(16))) {
                        attrs.RemoveAttr(attr_type);
                    }
                    GUI.color = GUI.contentColor;

                    GUILayout.EndHorizontal();
                }
            }

            if (attrs.Keys.Count < attr_type_arr.Length) {
                GUILayout.BeginHorizontal();
                mAttrType = (PlayerAttr)EditorGUILayout.EnumPopup(mAttrType);
                mAttrValue = EditorGUILayout.IntField(mAttrValue);

                if (GUILayout.Button("+", GUILayout.Width(16))) {
                    attrs.SetAttr(mAttrType, mAttrValue);
                    changed = true;
                }

                GUILayout.EndHorizontal();
            }

            return changed;
        }
    }
}
