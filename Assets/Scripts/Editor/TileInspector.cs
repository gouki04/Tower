using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.Tile))]
    public class TileInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var tile = target as Tower.Tile;
            if (tile != null) {
                tile.Row = EditorGUILayout.DelayedIntField("Row", tile.Row);
                tile.Column = EditorGUILayout.DelayedIntField("Column", tile.Column);
            }
        }
    }
}
