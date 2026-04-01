using UnityEngine;
using UnityEngine.Audio;

public abstract class SoundSlider : MonoBehaviour
{
    private const float MaxVolume = 20f;
    private const float MinVolume = -80f;

    [SerializeField] private AudioMixer _audioMixer;
    
    protected abstract string ParameterName { get; }

    public void ChangeVolume(float volume)
    {
        float logVolume = Mathf.Log10(volume) * 20;
        
        _audioMixer.SetFloat(ParameterName, Mathf.Clamp(logVolume, MinVolume, MaxVolume));
    }
}