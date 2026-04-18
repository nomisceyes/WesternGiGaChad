using UnityEngine;

public class VFXManager : MonoBehaviour, IService
{
    public ParticleSystem GhostDeathVFX;
    public ParticleSystem RifleShootVFX;
    public ParticleSystem HitVFX;

    private GameObject _vfx;

    public void Init()
    {
        _vfx = new GameObject("VFXManager");

        GhostDeathVFX = InstantiateAndPersist(Res.VFX.GhostDeathVFX);
        RifleShootVFX = InstantiateAndPersist(Res.VFX.RifleShootVFX);
        HitVFX = InstantiateAndPersist(Res.VFX.HitVFX);

        DontDestroyOnLoad(_vfx);
    }

    private ParticleSystem InstantiateAndPersist(ParticleSystem prefab)
    {
        var instance = Instantiate(prefab, transform);
        _vfx.transform.parent = instance.transform;
        DontDestroyOnLoad(instance.gameObject);
        return instance;
    }
}