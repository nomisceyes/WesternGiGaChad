using UnityEngine;

public class EffectSpawner : Spawner<Effect>
{
    protected override Effect CreatePrefab(Effect prefab, Vector3 position, Vector3 normal)
    {
        return Instantiate(prefab, position, Quaternion.LookRotation(normal));
    }
}