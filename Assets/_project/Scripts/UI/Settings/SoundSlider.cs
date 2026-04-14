using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public abstract class SoundSlider : MonoBehaviour
{
    private const float MaxVolume = 20f;
    private const float MinVolume = -80f;

    [SerializeField] private AudioMixer _audioMixer;
    private Slider _slider;

    protected abstract string ParameterName { get; }

    private void Awake() =>
        _slider = GetComponent<Slider>();

    private void Start()
    {
        Global.AudioManager.SetMusicVolume(_slider.value);
        Global.AudioManager.SetSoundVolume(_slider.value);
    }

    public void ChangeVolume(float volume)
    {
        float logVolume = Mathf.Log10(volume) * 20;

        _audioMixer.SetFloat(ParameterName, Mathf.Clamp(logVolume, MinVolume, MaxVolume));
    }
}