using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const int MinAmount = 0;

    public event Action ValueChanged;
    public event Action<Transform, int> Popup;
    public event Action Died;

    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int CurrentHealth { get; private set; }

    public bool IsAlive { get; private set; } = true;

    private void Awake() =>
        CurrentHealth = MaxHealth;

    private void Start() =>
        ValueChanged?.Invoke();

    public void TakeDamage(Transform popupPoint, int damage)
    {
        Popup?.Invoke(popupPoint, damage);

        if (damage >= 0)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, MinAmount, MaxHealth);
        }

        if (CurrentHealth <= 0)
        {
            IsAlive = false;
            Died?.Invoke();
        }

        ValueChanged?.Invoke();
    }

    public void Restore(int value)
    {
        IsAlive = true;

        CurrentHealth = Mathf.Clamp(CurrentHealth + Mathf.Abs(value), MinAmount, MaxHealth);

        ValueChanged?.Invoke();
    }

    public bool GetPossibleOfHealing() =>
        CurrentHealth < MaxHealth;

    public void Reset()
    {
        CurrentHealth = MaxHealth;
        ValueChanged?.Invoke();
    }
}