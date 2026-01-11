using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private bool _isAttack = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player) && _isAttack == false)
        {
            StartCoroutine(Attack(player));
        }
    }

    private IEnumerator Attack(Player player)
    {
        _isAttack = true;
        player.TakeDamage(5);

        yield return new WaitForSeconds(1f);

        _isAttack = false;
    }
}