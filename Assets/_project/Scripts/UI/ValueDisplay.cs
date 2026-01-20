using TMPro;
using UnityEngine;

public abstract class ValueDisplay<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _text;

    protected abstract T EventSource { get; }
    protected abstract void Subscribe(T source);
    protected abstract void Unsubscribe(T source);

    protected virtual void OnEnable() =>
        Subscribe(EventSource);

    protected virtual void OnDisable() =>
        Unsubscribe(EventSource);

    protected virtual string FormatValue(int current, int max) =>
        $"{current} / {max}";

    protected void UpdateDisplay(int current, int max) =>
        _text.text = FormatValue(current, max);
}