using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button StartButton;

    private void Awake()
    {
        StartButton.onClick.AddListener(StartGame);
    }

    private void Start()
    {
        Global.AudioManager.PlayMusic(Res.Audio.MainMenuMusic);
    }

    public void StartGame()
    {
        Global.SceneLoader.Load("Main");
    }
}