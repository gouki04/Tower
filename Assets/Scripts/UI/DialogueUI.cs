using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tower
{
    public class DialogueUI : MonoBehaviour
    {
        public UnityEngine.UI.Text NameText;
        public UnityEngine.UI.Text ContentText;

        protected List<Component.DialogueData> m_DialogueDataList;
        protected int m_CurrentDialogueIdx = 0;

        public delegate void FinishCallBack();
        protected FinishCallBack m_Callback;

        public void ShowDialogue(List<Component.DialogueData> dialogue, FinishCallBack callback)
        {
            m_DialogueDataList = dialogue;
            m_CurrentDialogueIdx = 0;
            m_Callback = callback;
            UpdateDialogue();

            gameObject.SetActive(true);
        }

        public void FinishDialogue()
        {
            gameObject.SetActive(false);

            m_DialogueDataList = null;
            m_CurrentDialogueIdx = 0;

            if (m_Callback != null) {
                m_Callback();
                m_Callback = null;
            }
        }

        protected void UpdateDialogue()
        {
            var dialogue = m_DialogueDataList[m_CurrentDialogueIdx];
            NameText.text = dialogue.Name;
            ContentText.text = dialogue.Text;
        }

        public void ClickDialogue()
        {
            ++m_CurrentDialogueIdx;

            if (m_CurrentDialogueIdx >= m_DialogueDataList.Count) {
                FinishDialogue();
            } else {
                UpdateDialogue();
            }
        }
    }
}
