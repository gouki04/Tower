using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.TileMap))]
    public class TileMapInspector : UnityEditor.Editor
    {
        protected int mRow;
        protected int mColumn;
        protected bool mFoldOut = true;

        protected bool mCreateBkFoldOut = false;
        protected UnityEngine.Object mBkTile;

        protected bool mDrawTileFoldOut = true;
        protected Rect mLastRect;
        protected UnityEngine.GameObject mBrushTile;
        protected bool mInDragBrush = false;
        
        protected enum BrushType
        {
            Draw,
            Erase,
        }

        protected BrushType mBrushType = BrushType.Draw;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var tile_map = target as Tower.TileMap;
            if (tile_map != null) {
                EditorGUILayout.LabelField(string.Format("TileSize = (row = {0}, column = {1})", tile_map.Row, tile_map.Column));

                EditorGUILayout.BeginHorizontal();

                EditorGUIUtility.labelWidth = 30;
                mRow = EditorGUILayout.IntField("Row", mRow);
                EditorGUIUtility.labelWidth = 50;
                mColumn = EditorGUILayout.IntField("Column", mColumn);
                EditorGUIUtility.labelWidth = 0;

                if (GUILayout.Button("SetSize")) {
                    tile_map.SetSize(mRow, mColumn);
                }

                EditorGUILayout.EndHorizontal();

                mFoldOut = EditorGUILayout.Foldout(mFoldOut, "Tile Data");
                if (mFoldOut) {
                    EditorGUI.indentLevel++;
                    for (var r = tile_map.Row - 1; r >= 0; --r) {
                        GUILayout.BeginHorizontal();
                        for (var c = 0; c < tile_map.Column; ++c) {
                            var tile = tile_map.GetTileAt(r, c);
                            GUILayout.Label(tile == null ? "0" : "#");
                        }
                        GUILayout.EndHorizontal();
                    }
                    EditorGUI.indentLevel--;
                }

                if (GUILayout.Button("UpdateChild")) {
                    tile_map.UpdateChild();
                }

                mCreateBkFoldOut = EditorGUILayout.Foldout(mCreateBkFoldOut, "Create Background");
                if (mCreateBkFoldOut) {
                    mBkTile = EditorGUILayout.ObjectField(mBkTile, typeof(GameObject), false);
                    if (mBkTile != null && GUILayout.Button("CreateBackground")) {
                        for (var r = 0; r < tile_map.Row; ++r) {
                            for (var c = 0; c < tile_map.Column; ++c) {
                                var tile_go = PrefabUtility.InstantiatePrefab(mBkTile) as GameObject;
                                tile_go.name = mBkTile.name;
                                tile_go.transform.parent = tile_map.transform;

                                var tile = tile_go.GetComponent<Tower.Tile>();
                                tile.Row = r;
                                tile.Column = c;
                            }
                        }
                    }
                }

                mDrawTileFoldOut = EditorGUILayout.Foldout(mDrawTileFoldOut, "Draw Tile");
                if (mDrawTileFoldOut) {
                    mBrushTile = EditorGUILayout.ObjectField("Brush", mBrushTile, typeof(GameObject), false) as GameObject;
                    if (Event.current.type == EventType.Repaint) {
                        mLastRect = GUILayoutUtility.GetLastRect();
                    }
                    GUILayout.Box(string.Empty, GUILayout.Width(tile_map.Column * 16), GUILayout.Height(tile_map.Row * 16));
                    for (var r = tile_map.Row - 1; r >= 0; --r) {
                        for (var c = 0; c < tile_map.Column; ++c) {
                            var rect = new Rect(mLastRect.x + c * 16, mLastRect.yMax + 5 + (tile_map.Row - 1 - r) * 16, 16, 16);
                            var old_tile = tile_map.GetTileAt(r, c);
                            if (old_tile != null) {
                                var spr = old_tile.GetComponent<SpriteRenderer>().sprite;
                                GUI.DrawTextureWithTexCoords(rect, spr.texture,
                                    new Rect(spr.textureRect.x / spr.texture.width, spr.textureRect.y / spr.texture.height, spr.textureRect.width / spr.texture.width, spr.textureRect.height / spr.texture.height));
                            } else {
                                GUI.Box(rect, string.Empty);
                            }

                            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition)) {
                                mInDragBrush = true;
                                if (Event.current.button == 0) {
                                    mBrushType = BrushType.Draw;
                                } else {
                                    mBrushType = BrushType.Erase;
                                }
                            } else if (Event.current.type == EventType.MouseUp) {
                                mInDragBrush = false;
                            }

                            if (mInDragBrush && rect.Contains(Event.current.mousePosition)) {
                                tile_map.DeleteTile(old_tile, false);
                                if (mBrushType == BrushType.Draw) {
                                    if (mBrushTile != null) {
                                        var tile_go = PrefabUtility.InstantiatePrefab(mBrushTile) as GameObject;
                                        tile_go.name = mBrushTile.name;
                                        tile_go.transform.parent = tile_map.transform;

                                        var tile = tile_go.GetComponent<Tower.Tile>();
                                        tile.Row = r;
                                        tile.Column = c;

                                        tile_map.SetTile(tile, true);
                                    }
                                }

                                Repaint();
                            }
                        }
                    }
                }
            }
        }
    }
}
