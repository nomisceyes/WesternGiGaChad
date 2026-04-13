using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ResoultsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _victoryText;
    [SerializeField] private TextMeshProUGUI _prepareText;
    [SerializeField] private TextMeshProUGUI _bossText;

    private bool _isPreparing = false;

    public async UniTask PrepareToBossFight()
    {
        _bossText.gameObject.SetActive(true);

        await UniTask.Delay(1000);

        _bossText.gameObject.SetActive(false);
    }

    public async UniTask PrepareToNextWave(CancellationToken token = default)
    {
        if (_isPreparing) return;

        _isPreparing = true;

        try
        {
            _victoryText.gameObject.SetActive(true);
            await UniTask.Delay(3000, cancellationToken: token);
            _victoryText.gameObject.SetActive(false);

            await UniTask.Delay(1000, cancellationToken: token);

            _prepareText.gameObject.SetActive(true);
            await UniTask.Delay(1000, cancellationToken: token);
            _prepareText.gameObject.SetActive(false);

            await UniTask.Delay(500, cancellationToken: token);
        }
        finally
        {
            _isPreparing = false;
        }
    }
}