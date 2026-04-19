using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected LayerMask Layers;
    public int MinDamage;
    public int MaxDamage;
}