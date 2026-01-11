using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected InputReader InputReader;
    [SerializeField] protected LayerMask Layers;
    [SerializeField] protected int MinDamage;
    [SerializeField] protected int MaxDamage;   

    protected void HitEffect(ParticleSystem hitImpactVFX, Vector3 position, Vector3 normal)
    {
        Vector3 offsetPosition = position + (normal * 0.05f);

        ParticleSystem hitEffect = Instantiate(hitImpactVFX, offsetPosition, Quaternion.LookRotation(normal));

        hitImpactVFX.Play();

        Destroy(hitEffect.gameObject, hitImpactVFX.main.duration);
    }   
}