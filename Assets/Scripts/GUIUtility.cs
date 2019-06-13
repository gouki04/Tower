using UnityEngine;

namespace Tower
{
    public class GUIUtility
    {
        public static void Sprite(Sprite spr)
        {
            GUILayout.Box(string.Empty, GUILayout.Width(spr.textureRect.width), GUILayout.Height(spr.textureRect.height));
            var rect = GUILayoutUtility.GetLastRect();
            GUI.DrawTextureWithTexCoords(rect, spr.texture,
                            new Rect(spr.textureRect.x / spr.texture.width, spr.textureRect.y / spr.texture.height, spr.textureRect.width / spr.texture.width, spr.textureRect.height / spr.texture.height));
        }
    }
}
