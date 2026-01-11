using UnityEngine;
using UnityEngine.UI;

public class HealthSliderBar : HealthBar
{
    [SerializeField] protected Slider Slider;

    private void Awake()
    {
        Slider.maxValue = Health.MaxAmount;

        Slider.SetValueWithoutNotify(Slider.maxValue);
    }

    public override void UpdateHealthAmount()
    {
        if (Health.IsAlive != true)
        {
            Slider.gameObject.SetActive(false);
        }
        else
        {
            Slider.value = Health.CurrentAmount;
            Slider.gameObject.SetActive(true);
        }
    }
}