using UnityEngine;

public class EnemyCountViewer : ValueDisplay<EnemySpawner>
{
    [SerializeField] private EnemySpawner _enemySpawner;
    
    protected override EnemySpawner EventSource => _enemySpawner;

    protected override void Subscribe(EnemySpawner source) =>
        source.ScoreChanged += UpdateDisplay;

    protected override void Unsubscribe(EnemySpawner source) =>
        source.ScoreChanged -= UpdateDisplay;
}