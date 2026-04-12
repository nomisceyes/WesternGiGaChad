using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button StartButton;

    private void Awake()
    {
        StartButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        Global.SceneLoader.Load("Main");
    }
}