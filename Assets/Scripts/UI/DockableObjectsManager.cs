using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DockableObjectsManager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject objectPrefab; // Prefab dell'oggetto 3D da creare
    public int maxObjectsPerDock = 3; // Numero massimo di oggetti associabili a ogni dock

    private GameObject createdObject;
    private Camera mainCamera;
    private bool isDragging = false;
    private List<GameObject> dockedObjects = new List<GameObject>();

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Crea un nuovo oggetto quando il pulsante TextMeshPro viene premuto
        if (eventData.pointerPress.gameObject.GetComponent<TextMeshProUGUI>() != null)
        {
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * 5f; // Posizione di spawn dell'oggetto
            createdObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            isDragging = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Inizia a trascinare l'oggetto
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Trascina l'oggetto in base al movimento del mouse
        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                createdObject.transform.position = hit.point;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Termina il trascinamento dell'oggetto
        isDragging = false;

        // Controlla se l'oggetto è stato rilasciato su un oggetto con tag "dock"
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("Dock"))
        {
            GameObject dock = eventData.pointerCurrentRaycast.gameObject;

            // Controlla se il dock ha raggiunto il limite massimo di oggetti associabili
            if (dockedObjects.Count < maxObjectsPerDock)
            {
                dockedObjects.Add(createdObject);
                createdObject.transform.SetParent(dock.transform); // Associa l'oggetto al dock
            }
            else
            {
                Debug.Log("Il dock ha raggiunto il limite massimo di oggetti associabili.");
                Destroy(createdObject); // Distruggi l'oggetto se il dock ha raggiunto il limite
            }
        }
    }
}
