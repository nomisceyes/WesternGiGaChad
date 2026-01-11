using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private const int MinCameraPriority = 10;
    private const int MaxCameraPriority = 20;

    [SerializeField] private CinemachineCamera _freelookCamera;
    [SerializeField] private CinemachineCamera _aimCamera;
    [SerializeField] private CinemachineInputAxisController _inputAxisController;
    [SerializeField] private CrosshairController _crosshairController;
    [SerializeField] private InputReader _inputReader;

    private CinemachineOrbitalFollow _orbitalFollow;
    private AimCameraController _aimCameraController;

    private bool _isAiming = false;

    private void Start()
    {
        _aimCameraController = _aimCamera.GetComponent<AimCameraController>();
        _inputAxisController = _freelookCamera.GetComponent<CinemachineInputAxisController>();
        _orbitalFollow = _freelookCamera.GetComponent<CinemachineOrbitalFollow>();
    }

    private void Update()
    {
        if(_inputReader.AimPressed && _isAiming == false) // Подумать
        {
            EnterAimMode();
        }
        else if(_inputReader.AimPressed == false && _isAiming) //
        {
            ExitAimMode();
        }
    }

    private void EnterAimMode()
    {
        _isAiming = true;

        SnapAimCameraToPlayerForward();

        _aimCamera.Priority = MaxCameraPriority;
        _freelookCamera.Priority = MinCameraPriority;

        _inputAxisController.enabled = false;
        _crosshairController.EnableCrosshair();
    }

    private void ExitAimMode()
    {
        _isAiming = false;

        SnapFreeLookBehindPlayer();

        _aimCamera.Priority = MinCameraPriority;
        _freelookCamera.Priority = MaxCameraPriority;

        _inputAxisController.enabled = true;
        _crosshairController.DisableCrosshair();
    }

    private void SnapFreeLookBehindPlayer()
    {
        Vector3 forward = _aimCamera.transform.forward;
        float angle = Mathf.Atan2(forward.x,forward.z) * Mathf.Rad2Deg;

        _orbitalFollow.HorizontalAxis.Value = angle;
    }

    private void SnapAimCameraToPlayerForward()
    {
        _aimCameraController.SetYawPitchFromCameraForward(_freelookCamera.transform);
    }
}