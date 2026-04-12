using DG.Tweening;
using UnityEngine;

public static class Global
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
    private static void OnBeforeSceneLoad()
    {
        if (_isInitialized) return;

        DOTween.Init();
        Res.InitAudio();
        //Res.InitVFX();
        
        _serviceHolder = new GameObject("---Services---");
        Object.DontDestroyOnLoad(_serviceHolder);

        Global.InputService = CreateSimpleService<InputService>();
        Global.SceneLoader = CreateSimpleService<SceneLoader>();
        Global.AudioManager = CreateSimpleService<AudioManager>(); 
    }

    private static T CreateSimpleService<T>() where T : Component, IService
    {
        GameObject gameObject = new GameObject(typeof(T).ToString()){transform = { parent = _serviceHolder.transform}};
        
        T component = gameObject.AddComponent<T>();
        component.Init();
        return gameObject.GetComponent<T>();
    }
}