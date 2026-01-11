using System.Collections;
using UnityEngine;

public class PopupSpawner : Spawner<DamagePopup>
{
    [SerializeField] private float _lifetime;

    private WaitForSeconds _lifetimePopup;

    private void Start()
    {
        _lifetimePopup = new(_lifetime);
    }

    public void Create(Transform popupPoint, int damage)
    {
        DamagePopup popup = Pool.Get();

        popup.transform.position = popupPoint.position;
        popup.SetUp(popupPoint, damage);

        StartCoroutine(DestroyDelay(popup));
    }

    private IEnumerator DestroyDelay(DamagePopup popup)
    {
        yield return _lifetimePopup;

        popup.Release();
    }
}