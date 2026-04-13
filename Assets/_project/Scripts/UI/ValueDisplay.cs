using TMPro;
using UnityEngine;

public abstract class ValueDisplay<T> : MonoBehaviour, IUIElement where T : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    protected abstract T EventSource { get; }
    protected abstract void Subscribe(T source);
    protected abstract void Unsubscribe(T source);

    public void Init()
    {
        Subscribe(EventSource);
        
        Debug.Log(gameObject.name + " init");
    }

    protected virtual void OnDisable() =>
        Unsubscribe(EventSource);

    protected virtual string FormatValue(int current, int max) =>
        $"{current} / {max}";

    protected void UpdateDisplay(int current, int max) =>
        _text.text = FormatValue(current, max);
}