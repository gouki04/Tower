using System.Collections;
using UnityEngine;

namespace Tower
{
    public class Logic : MonoBehaviour
    {
        public static Logic Instance = null;
        public void Start()
        {
            Instance = this;
        }

        public TileMap Floor = null;
        public Player Player = null;
        public Tile TriggeringTile = null;

        public void Transport(TileMap floor, int row, int column)
        {
            if (Floor != null) {
                Floor.gameObject.SetActive(false);
            }

            Floor = floor;
            Floor.gameObject.SetActive(true);
        }

        public void TriggerTile(TileIndex tile_pos)
        {
            if (TriggeringTile != null) {
                return;
            }

            var tile = Floor.GetTileAt(tile_pos);
            if (tile != null) {
                if (tile.CheckTrigger(Player)) {
                    StartCoroutine(TriggerTileRoutine(tile));
                }
            }
        }

        protected IEnumerator TriggerTileRoutine(Tile tile)
        {
            TriggeringTile = tile;
            yield return StartCoroutine(tile.TriggerRoutine(Player));
            TriggeringTile = null;
        }
    }
}
