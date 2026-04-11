using UnityEngine;

public static class ServiceLocator
{
    public static InputService InputService;
    public static SceneLoader SceneLoader;
    public static AudioManager AudioManager;
    public static Game Main;
    
    //public static VFXManager VFXManager;
}

[DefaultExecutionOrder(-9999)]
public static class Bootstrapper
{
    private static bool _isInitialized = false;
    private static GameObject _serviceHolder;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        if (_isInitialized) return;
        
        Res.InitAudio();
        //Res.InitVFX();
        
        _serviceHolder = new GameObject("---Services---");
        Object.DontDestroyOnLoad(_serviceHolder);

        ServiceLocator.InputService = CreateSimpleService<InputService>();
        ServiceLocator.SceneLoader = CreateSimpleService<SceneLoader>();
        ServiceLocator.AudioManager = CreateSimpleService<AudioManager>(); 
        ServiceLocator.Main = Object.FindFirstObjectByType<Game>();

        ServiceLocator.SceneLoader.OnLoaded = () =>
        {
            ServiceLocator.Main = Object.FindFirstObjectByType<Game>();
            Debug.Log(ServiceLocator.Main);
        };
    }

    private static T CreateSimpleService<T>() where T : Component, IService
    {
        GameObject gameObject = new GameObject(typeof(T).ToString()){transform = { parent = _serviceHolder.transform}};
        
        T component = gameObject.AddComponent<T>();
        component.Init();
        return gameObject.GetComponent<T>();
    }
}