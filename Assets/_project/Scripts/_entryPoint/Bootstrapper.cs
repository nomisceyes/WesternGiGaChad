using UnityEngine;

public static class ServiceLocator
{
    public static InputService InputService;
    public static SceneLoader SceneLoader;
    public static AudioManager AudioManager;
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
        
        _serviceHolder = new GameObject("---Services---");
        Object.DontDestroyOnLoad(_serviceHolder);
        
        ServiceLocator.InputService = CreateSimpleService<InputService>();
        ServiceLocator.SceneLoader = CreateSimpleService<SceneLoader>();
        ServiceLocator.AudioManager = CreateSimpleService<AudioManager>();
    }

    private static T CreateSimpleService<T>() where T : Component, IService
    {
        GameObject g = new GameObject(typeof(T).ToString());
        
        g.transform.parent = _serviceHolder.transform;
        T t = g.AddComponent<T>();
        t.Init();
        return g.GetComponent<T>();
    }
}