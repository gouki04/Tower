using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tower.Component
{
    [System.Serializable]
    public class GoodData
    {
        [SerializeField]
        public string Text;

        [SerializeField]
        public Attributes Cost = new Attributes();

        [SerializeField]
        public Attributes Get = new Attributes();

        public GoodData()
        {

        }

        public GoodData(string text, Attributes cost, Attributes get)
        {
            Text = text;
            Cost.Combine(cost);
            Get.Combine(get);
        }

        public GoodData(GoodData data)
        {
            Text = data.Text;
            Cost.Combine(data.Cost);
            Get.Combine(data.Get);
        }
    }

    public class Shop : TileComponent
    {
        public enum EState
        {
            Idle,
            Show,
            Finished
        }

        public string Text;
        public List<GoodData> GoodDataList = new List<GoodData>();

        protected EState m_State = EState.Idle;
        protected Player m_TriggerPlayer = null;

        public override IEnumerator OnTrigger(Player player)
        {
            m_TriggerPlayer = player;
            m_State = EState.Show;
            UIManager.Instance.ShopUI.ShowDialogue("商店老板", Text, GoodDataList, SelectItem);
            while (m_State != EState.Finished) {
                yield return 0;
            }
            m_TriggerPlayer = null;
        }

        public void SelectItem(int index)
        {
            if (index == -1) {
                m_State = EState.Finished;
            } else {
                var good = GoodDataList[index];

                if (m_TriggerPlayer.Attrs.Include(good.Cost)) {
                    m_TriggerPlayer.Attrs.Cost(good.Cost);
                    m_TriggerPlayer.Attrs.Combine(good.Get);

                    UIManager.Instance.TipsCanvasUI.Show(good.Text);
                }
            }
        }

        public void OnGUI()
        {
            //if (m_State == EState.Show) {
            //    GUILayout.Label(Text);
            //    foreach (var good in GoodDataList) {
            //        GUI.enabled = m_TriggerPlayer.Attrs.Include(good.Cost);
            //        if (GUILayout.Button(good.Text)) {
            //            m_TriggerPlayer.Attrs.Cost(good.Cost);
            //            m_TriggerPlayer.Attrs.Combine(good.Get);
            //        }
            //        GUI.enabled = true;
            //    }

            //    if (GUILayout.Button("离开")) {
            //        m_State = EState.Finished;
            //    }
            //}
        }
    }
}
