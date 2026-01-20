using UnityEngine;

public class Game : MonoBehaviour
{
    private IAssetProvider _assetProvider;
    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    private void Update()
    {
        _inputService?.Update();
    }
}