using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineThirdPersonFollow _aimCamera;
    [SerializeField] private InputActionReference _lookInput;
    [SerializeField] private Transform _yawTarget;
    [SerializeField] private Transform _pitchTarget;
    [SerializeField] private float _mouseSensitivity = 0.05f;
    [SerializeField] private float _sensitivity = 1.5f;
    [SerializeField] private float _minPitch = -40f;
    [SerializeField] private float _maxPitch = 80f;

    private float _yaw;
    private float _pitch;

    private void Start()
    {
        Vector3 angles = _yawTarget.rotation.eulerAngles;
        _yaw = angles.y;
        _pitch = angles.x;

        _lookInput.asset.Enable();
    }

    private void Update()
    {
        _pitchTarget.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        Vector2 look = _lookInput.action.ReadValue<Vector2>();
        look *= _mouseSensitivity;

        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);

        _yaw += look.x * _sensitivity;
        _pitch -= look.y * _sensitivity;

        _yawTarget.rotation = Quaternion.Euler(0f, _yaw, 0f);
        _pitchTarget.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    public void SetYawPitchFromCameraForward(Transform cameraTransform)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 flatForward = cameraTransform.forward;
        flatForward.y = 0f;

        if (flatForward.sqrMagnitude < 0.01f)
            return;

        _yaw = Quaternion.LookRotation(flatForward).eulerAngles.y;

        forward.Normalize();
        _pitch = -Mathf.Asin(forward.y) * Mathf.Rad2Deg;

        _yawTarget.rotation = Quaternion.Euler(0f, _yaw, 0f);
    }
}