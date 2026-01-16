using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private RectTransform _crosshairUI;
    [SerializeField] private Camera _aimCamera;
    [SerializeField] private LayerMask _raycastMask = ~0;
    [SerializeField] private float _maxDistance = 20f;
    [SerializeField] private float _crosshairOffsetMultiplier = 0.01f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _aimCamera = Camera.main;
        DisableCrosshair();
    }

    private void LateUpdate()
    {
        Vector3 screenCenter = new (Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = _aimCamera.ScreenPointToRay(screenCenter);
        Vector3 targetPosition;

        if(Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _raycastMask))
        {
            targetPosition = hit.point + hit.normal * _crosshairOffsetMultiplier;
            _crosshairUI.rotation = Quaternion.LookRotation(hit.normal);
            Debug.DrawLine(hit.point, hit.point + hit.normal * 2f, Color.green);
        }
        else
        {
            targetPosition = ray.GetPoint(_maxDistance);
            _crosshairUI.forward = _aimCamera.transform.forward;
        }

        _crosshairUI.position = targetPosition;
    }

    public void EnableCrosshair()
    {
        _crosshairUI.gameObject.SetActive(true);
    }

    public void DisableCrosshair()
    {
        _crosshairUI.gameObject.SetActive(false);
    }
}