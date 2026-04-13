using UnityEngine;

public static class Res
{
    public static class Audio
    {
        public static AudioClip BackgroundMusic;
        public static AudioClip RifleShoot;
        public static AudioClip RifleReload;
    }

    public static class VFX
    {
        public static ParticleSystem RifleShootVFX;
        public static ParticleSystem HitVFX;
        public static ParticleSystem GhostDeathVFX;
    }

    public static void InitAudio()
    {
        Audio.BackgroundMusic = Resources.Load<AudioClip>("Audio/Background");
        Audio.RifleShoot = Resources.Load<AudioClip>("Audio/Rifle_Shoot");
        Audio.RifleReload = Resources.Load<AudioClip>("Audio/Rifle_Reload");
    }

    public static void InitVFX()
    {
        VFX.RifleShootVFX = Resources.Load<ParticleSystem>("VFX/Shoot");
        VFX.HitVFX = Resources.Load<ParticleSystem>("VFX/Hit");
        VFX.GhostDeathVFX = Resources.Load<ParticleSystem>("VFX/EtherealHit");
    }
}