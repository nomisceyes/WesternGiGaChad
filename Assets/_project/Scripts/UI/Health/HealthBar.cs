using UnityEngine;

public abstract class HealthBar : MonoBehaviour, IHealthObserver
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.ValueChanged += UpdateHealthAmount;
    }

    private void OnDisable()
    {
        Health.ValueChanged -= UpdateHealthAmount;
    }

    public abstract void UpdateHealthAmount();
}