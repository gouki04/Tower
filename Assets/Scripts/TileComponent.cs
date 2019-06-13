using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tower
{
    [RequireComponent(typeof(Tile))]
    public class TileComponent : MonoBehaviour
    {
        public Tile Owner = null;
        public bool Removed = false;

        [HideInInspector]
        public bool Triggered = false;

        public void Start()
        {
            Owner = GetComponent<Tile>();
            Owner.AddTileComponent(this);

            OnEnter();
        }

        public void OnDestroy()
        {
            OnExit();

            Owner.RemoveTileComponent(this);
            Owner = null;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void RemoveSelfInTrigger()
        {
            Removed = true;
        }

        public virtual bool CheckCanTrigger(Player player)
        {
            return true;
        }

        public virtual void OnTriggerFailed(Player player)
        {

        }

        public virtual IEnumerator OnBeforeTrigger(Player player)
        {
            return null;
        }

        public virtual IEnumerator OnTrigger(Player player)
        {
            return null;
        }

        public virtual IEnumerator OnAfterTrigger(Player player)
        {
            return null;
        }
    }
}
