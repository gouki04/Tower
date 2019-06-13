using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tower
{
    public class ShopItemUI : MonoBehaviour
    {
        public UnityEngine.UI.Text ContentText;

        public void SetText(string text)
        {
            ContentText.text = text;
        }
    }
}
