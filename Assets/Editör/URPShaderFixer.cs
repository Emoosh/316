using UnityEngine;
using UnityEditor;

public class URPShaderFixer
{
    [MenuItem("Tools/Convert All Standard Materials to URP")]
    public static void ConvertMaterialsToURP()
    {
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat.shader.name == "Standard")
            {
                mat.shader = Shader.Find("Universal Render Pipeline/Lit");
                Debug.Log($"✅ Converted: {path}");
                EditorUtility.SetDirty(mat);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("🎉 All Standard materials converted to URP/Lit.");
    }
}
