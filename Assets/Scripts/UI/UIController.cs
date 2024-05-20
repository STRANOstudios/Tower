using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public ObjectPicker objectPicker; // Riferimento al componente ObjectPicker
    public List<GameObject> turretPrefab; // Lista di prefab delle torrette
    public List<GameObject> bufferPrefab; // Lista di prefab dei buffer

    // Metodo chiamato quando viene premuto un pulsante della torretta
    public void ButtonPressedTurret(int turretType)
    {
        if (turretType < 0 || turretType >= turretPrefab.Count)
        {
            Debug.LogError("Turret type out of range");
            return;
        }

        // Crea un'istanza della torretta selezionata
        GameObject turretInstance = Instantiate(turretPrefab[turretType]);
        //objectPicker.SelectTurret(turretInstance);
    }

    // Metodo chiamato quando viene premuto un pulsante del buffer
    public void ButtonPressedBuffer(int bufferType)
    {
        if (bufferType < 0 || bufferType >= bufferPrefab.Count)
        {
            Debug.LogError("Buffer type out of range");
            return;
        }

        // Crea un'istanza del buffer selezionato
        GameObject bufferInstance = Instantiate(bufferPrefab[bufferType]);
        //objectPicker.SelectTurret(bufferInstance);
    }
}