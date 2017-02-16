using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

[Serializable]
public class TilesetWindow : EditorWindow
{
    protected List<GameObject> Tiles = new List<GameObject>();
    protected static GameObject CurrentSelection = null;

    protected float WindowWidth
    {
        get
        {
            return position.width;
        }
    }

    [MenuItem("Window/TileSet Window")]
	public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TilesetWindow));
    }

    protected void Refresh()
    {
        Tiles.Clear();

        foreach (var path in Directory.GetFiles(Application.dataPath + "/Resources/Prefabs/Tile/"))
        {
            if (Path.GetExtension(path) == ".prefab")
            {
                var tile = Resources.Load<GameObject>("Prefabs/Tile/" + Path.GetFileNameWithoutExtension(path));
                Tiles.Add(tile);
            }
        }
    }

    protected void DrawTile(GameObject tile)
    {
        var spr = tile.GetComponent<SpriteRenderer>();
        var tex = spr.sprite.texture;
        var tex_coord = spr.sprite.textureRect;
        tex_coord.x = tex_coord.x / tex.width;
        tex_coord.width = tex_coord.width / tex.width;
        tex_coord.y = tex_coord.y / tex.height;
        tex_coord.height = tex_coord.height / tex.height;

        var rect = GUILayoutUtility.GetRect(64, 64, GUIStyle.none);
        GUI.color = CurrentSelection == tile ? new Color(153 / 255f, 204 / 255f, 1f, 1) : Color.white;

        GUI.DrawTextureWithTexCoords(rect, tex, tex_coord);

        if (Event.current.type == EventType.MouseDown)
        {
            if (rect.Contains(Event.current.mousePosition))
            {
                if (Event.current.button == 0)
                {
                    if (CurrentSelection == tile)
                    {
                        CurrentSelection = null;
                    }
                    else
                    {
                        CurrentSelection = tile;
                    }

                    Repaint();
                }
            }
        }

        GUI.color = Color.white;
    }

    public void OnGUI()
    {
        GUILayout.Label(position.width.ToString());
        if (GUILayout.Button("Refresh"))
        {
            Refresh();
        }

        const int space = 2;
        const int previewSize = 64;
        var width = (int)Mathf.Max((WindowWidth - 16) / (previewSize + space), 1);

        for (int index = 0; index < Tiles.Count; index += width)
        {
            EditorGUILayout.BeginHorizontal();
            var hor = Mathf.Min(Tiles.Count - index, width);
            for (var i = 0; i < hor; i++)
            {
                var assetIndex = index + i;
                var tile = Tiles[assetIndex];

                DrawTile(tile);
                GUILayout.Space(space);
            }

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(space);
        }
    }
}
