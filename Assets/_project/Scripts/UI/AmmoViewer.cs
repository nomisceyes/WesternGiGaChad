using TMPro;
using UnityEngine;

public class AmmoViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RangeWeapon _gun;

    private void OnEnable() =>   
        _gun.AmmoChanged += OnAmmoChanged;    

    private void OnDisable() =>   
        _gun.AmmoChanged -= OnAmmoChanged;   

    public void OnAmmoChanged(int amount, int maxAmount) =>
        _text.text = ($"{amount} / {maxAmount}");
}