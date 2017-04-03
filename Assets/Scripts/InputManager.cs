using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tower
{
    public class InputManager : MonoBehaviour
    {
        public Logic Logic;

        protected void Update()
        {
            if (CheckMouseClick()) {
                var tile_pos = ScreenPosToTilePos(Input.mousePosition);
                if (tile_pos != TileIndex.invalid) {
                    Logic.TriggerTile(tile_pos);
                }
            }
        }

        protected void OnGUI()
        {
            var tile_pos = ScreenPosToTilePos(Input.mousePosition);
            if (tile_pos != TileIndex.invalid) {
                GUI.Label(new Rect(0, 0, 150, 20), tile_pos.ToString());
            } else {
                GUI.Label(new Rect(0, 0, 150, 20), "invaild");
            }
        }

        public TileIndex ScreenPosToTilePos(Vector3 pos)
        {
            var tile_pos = Logic.Floor.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(pos));
            tile_pos.y = (float)Math.Round(tile_pos.y);
            tile_pos.x = (float)Math.Round(tile_pos.x);

            if (tile_pos.y >= 0 && tile_pos.y < Logic.Floor.Row && tile_pos.x >= 0 && tile_pos.x < Logic.Floor.Column) {
                return new TileIndex((int)tile_pos.y, (int)tile_pos.x);
            } else {
                return TileIndex.invalid;
            }
        }

        #region mouse

        protected bool m_isMouseDown = false;
        protected float m_mouseDownTime = 0;
        protected Vector3 m_mouseDownPos = Vector3.zero;

        public float MouseClickMaxDeltaTime = 0.3f;
        public float MouseClickMaxDistanceInPixel = 10.0f;

        protected bool CheckMouseClick()
        {
            if (Input.GetMouseButtonDown(0)) {
                m_isMouseDown = true;
                m_mouseDownTime = Time.realtimeSinceStartup;
                m_mouseDownPos = Input.mousePosition;
            } else if (m_isMouseDown && Input.GetMouseButtonUp(0)) {
                var dt = Time.realtimeSinceStartup - m_mouseDownTime;
                if (dt > MouseClickMaxDeltaTime) {
                    return false;
                }

                var distance = Vector3.Distance(Input.mousePosition, m_mouseDownPos);
                if (distance > MouseClickMaxDistanceInPixel) {
                    return false;
                }
                return true;
            }

            return false;
        }

        #endregion
    }
}
