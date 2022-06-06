using DarkestDungeon.Configs;
using UnityEditor;
using UnityEngine;

public class MenuItems
{
    [MenuItem("Assets/Create/DarkestDungeon/CharacterConfig")]
    static void CreateAsset()
    {
        var asset = ScriptableObject.CreateInstance<CharacterConfig>();

        AssetDatabase.CreateAsset(asset, "Assets/Configs/CharacterConfig.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
