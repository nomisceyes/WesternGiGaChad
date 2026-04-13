public class EnemyCountViewer : ValueDisplay<EnemySpawner>
{
    protected override EnemySpawner EventSource => Global.Main.EnemySpawner;
    
    protected override void Subscribe(EnemySpawner source) =>
        source.ScoreChanged += UpdateDisplay;

    protected override void Unsubscribe(EnemySpawner source) =>
        source.ScoreChanged -= UpdateDisplay;
}