using UnityEngine;

public class Player : MonoBehaviour
{
    public Health Health { get; private set; }

    public bool IsAiming { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    public void TakeDamage(int damage)
    {
        Health.TakeDamage(null, damage);
    }
}