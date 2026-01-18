using UnityEngine;

public class ServiceInstaller : MonoBehaviour
{
    private IInputService _inputService;
    private IAssetProvider _assetProvider;

    private void Awake()
    {
        Debug.Log("<color=#0CFF00>Service instantiated!</color>");
        
        // ServiceLocator.AddService<IAssetProvider>(_assetProvider);
        // ServiceLocator.AddService<IInputService>(_inputService);
    }
}