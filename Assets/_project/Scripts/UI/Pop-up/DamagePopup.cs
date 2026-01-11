using System;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour, IObject<DamagePopup>
{
    [SerializeField] private TextMeshPro _text;

    public event Action<DamagePopup> Released;
    public event Action<Vector3> AnimPopup;

    public void SetUp(Transform point, int damageAmount)
    {
        _text.text = damageAmount.ToString();
        AnimPopup?.Invoke(point.position);
    }

    public void Release()
    {
        Released?.Invoke(this);
    }
}