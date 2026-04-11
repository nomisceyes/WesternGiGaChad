using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected LayerMask Layers;
    [SerializeField] protected int minDamage;
    [SerializeField] protected int maxDamage;

    private GameObject _hitEffect;

    private void OnDestroy()
    {
        Destroy(_hitEffect);
    }

    protected void HitEffect(ParticleSystem vfx, Vector3 position, Vector3 normal)
    {
        Vector3 offsetPosition = position + (normal * 0.05f);

        //ParticleSystem hitEffect = Instantiate(hitImpactVFX, offsetPosition, Quaternion.LookRotation(normal));
        ParticleSystem hit = Instantiate(vfx, offsetPosition, Quaternion.LookRotation(normal));

        hit.Play();

        Destroy(hit.gameObject, hit.main.duration);
    }
}