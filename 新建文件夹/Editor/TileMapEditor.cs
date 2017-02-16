using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor
{
    public void OnSceneGUI()
    {
        var tilemap = (TileMap)target;
        var pos = tilemap.transform.position;

        Handles.color = Color.black;
        for (var row = 0; row <= tilemap.Row; ++row)
        {
            var p = pos;
            p.y += row;

            Handles.DrawLine(p, p + Vector3.right * 11);
        }

        for (var col = 0; col <= tilemap.Col; ++col)
        {
            var p = pos;
            p.x += col;

            Handles.DrawLine(p, p + Vector3.up * 11);
        }

        Handles.Label(pos, Event.current.mousePosition.ToString());
        Handles.color = Color.white;
    }
}
