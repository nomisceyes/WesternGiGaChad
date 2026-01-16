using UnityEngine;

public interface IInputService : IService, IStateUpdater
{   
    public bool AimPressed { get; }

    public Vector2 GetMoveInput();
    public bool IsAiming();
    public bool IsShooting();   
    public void Enable();
}