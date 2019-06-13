using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tower
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance = null;
        public void Start()
        {
            Instance = this;
        }

        public DialogueUI DialogueUI;
        public ShopUI ShopUI;
        public TipsUI TipsCanvasUI;
    }
}
