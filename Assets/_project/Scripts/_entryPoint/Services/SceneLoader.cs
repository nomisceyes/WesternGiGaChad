using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IService
{
    public string CurrentSceneName = null;
    public Action OnLoaded;

    public void Init()
    {
        CurrentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += (scene, sceneMode) => OnLoaded?.Invoke();
    }

    public void Load(string name) =>
        LoadScene(name);

    private void LoadScene(string name)
    {
        if (CurrentSceneName == null) return;
        SceneManager.LoadScene(CurrentSceneName);
        CurrentSceneName = name;
    }
}