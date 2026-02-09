using UnityEngine;

public interface IInputService
{   
    public bool AimPressed { get; }

    public Vector2 GetMoveInput();
    public bool IsAiming();
    public void Update();
    public bool Aiming();
    public bool IsShooting();   
    public void Enable();
}