using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tower
{
    [System.Serializable]
    public struct DialogueData
    {
        [SerializeField]
        public Texture2D Portait;

        [SerializeField]
        public string Name;

        [SerializeField]
        public string Text;
    }

    public class Dialogue : TileComponent
    {
        public delegate bool Condition(Player player);

        public Condition Conditions;

        public List<DialogueData> BeforeTriggerDialogue = null;
        public List<DialogueData> TriggerDialogue = null;
        public List<DialogueData> AfterTriggerDialogue = null;

        public override bool CheckCanTrigger(Player player)
        {
            if (Conditions != null) {
                return Conditions(player);
            }

            return true;
        }

        public override IEnumerator OnBeforeTrigger(Player player)
        {
            if (BeforeTriggerDialogue != null) {

            }
            return null;
        }

        public override IEnumerator OnTrigger(Player player)
        {
            return null;
        }

        public override IEnumerator OnAfterTrigger(Player player)
        {
            return null;
        }
    }
}
