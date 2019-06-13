using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.Component.Item))]
    public class ItemInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var item = target as Tower.Component.Item;
            if (item != null) {
                if (Tower.Editor.Utility.AttributesField(item.Attrs)) {
                    if (!Application.isPlaying) {
                        EditorUtility.SetDirty(item); // this line doesn't work at unity5
                        EditorSceneManager.MarkAllScenesDirty();
                    }
                }
            }
        }
    }
}
