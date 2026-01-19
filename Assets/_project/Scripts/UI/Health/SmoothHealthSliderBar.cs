using System.Collections;
using UnityEngine;

public class SmoothHealthSliderBar : HealthSliderBar
{
    [SerializeField] private float _fillingSpeed = 10f;

    private Coroutine _coroutine;

    public override void UpdateHealthAmount()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (Health.IsAlive != true)
        {
            Slider.gameObject.SetActive(false);
        }
        else
        {
            _coroutine = StartCoroutine(SmoothFill());
            Slider.gameObject.SetActive(true);
        }
    }

    private IEnumerator SmoothFill()
    {
        while (Mathf.Approximately(Slider.value, Health.CurrentHealth) == false)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, Health.CurrentHealth, _fillingSpeed * Time.deltaTime);

            yield return null;
        }
    }
}