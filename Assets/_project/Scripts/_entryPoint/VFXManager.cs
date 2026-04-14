using UnityEngine;

public class VFXManager : MonoBehaviour, IService
{
    public ParticleSystem GhostDeathVFX;
    public ParticleSystem RifleShootVFX;
    public ParticleSystem HitVFX;
    
    public void Init()
    {
        GhostDeathVFX =  Instantiate(Res.VFX.GhostDeathVFX);
        RifleShootVFX = Instantiate(Res.VFX.RifleShootVFX);
        HitVFX = Instantiate(Res.VFX.HitVFX);
    }
}