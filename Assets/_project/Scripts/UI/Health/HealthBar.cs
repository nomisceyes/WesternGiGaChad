using UnityEngine;

public abstract class HealthBar : MonoBehaviour, IHealthObserver
{
    protected Health Health;
    
    [Inject]
    public void Construct(Health health)
    {
        Health = health;
    }
    
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