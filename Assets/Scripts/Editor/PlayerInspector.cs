using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.Player))]
    public class PlayerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var player = target as Tower.Player;
            if (player != null) {
                GUILayout.Space(3);
                GUILayout.Label("Attributes:");
                if (Tower.Editor.Utility.AttributesField(player.Attrs)) {
                    if (!Application.isPlaying) {
                        EditorUtility.SetDirty(player); // this line doesn't work at unity5
                        EditorSceneManager.MarkAllScenesDirty();
                    }
                }

                GUILayout.Space(3);
                GUILayout.Label("Switches:");
                if (Tower.Editor.Utility.SwitchField(player.Switches)) {
                    if (!Application.isPlaying) {
                        EditorUtility.SetDirty(player); // this line doesn't work at unity5
                        EditorSceneManager.MarkAllScenesDirty();
                    }
                }
            }
        }
    }
}
