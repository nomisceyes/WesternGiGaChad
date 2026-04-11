using UnityEngine;

public class MainMenu : MonoBehaviour
{

    // [Inject]
    // public void Construct(ISceneLoader sceneLoader)
    // {
    //     _sceneLoader = sceneLoader;
    // }
    
    public void StartGame()
    {
        ServiceLocator.SceneLoader.Load("Main");
        //_sceneLoader.Load("Main");
    }
}