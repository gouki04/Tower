using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Tower
{
    public class Tile : MonoBehaviour
    {
        public int Row
        {
            get {
                return (int)Math.Floor(transform.localPosition.y);
            }
            set {
                transform.localPosition = new Vector3(transform.localPosition.x, value, transform.localPosition.z);
            }
        }

        public int Column
        {
            get {
                return (int)Math.Floor(transform.localPosition.x);
            }
            set {
                transform.localPosition = new Vector3(value, transform.localPosition.y, transform.localPosition.z);
            }
        }

        public bool Block = true;

        public bool NeedTrigger = false;

        public TileMap ParentTileMap
        {
            get {
                return transform.parent.GetComponent<TileMap>();
            }
        }

        public void DestroySelf()
        {
            ParentTileMap.DeleteTile(this);
        }

        public bool CheckTrigger(Player player)
        {
            var trigger_succeed = true;
            foreach (var com in m_ComponetList) {
                if (!com.CheckCanTrigger(player)) {
                    trigger_succeed = false;
                    break;
                }
            }

            if (!trigger_succeed) {
                foreach (var com in m_ComponetList) {
                    com.OnTriggerFailed(player);
                }
            }
            return trigger_succeed;
        }

        public IEnumerator TriggerRoutine(Player player)
        {
            foreach (var com in m_ComponetList) {
                var routine = com.OnBeforeTrigger(player);
                if (routine != null) {
                    yield return StartCoroutine(routine);
                }
            }

            foreach (var com in m_ComponetList) {
                var routine = com.OnTrigger(player);
                if (routine != null) {
                    yield return StartCoroutine(routine);
                }
            }

            foreach (var com in m_ComponetList) {
                var routine = com.OnAfterTrigger(player);
                if (routine != null) {
                    yield return StartCoroutine(routine);
                }
            }
        }

        #region component

        protected List<TileComponent> m_ComponetList = new List<TileComponent>();
        public void AddTileComponent(TileComponent com)
        {
            m_ComponetList.Add(com);
        }

        public void RemoveTileComponent(TileComponent com)
        {
            m_ComponetList.Remove(com);
        }

        #endregion
    }
}
