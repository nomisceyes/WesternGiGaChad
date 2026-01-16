using UnityEditor;
using UnityEngine;

public class GameFactory : IGameFactory
{  
    public readonly IAssetProvider Assets;

    public GameFactory(IAssetProvider assets)
    {
        Assets = assets;
    }

    public GameObject CreatePlayer(GameObject at) =>
       Assets.Instantiate(AssetPath.HeroPath, at.transform.position);
}