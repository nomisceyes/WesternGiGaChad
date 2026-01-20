using UnityEngine;

public class AmmoViewer : ValueDisplay<RangeWeapon>
{
    [SerializeField] private RangeWeapon _gun;
    
    protected override RangeWeapon EventSource => _gun;
    
    protected override void Subscribe(RangeWeapon source) =>
        source.AmmoChanged += UpdateDisplay;

    protected override void Unsubscribe(RangeWeapon source) =>
        source.AmmoChanged -= UpdateDisplay;
}