using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHealthObserver
{
    [SerializeField] private float _fillingSpeed = 10f;
    [SerializeField] protected Health Health;
    [SerializeField] protected Slider Slider;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        Health.ValueChanged += UpdateHealthAmount;
        Health.Died += DisableHealthBar;

        Slider.maxValue = Health.MaxHealth;
        Slider.SetValueWithoutNotify(Slider.maxValue);
        Slider.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Health.ValueChanged -= UpdateHealthAmount;
        Health.Died -= DisableHealthBar;
    }

    public void UpdateHealthAmount()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothFill());
    }

    private void DisableHealthBar() =>
        Slider.gameObject.SetActive(false);

    private IEnumerator SmoothFill()
    {
        while (Mathf.Approximately(Slider.value, Health.CurrentHealth) == false)
        {
            Slider.value = Mathf.MoveTowards(Slider.value, Health.CurrentHealth, _fillingSpeed * Time.deltaTime);

            yield return null;
        }
    }
}