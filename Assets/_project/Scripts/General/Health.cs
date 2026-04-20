using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const int MinAmount = 0;

    public event Action ValueChanged;
    public event Action Died;

    [field: SerializeField] public int MaxHealth { get; private set; }
    [field: SerializeField] public int CurrentHealth { get; private set; }

    public bool IsAlive { get; private set; } = true;
    
    private void Awake() =>
        CurrentHealth = MaxHealth;
    
    private void Start() =>
        ValueChanged?.Invoke();

    public void TakeDamage(int damage)
    {
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

    public void Reset()
    {
        IsAlive = true;
        CurrentHealth = MaxHealth;
        ValueChanged?.Invoke();
    }
}