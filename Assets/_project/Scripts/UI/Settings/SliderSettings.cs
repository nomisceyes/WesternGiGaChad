using UnityEngine;
using UnityEngine.UI;

public class SliderSettings : MonoBehaviour
{
    private Slider _slider;
    
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = Global.AudioManager.MusicVolume;
    }
    
    public void UpdateMusicVolume()
    {
        Global.AudioManager.SetMusicVolume(_slider.value);
    }

    public void UpdateSoundVolume()
    {
        Global.AudioManager.SetSoundVolume(_slider.value);
    }
}