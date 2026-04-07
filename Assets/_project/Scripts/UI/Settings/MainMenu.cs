using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour, ICoroutineRunner
{
    private SceneLoader _sceneLoader; 
    
    private void Start()
    {
        _sceneLoader = new SceneLoader(this);
    }
    
    public void StartGame()
    {
        _sceneLoader.Load("Main");
    }

    public Coroutine StartRoutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }
}