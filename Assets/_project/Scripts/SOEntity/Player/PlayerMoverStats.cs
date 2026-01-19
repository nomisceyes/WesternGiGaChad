using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoverStats", menuName = "ScriptableObjects/PlayerMoverStats")]
public class PlayerMoverStats : ScriptableObject
{
     public float Gravity = 9.81f;
     
     public float Speed;
     public float BackwardSpeed = 3f;
     public float Acceleration = 10f;
     public float RotationSpeed = 10f;
     public bool ShouldFaceMoveDirection = false;
}