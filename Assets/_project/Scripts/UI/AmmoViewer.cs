using UnityEngine;

public class AmmoViewer : ValueDisplay<RangeWeapon>
{
    //[SerializeField] private RangeWeapon _gun;
    
    protected override RangeWeapon EventSource => FindFirstObjectByType<RangeWeapon>();
    
    protected override void Subscribe(RangeWeapon source) =>
        source.AmmoChanged += UpdateDisplay;

    protected override void Unsubscribe(RangeWeapon source) =>
        source.AmmoChanged -= UpdateDisplay;
}