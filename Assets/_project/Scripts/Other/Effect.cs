using System;
using UnityEngine;

public class Effect : MonoBehaviour, IObject<Effect>
{
    public event Action<Effect> Released;
}