using UnityEngine;

[
    DisallowMultipleComponent,
    RequireComponent(typeof(Rigidbody)),
    RequireComponent(typeof(Collider))
]
public class DragAndDrop3D : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask dockableLayer;

    private Camera mainCamera;
    private Vector3 offset;
    private float zCoordinate;

    private Vector3 oldPosition;
    private TurretController turretController;

    void Start()
    {
        mainCamera = Camera.main;
        turretController = gameObject.GetComponent<TurretController>();
    }

    void OnMouseDown()
    {
        zCoordinate = mainCamera.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPosition();
        oldPosition = transform.position;
        if (turretController) turretController.OnDragging();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        CheckForDockableObject();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = zCoordinate;

        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void CheckForDockableObject()
    {
        if (turretController) turretController.ResetState();

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, dockableLayer))
        {
            if (gameObject.transform.parent)
            {
                gameObject.transform.parent.GetComponent<DockableObjectsManager>().OnUndocking(gameObject);
            }

            if (!hit.transform.GetComponent<DockableObjectsManager>().OnDocking(gameObject))
            {
                transform.position = oldPosition;
            }
        }
        else
        {
            transform.position = oldPosition;
        }
    }
}
