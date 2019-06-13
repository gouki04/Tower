using UnityEngine;

namespace Tower
{
    public class HUDUI : MonoBehaviour
    {
        public UnityEngine.UI.Text LvText;
        public UnityEngine.UI.Text HpText;
        public UnityEngine.UI.Text AtkText;
        public UnityEngine.UI.Text DefText;
        public UnityEngine.UI.Text GoldText;
        public UnityEngine.UI.Text ExpText;
        public UnityEngine.UI.Text KeyYellowText;
        public UnityEngine.UI.Text KeyBlueText;
        public UnityEngine.UI.Text KeyRedText;

        public Player Player;

        public void Update()
        {
            var player = Player;
            LvText.text = player.Attrs.Lv.ToString();
            HpText.text = player.Attrs.Hp.ToString();
            AtkText.text = player.Attrs.Atk.ToString();
            DefText.text = player.Attrs.Def.ToString();
            GoldText.text = player.Attrs.Gold.ToString();
            ExpText.text = player.Attrs.Exp.ToString();
            KeyYellowText.text = player.Attrs.Key_Yellow.ToString();
            KeyBlueText.text = player.Attrs.Key_Blue.ToString();
            KeyRedText.text = player.Attrs.Key_Red.ToString();
        }
    }
}
