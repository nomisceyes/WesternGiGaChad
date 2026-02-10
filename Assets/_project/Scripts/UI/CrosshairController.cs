using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private Image _crosshair;
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

    private void FixedUpdate()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = _aimCamera.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPosition;
    
        if(Physics.Raycast(ray, out RaycastHit hit, _maxDistance, _raycastMask))
        {
            targetPosition = hit.point + hit.normal * _crosshairOffsetMultiplier;
            //_crosshairUI.rotation = Quaternion.LookRotation(hit.normal);
            Debug.DrawLine(hit.point, hit.point + hit.normal * 2f, Color.green);
        }
        else
        {
            targetPosition = ray.GetPoint(_maxDistance);
            _target.transform.forward = _aimCamera.transform.forward;
        }
    
        _target.transform.position = targetPosition;
    }

    public void EnableCrosshair()
    {
        _crosshair.gameObject.SetActive(true);
    }

    public void DisableCrosshair()
    {
        _crosshair.gameObject.SetActive(false);
    }
}