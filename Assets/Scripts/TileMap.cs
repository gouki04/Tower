using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tower
{
    public class TileMap : MonoBehaviour
    {
        public int Row
        {
            get {
                return mRow;
            }
        }

        public int Column
        {
            get {
                return mColumn;
            }
        }

        public int Floor = 0;

        [SerializeField]
        [HideInInspector]
        protected int mRow = 0;

        [SerializeField]
        [HideInInspector]
        protected int mColumn = 0;

        [SerializeField]
        protected Tile[,] mTiles = null;

        protected List<Tile> m_DeletedTileList = new List<Tile>();

        public void SetSize(int row, int column)
        {
            if (row != Row || column != Column || mTiles == null) {
                mRow = row;
                mColumn = column;

                var tiles = new Tile[row, column];

                if (mTiles != null) {
                    // copy tiles from mTiles to tiles
                    for (var r = 0; r < mTiles.GetLength(0); ++r) {
                        if (r < tiles.GetLength(0)) {
                            for (var c = 0; c < mTiles.GetLength(1); ++c) {
                                if (c < tiles.GetLength(1)) {
                                    tiles[r, c] = mTiles[r, c];
                                }
                            }
                        }
                    }
                }

                mTiles = tiles;
            }
        }

        public void ClearAllTiles()
        {
            if (mTiles == null) {
                return;
            }

            for (var r = 0; r < mTiles.GetLength(0); ++r) {
                for (var c = 0; c < mTiles.GetLength(1); ++c) {
                    mTiles[r, c] = null;
                }
            }
        }

        public bool SetTile(Tile tile, bool overwrite = false)
        {
            if (mTiles == null) {
                return false;
            }

            if (tile.Row < 0 || tile.Column < 0 || tile.Row >= Row || tile.Column >= Column) {
                return false;
            }

            if (mTiles[tile.Row, tile.Column] != null && !overwrite) {
                return false;
            }

            mTiles[tile.Row, tile.Column] = tile;
            return true;
        }

        public void DeleteTile(Tile tile, bool delay_delete = true)
        {
            if (mTiles == null || tile == null) {
                return;
            }

            if (tile.Row < 0 || tile.Column < 0 || tile.Row >= Row || tile.Column >= Column) {
                return;
            }

            mTiles[tile.Row, tile.Column] = null;

            if (delay_delete) {
                m_DeletedTileList.Add(tile);
            } else {
                DestroyImmediate(tile.gameObject);
            }
        }

        public void DeleteTileAt(int r, int c, bool delay_delete = true)
        {
            DeleteTile(GetTileAt(r, c), delay_delete);
        }

        public Tile GetTileAt(TileIndex idx)
        {
            return GetTileAt(idx.Row, idx.Column);
        }

        public Tile GetTileAt(int r, int c)
        {
            if (mTiles == null) {
                return null;
            }

            if (r < 0 || c < 0 || r >= Row || c >= Column) {
                return null;
            }

            return mTiles[r, c];
        }

        public void UpdateChild()
        {
            ClearAllTiles();

            foreach (var tile in GetComponentsInChildren<Tower.Tile>()) {
                SetTile(tile);
            }
        }

        public void Start()
        {
            SetSize(Row, Column);
            UpdateChild();
        }

        public void OnValidate()
        {
            SetSize(Row, Column);
            UpdateChild();
        }

        public void Update()
        {
            if (m_DeletedTileList.Count > 0) {
                foreach (var tile in m_DeletedTileList) {
                    Destroy(tile.gameObject);
                }

                m_DeletedTileList.Clear();
            }
        }
    }
}
