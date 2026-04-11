using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ResoultsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _victoryText;
    [SerializeField] private TextMeshProUGUI _prepareText;
    [SerializeField] private TextMeshProUGUI _bossText;

    public void BossFight() =>
        StartCoroutine(PrepareToBossFight());

    public void PrepareToNextWave() =>
        StartCoroutine(PrepareToNextWaveRoutine());

    private IEnumerator PrepareToBossFight()
    {
        _bossText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _bossText.gameObject.SetActive(false);
    }

    private IEnumerator PrepareToNextWaveRoutine()
    {
        _victoryText.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);

        _victoryText.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        _prepareText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _prepareText.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(0.5f);
    }
}