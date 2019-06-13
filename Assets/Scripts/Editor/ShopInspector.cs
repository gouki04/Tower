using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Tower.Editor
{
    [CustomEditor(typeof(Tower.Component.Shop))]
    public class ShopInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var shop = target as Tower.Component.Shop;
            if (shop != null) {
                foreach (var good in shop.GoodDataList) {
                    good.Text = EditorGUILayout.TextField(good.Text);

                    GUILayout.Label("Cost:");
                    if (Tower.Editor.Utility.AttributesField(good.Cost)) {
                        if (!Application.isPlaying) {
                            EditorUtility.SetDirty(shop); // this line doesn't work at unity5
                            EditorSceneManager.MarkAllScenesDirty();
                        }
                    }

                    GUILayout.Label("Get:");
                    if (Tower.Editor.Utility.AttributesField(good.Get)) {
                        if (!Application.isPlaying) {
                            EditorUtility.SetDirty(shop); // this line doesn't work at unity5
                            EditorSceneManager.MarkAllScenesDirty();
                        }
                    }

                    GUILayout.Space(3);
                }

                if (GUILayout.Button("Add Good")) {
                    if (shop.GoodDataList.Count > 0) {
                        shop.GoodDataList.Add(new Component.GoodData(shop.GoodDataList[shop.GoodDataList.Count - 1]));
                    } else {
                        shop.GoodDataList.Add(new Component.GoodData());
                    }
                }
            }
        }
    }
}
