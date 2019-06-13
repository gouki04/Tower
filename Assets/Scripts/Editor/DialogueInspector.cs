using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.Component.Dialogue))]
    public class DialogueInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var dialogue = target as Tower.Component.Dialogue;
            if (dialogue != null) {
                
            }
        }
    }
}
