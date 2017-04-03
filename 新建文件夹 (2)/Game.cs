using UnityEngine;
using System.Collections;
using Rotorz.Tile;
using System.Collections.Generic;

public class Game : MonoBehaviour
{
    public OldSchoolRPGDialogueGUI DialoguerSkin;

    protected void Start ()
    {
        Transport(Floor, PlayerPos);
        Dialoguer.Initialize();
        DialoguerSkin.addDialoguerEvents();
    }

    protected void Update ()
    {
        if (CheckMouseClick()) {
            var tile_index = ScreenPosToTileIndex(Input.mousePosition);
            Tile = Floor.GetTileOrNull(tile_index);

            if (Tile != null && CheckTileIndexTriggerable(tile_index)) {
                var obj = Tile.gameObject.GetComponent<Tower.Obj>();
                if (obj != null) {
                    obj.Pos = tile_index;

                    if (obj.Trigger(this)) {
                        Floor.EraseTile(tile_index);
                        UpdateTileMap();
                    }
                }
            }
        }
	}

    protected delegate void TileMapSearcher(TileIndex p);
    public void UpdateTileMap()
    {
        for (var r = 0; r < Floor.RowCount; ++r) {
            for (var c = 0; c < Floor.ColumnCount; ++c) {
                TileMap[r, c] = 0;

                var tile = Floor.GetTileOrNull(r, c);
                if (tile != null) {
                    var spr = tile.gameObject.GetComponent<SpriteRenderer>();
                    if (spr != null) {
                        spr.color = Color.gray;
                    }
                }
            }
        }

        var open_list = new Queue<TileIndex>();
        open_list.Enqueue(PlayerPos);

        TileMapSearcher search_helper = delegate (TileIndex p) {
            if (p.row < 0 || p.row >= Floor.RowCount || p.column < 0 || p.column >= Floor.ColumnCount) {
                return;
            }

            if (TileMap[p.row, p.column] != 0) {
                return;
            }

            var tile = Floor.GetTileOrNull(p);
            if (tile != null) {
                TileMap[p.row, p.column] = 1;

                var spr = tile.gameObject.GetComponent<SpriteRenderer>();
                if (spr != null) {
                    spr.color = Color.white;
                }
            }
            else {
                open_list.Enqueue(p);
            }
        };

        while (open_list.Count > 0) {
            var p = open_list.Dequeue();
            TileMap[p.row, p.column] = 1;

            var tile = Floor.GetTileOrNull(p);
            if (tile != null) {
                var spr = tile.gameObject.GetComponent<SpriteRenderer>();
                if (spr != null) {
                    spr.color = Color.white;
                }
            }

            search_helper(new TileIndex(p.row, p.column - 1));
            search_helper(new TileIndex(p.row, p.column + 1));
            search_helper(new TileIndex(p.row - 1, p.column));
            search_helper(new TileIndex(p.row + 1, p.column));
        }
    }

    public bool CheckTileIndexTriggerable(TileIndex pos)
    {
        if (pos == TileIndex.invalid) {
            return false;
        }

        return TileMap[pos.row, pos.column] == 1;
    }

    public void Transport(TileSystem floor, TileIndex pos)
    {
        if (Floor != null) {
            Floor.gameObject.SetActive(false);
        }

        Floor = floor;
        PlayerPos = pos;

        if (TileMap == null || TileMap.GetLength(0) != Floor.RowCount || TileMap.GetLength(1) != Floor.ColumnCount) {
            TileMap = new int[floor.RowCount, floor.ColumnCount];
        }
        UpdateTileMap();

        Floor.gameObject.SetActive(true);
    }

    public void MoveTile(int from_row, int from_col, int row, int col)
    {
        var tile = Floor.GetTileOrNull(from_row, from_col);
        Floor.SetTile(row, col, tile);
        //Floor.EraseTile(from_row, from_col);
    }

    public void DeleteTile(GameObject obj)
    {
        //var floor = obj.GetComponentInParent<TileSystem>();
        //if (floor != null) {
        //    floor.EraseTile(obj.transform.);
        //    UpdateTileMap();
        //}
    }

    protected void OnGUI()
    {
        var tile_index = ScreenPosToTileIndex(Input.mousePosition);
        if (tile_index != TileIndex.invalid) {
            GUI.Label(new Rect(0, 0, 60, 20), string.Format("({0}, {1})", tile_index.row, tile_index.column));
        }
        else {
            GUI.Label(new Rect(0, 0, 60, 20), "invaild");
        }
    }

    #region player

    public int HP = 0;
    public int ATK = 0;
    public int DEF = 0;
    public int Gold = 0;
    public int Exp = 0;

    public Dictionary<uint, uint> Switches = new Dictionary<uint, uint>();

    public Dictionary<Tower.EKey, uint> Keys = new Dictionary<Tower.EKey, uint>()
    {
        { Tower.EKey.Yellow, 0 },
        { Tower.EKey.Blue, 0 },
        { Tower.EKey.Red, 0 },
    };

    public TileIndex PlayerPos = TileIndex.invalid;
    public int[,] TileMap = null;

    public void AddKey(Tower.EKey key_type)
    {
        Keys[key_type]++;
    }

    public bool UseKey(Tower.EKey key_type)
    {
        var cnt = Keys[key_type];
        if (cnt <= 0) {
            return false;
        }

        Keys[key_type] = cnt - 1;
        return true;
    }

    public void AddHp(int hp)
    {
        var new_hp = HP + hp;
        if (new_hp <= 0) {
            // TODO die
            HP = 0;
        }
        else {
            HP = new_hp;
        }
    }

    public void AddAtk(int atk)
    {
        ATK += atk;
    }

    public void AddDef(int def)
    {
        DEF += def;
    }

    public void AddGold(int gold)
    {
       Gold += gold;
    }

    public void AddExp(int exp)
    {
        Exp += exp;
    }

    #endregion

    #region tilesystem

    public TileSystem Floor;
    public TileData Tile;

    public TileIndex ScreenPosToTileIndex(Vector3 pos)
    {
        var ray = Camera.main.ScreenPointToRay(pos);
        return Floor.ClosestTileIndexFromRay(ray);
    }

    public TileData GetTileByScreenPos(Vector3 pos)
    {
        var tile_index = ScreenPosToTileIndex(pos);
        return Floor.GetTileOrNull(tile_index);
    }

    #endregion

    #region mouse

    protected bool _isMouseDown = false;
    protected float _mouseDownTime = 0;
    protected Vector3 _mouseDownPos = Vector3.zero;

    public float MouseClickMaxDeltaTime = 0.3f;
    public float MouseClickMaxDistanceInPixel = 10.0f;

    protected bool CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) {
            _isMouseDown = true;
            _mouseDownTime = Time.realtimeSinceStartup;
            _mouseDownPos = Input.mousePosition;
        }
        else if (_isMouseDown && Input.GetMouseButtonUp(0)) {
            var dt = Time.realtimeSinceStartup - _mouseDownTime;
            if (dt > MouseClickMaxDeltaTime) {
                return false;
            }

            var distance = Vector3.Distance(Input.mousePosition, _mouseDownPos);
            if (distance > MouseClickMaxDistanceInPixel) {
                return false;
            }
            return true;
        }

        return false;
    }

    #endregion
}
