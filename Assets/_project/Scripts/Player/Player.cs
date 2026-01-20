using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _yawCamera;
    [SerializeField] private PlayerMoverStats _playerMoverStats;

    private Health _health;
    private Mover _mover;

    private IInputService
        _inputService; // Возможно нужен будет если делать прицеливание через этот класс и мозг не ебать

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;

        _mover = new Mover(_characterController, _camera, _yawCamera, _playerMoverStats, _inputService, transform);
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void Update()
    {
        _mover.Update();
    }

    public void TakeDamage(int damage) =>
        _health.TakeDamage(null, damage);

    public bool IsAlive() =>
        _health.IsAlive;
}