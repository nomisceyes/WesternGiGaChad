using UnityEngine;

public class ServiceInstaller : MonoBehaviour
{
    private IInputService _inputService;
    private IAssetProvider _assetProvider;

    private void Awake()
    {
        ServiceLocator.AddService<IAssetProvider>(new AssetProvider());
        ServiceLocator.AddService<IInputService>(new InputService());
    }
}