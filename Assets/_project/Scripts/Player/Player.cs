using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _flashDuration = 0.4f;
    
    [HideInInspector] public Health Health;
    [HideInInspector] public WeaponUser WeaponUser;

    private Material _material;
    private float _hitAmount = 0f;
    
    private string _hitAmountProperty = "_HitAmount";

    private void Awake()
    {
        Health = GetComponent<Health>();
        WeaponUser = GetComponent<WeaponUser>();
    }
    
    private void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        
        if(renderer != null)
            _material = renderer.material;
    }

    private void Update()
    {
        if (_hitAmount > 0f)
        {
            _hitAmount -= Time.deltaTime / _flashDuration;
            
            _hitAmount = Mathf.Max(0f, _hitAmount);

            if (_material != null)
            {
                _material.SetFloat(_hitAmountProperty, _hitAmount);
            }
        }
    }
    
    public void TakeDamage(int damage)
    {
        _hitAmount = 1f;
        _material.SetFloat(_hitAmountProperty, _hitAmount);
        
        Health.TakeDamage(null, damage);
    }
}