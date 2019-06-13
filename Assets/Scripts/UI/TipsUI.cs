using UnityEngine;

namespace Tower
{
    public class TipsUI : MonoBehaviour
    {
        public UnityEngine.UI.Text ContentText;

        public void Show(string text)
        {
            UpdateContent(text);

            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        protected void UpdateContent(string text)
        {
            ContentText.text = text;
        }
    }
}
