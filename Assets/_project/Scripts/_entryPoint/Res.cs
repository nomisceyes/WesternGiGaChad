using UnityEngine;

public static class Res
{
    public static class Audio
    {
        public static AudioClip BackgroundMusic;
        public static AudioClip MainMenuMusic;
        public static AudioClip RifleShootSound;
        public static AudioClip RifleReloadSound;
        public static AudioClip MouseClickSound;
    }

    public static class VFX
    {
        public static ParticleSystem RifleShootVFX;
        public static ParticleSystem HitVFX;
        public static ParticleSystem GhostDeathVFX;
    }

    public static void InitAudio()
    {
        Audio.BackgroundMusic = Resources.Load<AudioClip>("Audio/Music/Background");
        Audio.MainMenuMusic = Resources.Load<AudioClip>("Audio/Music/MainMenu");
        Audio.RifleShootSound = Resources.Load<AudioClip>("Audio/SFX/Rifle_Shoot");
        Audio.RifleReloadSound = Resources.Load<AudioClip>("Audio/SFX/Rifle_Reload");
        Audio.MouseClickSound = Resources.Load<AudioClip>("Audio/SFX/Click");
    }

    public static void InitVFX()
    {
        VFX.RifleShootVFX = Resources.Load<ParticleSystem>("VFX/ShootSmoke");
        VFX.HitVFX = Resources.Load<ParticleSystem>("VFX/Hit");
        VFX.GhostDeathVFX = Resources.Load<ParticleSystem>("VFX/EtherealHit");
    }
}