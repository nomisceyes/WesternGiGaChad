using System.Collections;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;

    private Vector3 startScale = new Vector3(0, 0, 0);
    private Vector3 endScale = new Vector3(2.3f, 2.3f, 2.3f);
    private bool _canAttack = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            Debug.Log("Hit");
            
            player.TakeDamage(_damage);
        }
    }

    public void Prepare()
    {
        StartCoroutine(TestAttack());
    }

    private IEnumerator TestAttack()    // Сделать через OverlapSphere
    {
        float journey = 0f;

        while (journey < 1f)
        {
            journey += Time.deltaTime * _speed;
            transform.localScale = Vector3.Lerp(startScale, endScale, journey);
            yield return null;
        }

        transform.localScale = endScale;

        if (transform.localScale == endScale)
            Destroy(gameObject);
    }
}