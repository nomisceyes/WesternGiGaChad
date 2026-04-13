public class AmmoViewer : ValueDisplay<RangeWeapon>
{
    protected override RangeWeapon EventSource => Global.Main.Player.WeaponUser.Gun;

    protected override void Subscribe(RangeWeapon source)
    {
        source.AmmoChanged += UpdateDisplay;
        UpdateDisplay(source.CurrentAmmo, source.MaxAmmo);
    }

    protected override void Unsubscribe(RangeWeapon source) =>
        source.AmmoChanged -= UpdateDisplay;
}