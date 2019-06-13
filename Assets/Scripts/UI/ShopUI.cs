using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    public class ShopUI : MonoBehaviour
    {
        public UnityEngine.UI.Text NameText;
        public UnityEngine.UI.Text ContentText;
        public ShopItemUI ShopItemUIPrefab;

        protected List<ShopItemUI> m_ShopItemUIList = new List<ShopItemUI>();
        protected List<Component.GoodData> m_GoodDataList;

        public delegate void SelectCallBack(int index);
        protected SelectCallBack m_Callback;

        public void ShowDialogue(string name, string text, List<Component.GoodData> goodList, SelectCallBack callback)
        {
            m_GoodDataList = goodList;
            m_Callback = callback;

            UpdateDialogue(name, text);

            gameObject.SetActive(true);
        }

        public void FinishDialogue()
        {
            gameObject.SetActive(false);

            if (m_Callback != null) {
                m_Callback(-1);
                m_Callback = null;
            }
        }

        protected void UpdateDialogue(string name, string text)
        {
            NameText.text = name;
            ContentText.text = text;

            foreach (var item in m_ShopItemUIList) {
                Destroy(item.gameObject);
            }
            m_ShopItemUIList.Clear();

            ShopItemUI itemUI = null;
            foreach (var good in m_GoodDataList) {
                itemUI = Instantiate(ShopItemUIPrefab);
                itemUI.transform.parent = ShopItemUIPrefab.transform.parent;
                itemUI.transform.localScale = Vector3.one;
                itemUI.SetText(good.Text);
                itemUI.gameObject.SetActive(true);

                m_ShopItemUIList.Add(itemUI);
            }

            itemUI = Instantiate(ShopItemUIPrefab);
            itemUI.transform.parent = ShopItemUIPrefab.transform.parent;
            itemUI.transform.localScale = Vector3.one;
            itemUI.SetText("离开");
            itemUI.gameObject.SetActive(true);

            m_ShopItemUIList.Add(itemUI);
        }

        public void ClickShopItem(ShopItemUI itemUI)
        {
            var index = m_ShopItemUIList.IndexOf(itemUI);
            if (index >= m_GoodDataList.Count) {
                FinishDialogue();
            } else {
                if (m_Callback != null) {
                    m_Callback(index);
                }
            }
        }
    }
}
