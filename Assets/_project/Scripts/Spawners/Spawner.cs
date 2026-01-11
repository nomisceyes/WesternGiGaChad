using UnityEngine;
using UnityEngine.Pool;

public class Spawner<TObject> : MonoBehaviour where TObject : MonoBehaviour, IObject<TObject>
{
    [SerializeField] private TObject Prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maxPoolSize;

    protected ObjectPool<TObject> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<TObject>(
              createFunc: () => Instantiate(Prefab, transform, true),
              actionOnGet: (@object) => SetUpObject(@object),
              actionOnRelease: (@object) => ResetObject(@object),
              defaultCapacity: _poolCapacity,
              maxSize: _maxPoolSize);
    }

    public virtual void Release(TObject @object) =>
        Pool.Release(@object);

    protected virtual void ResetObject(TObject @object)
    {
        @object.Released -= Release;
        @object.gameObject.SetActive(false);
    }

    protected virtual void SetUpObject(TObject @object)
    {
        @object.Released += Release;
        @object.gameObject.SetActive(true);
    }
}