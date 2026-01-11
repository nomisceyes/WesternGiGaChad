using UnityEngine;

public class Player : MonoBehaviour
{
    public Health Health;

    public void TakeDamage(int damage)
    {
        Health.TakeDamage(null, damage);
    }
}