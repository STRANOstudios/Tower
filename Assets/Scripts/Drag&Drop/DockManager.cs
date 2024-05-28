using System.Collections.Generic;
using UnityEngine;

public class DockManager : MonoBehaviour
{
    public int maxObjectsPerDock = 3;
    private List<GameObject> dockedObjects = new List<GameObject>();

    public void AddObjectToDock(GameObject obj)
    {
        if (dockedObjects.Count < maxObjectsPerDock)
        {
            dockedObjects.Add(obj);
            obj.transform.SetParent(transform);
            PositionObjectsInDock();
        }
        else
        {
            Debug.Log("Il dock ha raggiunto il limite massimo di oggetti associabili.");
            Destroy(obj); // Puoi decidere cosa fare con l'oggetto se il dock è pieno
        }
    }

    private void PositionObjectsInDock()
    {
        // Posiziona gli oggetti impilati sopra il dock
        for (int i = 0; i < dockedObjects.Count; i++)
        {
            dockedObjects[i].transform.localPosition = new Vector3(0, i * 1.0f, 0); // Modifica il valore di y per regolare l'altezza dell'impilamento
        }
    }
}
