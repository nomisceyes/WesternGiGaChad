using System.Collections;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    
    private readonly Vector3 _startScale = new Vector3(0, 0, 0);
    private readonly Vector3 _endScale = new Vector3(3.5f, 3.5f, 3.5f);
    
    private float _radius;

    private void Start()
    {
        _radius = _endScale.x / 2;
    }

    public void Prepare()
    {
        StartCoroutine(TestAttack());
    }

    private IEnumerator TestAttack()
    {
        float journey = 0f;

        while (journey < 1f)
        {
            journey += Time.deltaTime * _speed;
            transform.localScale = Vector3.Lerp(_startScale, _endScale, journey);
            yield return null;
        }

        transform.localScale = _endScale;

        CheckForPlayerHit();
        
        Destroy(gameObject);
    }

    private void CheckForPlayerHit()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Player player))
            {
                player.TakeDamage(_damage);
                break;
            }
        }
    }
}