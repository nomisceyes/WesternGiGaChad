using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;

    private bool _isAttack = false;

    private WaitForSeconds _delay;

    private void Start()
    {
        _delay = new WaitForSeconds(_attackDelay);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player) && _isAttack == false)
            StartCoroutine(Attack(player));
    }

    private IEnumerator Attack(Player player)
    {
        _isAttack = true;
        player.TakeDamage(_damage);

        yield return _delay;

        _isAttack = false;
    }
}