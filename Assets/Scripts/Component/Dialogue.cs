using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [System.Serializable]
    public struct DialogueData
    {
        [SerializeField]
        public Sprite Portait;

        [SerializeField]
        public string Name;

        [SerializeField]
        public string Text;

        public DialogueData(Sprite portait, string name, string text)
        {
            Portait = portait;
            Name = name;
            Text = text;
        }
    }

    public enum DialogueShowMode
    {
        Once = 0,
        Loop,
    }

    public class Dialogue : TileComponent
    {
        public enum EState
        {
            Idle,
            Show,
            Finished
        }

        public DialogueShowMode ShowMode = DialogueShowMode.Once;
        public ETriggerType TriggerType = ETriggerType.Trigger;
        public List<DialogueData> DialogueDataList = new List<DialogueData>();

        protected EState m_State = EState.Idle;
        protected int m_CurrentDialogueIdx = 0;

        public override bool CheckCanTrigger(Player player)
        {
            return true;
        }

        public override IEnumerator OnBeforeTrigger(Player player)
        {
            if (TriggerType == ETriggerType.BeforeTrigger) {
                ShowDialogue(DialogueDataList);
                OnDialogueStart(player);
                while (m_State != EState.Finished) {
                    yield return 0;
                }
            } else {
                yield break;
            }
        }

        public override IEnumerator OnTrigger(Player player)
        {
            if (TriggerType == ETriggerType.Trigger) {
                ShowDialogue(DialogueDataList);
                OnDialogueStart(player);
                while (m_State != EState.Finished) {
                    yield return 0;
                }
            }
            else {
                yield break;
            }
        }

        public override IEnumerator OnAfterTrigger(Player player)
        {
            if (TriggerType == ETriggerType.AfterTrigger) {
                ShowDialogue(DialogueDataList);
                OnDialogueStart(player);
                while (m_State != EState.Finished) {
                    yield return 0;
                }
            }

            OnDialogueFinished(player);

            if (ShowMode == DialogueShowMode.Once) {
                RemoveSelfInTrigger();
            }
        }

        protected void ShowDialogue(List<DialogueData> dialogue)
        {
            m_State = EState.Show;
            m_CurrentDialogueIdx = 0;

            UIManager.Instance.DialogueUI.ShowDialogue(dialogue, FinishDialogue);
        }

        protected void FinishDialogue()
        {
            m_State = EState.Finished;
            m_CurrentDialogueIdx = 0;
        }

        protected virtual void OnDialogueStart(Player player)
        {

        }

        protected virtual void OnDialogueFinished(Player player)
        {

        }

        public void OnGUI()
        {
            //if (m_State == EState.Show) {
            //    var dialogue = DialogueDataList[m_CurrentDialogueIdx];
            //    if (dialogue.Portait != null) {
            //        Tower.GUIUtility.Sprite(dialogue.Portait);
            //    }
            //    GUILayout.Label(dialogue.Name);
            //    GUILayout.Label(dialogue.Text);

            //    if (GUILayout.Button("Next")) {
            //        ++m_CurrentDialogueIdx;
            //        if (m_CurrentDialogueIdx >= DialogueDataList.Count) {
            //            FinishDialogue();
            //        }
            //    }
            //}
        }
    }
}
