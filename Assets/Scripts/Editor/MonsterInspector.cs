using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.Monster))]
    public class MonsterInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var monster = target as Tower.Monster;
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
