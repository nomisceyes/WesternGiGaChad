using UnityEngine;

public static class Res
{
    public static class Audio
    {
        public static AudioClip BackgroundMusic;
        public static AudioClip RifleShoot;
        public static AudioClip RifleReload;
    }

    // public static class VFX
    // {
    //     public static ParticleSystem ShootVFX;
    //     public static ParticleSystem HitVFX;
    // }

    public static void InitAudio()
    {
        Audio.BackgroundMusic = Resources.Load<AudioClip>("Audio/Background");
        Audio.RifleShoot = Resources.Load<AudioClip>("Audio/Rifle_Shoot");
        Audio.RifleReload = Resources.Load<AudioClip>("Audio/Rifle_Reload");
    }

    // public static void InitVFX()
    // {
    //     VFX.ShootVFX = Resources.Load<ParticleSystem>("VFX/Shoot");
    //     VFX.HitVFX = Resources.Load<ParticleSystem>("VFX/Hit");
    // }
}