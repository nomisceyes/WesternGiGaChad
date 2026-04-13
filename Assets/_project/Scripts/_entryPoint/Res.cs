using UnityEngine;

public static class Res
{
    public static class Audio
    {
        public static AudioClip BackgroundMusic;
        public static AudioClip RifleShootSound;
        public static AudioClip RifleReloadSound;
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
        Audio.RifleShootSound = Resources.Load<AudioClip>("Audio/Rifle_Shoot");
        Audio.RifleReloadSound = Resources.Load<AudioClip>("Audio/Rifle_Reload");
    }

    public static void InitVFX()
    {
        VFX.RifleShootVFX = Resources.Load<ParticleSystem>("VFX/ShootSmoke");
        VFX.HitVFX = Resources.Load<ParticleSystem>("VFX/Hit");
        VFX.GhostDeathVFX = Resources.Load<ParticleSystem>("VFX/EtherealHit");
    }
}