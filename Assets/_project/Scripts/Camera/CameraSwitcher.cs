using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _freelookCamera;
    [SerializeField] private CinemachineCamera _aimCamera;
    [SerializeField] private CinemachineInputAxisController _inputAxisController;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Mover _player;
    [SerializeField] private GameObject _crosshairUI; // «¿Ã≈Õ»“‹
    [SerializeField] private CrosshairController _crosshairController;
    [SerializeField] private InputReader _inputReader;

    private AimCameraController _aimCameraController;
    private bool _isAiming = false;

    public bool AimPressed;

    private void Start()
    {
        _aimCameraController = _aimCamera.GetComponent<AimCameraController>();
        _inputAxisController = _freelookCamera.GetComponent<CinemachineInputAxisController>();
    }

    private void Update()
    {
        AimPressed = _inputReader.IsAiming();

        _player.IsAiming = AimPressed;

        if(AimPressed && _isAiming == false)
        {
            EnterAimMode();
        }
        else if(AimPressed == false && _isAiming)
        {
            ExitAimMode();
        }
    }

    private void EnterAimMode()
    {
        _isAiming = true;

        SnapAimCameraToPlayerForward();

        _aimCamera.Priority = 20;
        _freelookCamera.Priority = 10;

        _inputAxisController.enabled = false;
        _crosshairController.EnableCrosshair();
    }

    private void ExitAimMode()
    {
        _isAiming = false;

        SnapFreeLookBehindPlayer();

        _aimCamera.Priority = 10;
        _freelookCamera.Priority = 20;

        _inputAxisController.enabled = true;
        _crosshairController.DisableCrosshair();
    }

    private void SnapFreeLookBehindPlayer()
    {
        CinemachineOrbitalFollow orbitalFollow = _freelookCamera.GetComponent<CinemachineOrbitalFollow>();
        Vector3 forward = _aimCamera.transform.forward;
        float angle = Mathf.Atan2(forward.x,forward.z) * Mathf.Rad2Deg;

        orbitalFollow.HorizontalAxis.Value = angle;
    }

    private void SnapAimCameraToPlayerForward()
    {
        _aimCameraController.SetYawPitchFromCameraForward(_freelookCamera.transform);
    }
}