public class EnemyCountViewer : ValueDisplay<EnemySpawner>
{
    protected override EnemySpawner EventSource => FindFirstObjectByType<EnemySpawner>();
    
    protected override void Subscribe(EnemySpawner source) =>
        source.ScoreChanged += UpdateDisplay;

    protected override void Unsubscribe(EnemySpawner source) =>
        source.ScoreChanged -= UpdateDisplay;
}