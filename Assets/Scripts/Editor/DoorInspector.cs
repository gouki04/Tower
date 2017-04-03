using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.Door))]
    public class DoorInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var door = target as Tower.Door;
            if (door != null) {
                if (Tower.Editor.Utility.AttributesField(door.Cost)) {
                    if (!Application.isPlaying) {
                        EditorUtility.SetDirty(door); // this line doesn't work at unity5
                        EditorSceneManager.MarkAllScenesDirty();
                    }
                }
            }
        }
    }
}
