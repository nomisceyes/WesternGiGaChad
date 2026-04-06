using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private List<Panel> _panels;

    private SceneLoader _sceneLoader; 
    
    private void Start()
    {
        _sceneLoader = new SceneLoader(this);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            foreach (Panel p in _panels)
            {
                p.gameObject.SetActive(false);
            }
        }
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