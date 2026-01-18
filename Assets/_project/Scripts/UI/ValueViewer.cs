using TMPro;
using UnityEngine;

public class ValueViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnEnable() =>
        _enemySpawner.ScoreChanged += OnValueChanged;

    private void OnDisable() =>
        _enemySpawner.ScoreChanged -= OnValueChanged;

    public void OnValueChanged(int amount, int maxAmount) =>
        _text.text = ($"{amount} / {maxAmount}");
}