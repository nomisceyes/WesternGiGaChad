using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Health _health;

    public void TakeDamage(int damage) =>
        _health.TakeDamage(null, damage);

    public bool IsAlive() =>
        _health.IsAlive;
}