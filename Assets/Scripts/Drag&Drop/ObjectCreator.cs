using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab dell'oggetto 3D da creare
    private GameObject currentObject;
    private Camera mainCamera;
    private bool isDragging = false;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Metodo da collegare all'evento OnClick del pulsante TextMeshPro
    public void CreateObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = hit.point;
            currentObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            isDragging = true;
        }
    }

    private void Update()
    {
        if (isDragging && currentObject != null)
        {
            // Continua a trascinare l'oggetto seguendo il mouse
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                currentObject.transform.position = hit.point;
            }

            // Rilascia l'oggetto quando il pulsante del mouse viene rilasciato
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;

                // Controlla se l'oggetto è stato rilasciato su un "dock"
                if (Physics.Raycast(ray, out RaycastHit hitAfterRelease))
                {
                    if (hitAfterRelease.collider != null && hitAfterRelease.collider.CompareTag("Dock"))
                    {
                        DockManager dockManager = hitAfterRelease.collider.GetComponent<DockManager>();
                        if (dockManager != null)
                        {
                            dockManager.AddObjectToDock(currentObject);
                            currentObject = null; // Rimuove il riferimento all'oggetto corrente poiché è stato associato
                        }
                    }
                    else
                    {
                        // Distrugge l'oggetto se non viene rilasciato su un "dock"
                        Destroy(currentObject);
                        currentObject = null;
                    }
                }
                else
                {
                    // Distrugge l'oggetto se non viene rilasciato su un "dock"
                    Destroy(currentObject);
                    currentObject = null;
                }
            }
        }
    }
}
