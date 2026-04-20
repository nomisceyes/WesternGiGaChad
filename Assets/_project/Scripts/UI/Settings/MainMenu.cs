using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button StartButton;

    private void Awake()
    {
        //StartButton.onClick.AddListener(StartGame);
    }

    private void Start()
    {
        Global.AudioManager.PlayMusic(Res.Audio.MainMenuMusic);
    }

    private async UniTaskVoid StartGame()
    {
        await Global.SceneLoader.Load("Main");
    }
}