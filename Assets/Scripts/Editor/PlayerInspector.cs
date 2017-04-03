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
                if (Tower.Editor.Utility.AttributesField(player.Attrs)) {
                    if (!Application.isPlaying) {
                        EditorUtility.SetDirty(player); // this line doesn't work at unity5
                        EditorSceneManager.MarkAllScenesDirty();
                    }
                }
            }
        }
    }
}
