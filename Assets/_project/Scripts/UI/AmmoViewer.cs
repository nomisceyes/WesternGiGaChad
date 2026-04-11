public class AmmoViewer : ValueDisplay<RangeWeapon>
{
    protected override RangeWeapon EventSource => FindFirstObjectByType<RangeWeapon>();

    protected override void Subscribe(RangeWeapon source) =>
        source.AmmoChanged += UpdateDisplay;

    protected override void Unsubscribe(RangeWeapon source) =>
        source.AmmoChanged -= UpdateDisplay;
}