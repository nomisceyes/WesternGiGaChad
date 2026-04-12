using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CameraSwitcher : MonoBehaviour
{
    private const int MinCameraPriority = 10;
    private const int MaxCameraPriority = 20;

    [SerializeField] private Rig _aimRig;
    [SerializeField] private CinemachineCamera _freelookCamera;
    [SerializeField] private CinemachineCamera _aimCamera;
    [SerializeField] private CinemachineInputAxisController _inputAxisController;
    [SerializeField] private CrosshairController _crosshairController;
    
    private CinemachineOrbitalFollow _orbitalFollow;
    private AimCameraController _aimCameraController;

    private float _aimRigWeight;
    private bool _isAiming = false;
    
    private void Start()
    {
        _aimCameraController = _aimCamera.GetComponent<AimCameraController>();
        _inputAxisController = _freelookCamera.GetComponent<CinemachineInputAxisController>();
        _orbitalFollow = _freelookCamera.GetComponent<CinemachineOrbitalFollow>();
        
        _aimRig.weight = 0f;
    }

    private void Update()
    {
        switch (Global.InputService.AimPressed)
        {
            case true when _isAiming == false:
                EnterAimMode();
                break;
            case false when _isAiming:
                ExitAimMode();
                break;
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

        
        _aimRig.weight = 1f;
    }

    private void ExitAimMode()
    {
        _isAiming = false;

        SnapFreeLookBehindPlayer();

        _aimCamera.Priority = MinCameraPriority;
        _freelookCamera.Priority = MaxCameraPriority;

        _inputAxisController.enabled = true;
        _crosshairController.DisableCrosshair();

        _aimRig.weight = 0f;
    }

    private void SnapFreeLookBehindPlayer()
    {
        Vector3 forward = _aimCamera.transform.forward;
        float angle = Mathf.Atan2(forward.x, forward.z) * Mathf.Rad2Deg;

        _orbitalFollow.HorizontalAxis.Value = angle;
    }

    private void SnapAimCameraToPlayerForward()
    {
        _aimCameraController.SetYawPitchFromCameraForward(_freelookCamera.transform);
    }
}