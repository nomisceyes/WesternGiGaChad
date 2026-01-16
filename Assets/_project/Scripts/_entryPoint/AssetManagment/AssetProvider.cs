using UnityEditor;
using UnityEngine;

public class AssetProvider : IAssetProvider
{
    public GameObject Instantiate(string path, Vector3 at)
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        return Object.Instantiate(prefab, at, Quaternion.identity);
    }
}