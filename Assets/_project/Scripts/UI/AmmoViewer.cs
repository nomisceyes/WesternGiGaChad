using TMPro;
using UnityEngine;

public class AmmoViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Weapon _weapon;

    private void OnEnable()
    {
        _weapon.AmmoChanged += OnAmmoChanged;
    }

    private void OnDisable()
    {
        _weapon.AmmoChanged -= OnAmmoChanged;
    }

    public void OnAmmoChanged(int amount, int maxAmount) =>
        _text.text = ($"{amount} / {maxAmount}");
}