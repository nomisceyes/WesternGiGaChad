using System.Collections;
using UnityEngine;

public class PopupSpawner : Spawner<DamagePopup>
{
    public void Create(Transform popupPoint, int damage)
    {
        DamagePopup popup = Pool.Get();

        popup.transform.position = popupPoint.position;
        popup.SetUp(popupPoint, damage);

        StartCoroutine(DestroyDelay(popup));
    }

    private IEnumerator DestroyDelay(DamagePopup popup)
    {
        yield return new WaitForSeconds(1f); // Исправить

        popup.Release();
    }
}