using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour, IService
{
    public string CurrentSceneName = null;
    public Action OnLoaded;

    private GameObject _curtainCanvas;
    private Image _curtainImage;
    private Text _curtainText;

    public void Init()
    {
        CreateCurtain();
        
        CurrentSceneName = SceneManager.GetActiveScene().name;
    }

    public async UniTask Load(string name, float fadeDuration = 0.5f)
    {
        await Fade(2);
        LoadScene(name);
        await UnFade(2);
    }

    private async UniTask Fade(float duration)
    {
        if (_curtainCanvas == null)
        {
            await UniTask.Yield();
            return;
        }
        
        _curtainCanvas.SetActive(true);
        
        await DOTween.Sequence()
            .Join(_curtainImage.DOFade(1, duration))
            .Join(_curtainText.DOFade(1, duration))
            .AsyncWaitForCompletion()
            .AsUniTask();
    }

    private async UniTask UnFade(float duration)
    {
        if (_curtainCanvas == null)
        {
            await UniTask.Yield();
            return;
        }

        await DOTween.Sequence()
            .Join(_curtainImage.DOFade(0, duration))
            .Join(_curtainText.DOFade(0, duration))
            .OnComplete(()=> _curtainCanvas.SetActive(false))
            .AsyncWaitForCompletion()
            .AsUniTask();
    }

    private void CreateCurtain()
    {
        GameObject curtainPregab = Resources.Load<GameObject>("Curtain");

        if (curtainPregab != null)
        {
            _curtainCanvas = Instantiate(curtainPregab);
            _curtainImage = _curtainCanvas.GetComponentInChildren<Image>();
            _curtainText = _curtainImage.GetComponentInChildren<Text>();
            DontDestroyOnLoad(_curtainCanvas);
            _curtainCanvas.SetActive(false);
        }

        _ = UnFade(0.5f);
    }

    private void LoadScene(string name)
    {
        if (CurrentSceneName == null) return;

        CurrentSceneName = name;
        SceneManager.LoadScene(CurrentSceneName);

        Debug.Log("Scene: " + CurrentSceneName + " Loaded");
    }
}