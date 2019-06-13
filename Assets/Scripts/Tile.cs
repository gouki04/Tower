using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Tower
{
    public enum ETriggerType
    {
        BeforeTrigger = 0,
        Trigger,
        AfterTrigger
    }

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

        protected bool m_Removed = false;
        public void RemoveSelfInRoutine()
        {
            m_Removed = true;
        }

        public bool CheckTrigger(Player player)
        {
            var trigger_succeed = true;
            foreach (var com in m_ComponetList) {
                if (com.CheckCanTrigger(player)) {
                    com.Triggered = true;
                    trigger_succeed = true;
                } else {
                    com.OnTriggerFailed(player);
                    com.Triggered = false;
                }
            }
            
            return trigger_succeed;
        }

        public IEnumerator TriggerRoutine(Player player)
        {
            foreach (var com in m_ComponetList) {
                if (com.Triggered) {
                    var routine = com.OnBeforeTrigger(player);
                    if (routine != null) {
                        yield return StartCoroutine(routine);
                    }
                }
            }

            CheckAllComponentRemoved();

            foreach (var com in m_ComponetList) {
                if (com.Triggered) {
                    var routine = com.OnTrigger(player);
                    if (routine != null) {
                        yield return StartCoroutine(routine);
                    }
                }
            }

            CheckAllComponentRemoved();

            foreach (var com in m_ComponetList) {
                if (com.Triggered) {
                    var routine = com.OnAfterTrigger(player);
                    if (routine != null) {
                        yield return StartCoroutine(routine);
                    }
                }
            }

            CheckAllComponentRemoved();

            if (m_Removed) {
                ParentTileMap.DeleteTile(this);
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

        protected void CheckAllComponentRemoved()
        {
            for (var i = m_ComponetList.Count - 1; i >= 0; --i) {
                var com = m_ComponetList[i];
                if (com.Removed) {
                    Destroy(com);
                }
            }
        }

        #endregion
    }
}
