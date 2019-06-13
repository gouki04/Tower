using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.Component.Monster))]
    public class MonsterInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var monster = target as Tower.Component.Monster;
            if (monster != null) {
                if (Tower.Editor.Utility.AttributesField(monster.Attrs)) {
                    if (!Application.isPlaying) {
                        EditorUtility.SetDirty(monster); // this line doesn't work at unity5
                        EditorSceneManager.MarkAllScenesDirty();
                    }
                }
            }
        }
    }
}
