using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button turretStandard;
    [SerializeField] Button turretShotgun;
    [SerializeField] Button turretArtelly;
    [SerializeField] Button boostSpeed;
    [SerializeField] Button boostDamage;
    [Space]
    [SerializeField] GameObject turretStandardPrefab;
    [SerializeField] GameObject turretShotgunPrefab;
    [SerializeField] GameObject turretArtellyPrefab;
    [SerializeField] GameObject boostSpeedPrefab;
    [SerializeField] GameObject boostDamagePrefab;
    [Space]
    [SerializeField] Transform spawnPosition;

    [Header("Settings")]
    [SerializeField] LayerMask excludeLayer;
    [SerializeField, Min(0)] float radius = 5f;

    private void OnEnable()
    {
        turretStandard.onClick.AddListener(() => { Spawn(turretStandardPrefab); });
        turretShotgun.onClick.AddListener(() => { Spawn(turretShotgunPrefab); });
        turretArtelly.onClick.AddListener(() => { Spawn(turretArtellyPrefab); });
        boostSpeed.onClick.AddListener(() => { Spawn(boostSpeedPrefab); });
        boostDamage.onClick.AddListener(() => { Spawn(boostDamagePrefab); });
    }

    private void OnDisable()
    {
        turretStandard.onClick.RemoveAllListeners();
        turretShotgun.onClick.RemoveAllListeners();
        turretArtelly.onClick.RemoveAllListeners();
        boostSpeed.onClick.RemoveAllListeners();
        boostDamage.onClick.RemoveAllListeners();
    }

    private void Spawn(GameObject prefab)
    {
        if (!IsTargetOccupied()) Instantiate(prefab, spawnPosition.position, Quaternion.identity);
    }

    private bool IsTargetOccupied()
    {
        //Ray ray = new(spawnPosition.position + Vector3.up, Vector3.down);
        //float maxDistance = 2 * radius;

        //if (Physics.SphereCast(ray, radius, out RaycastHit hit, maxDistance/*, ~excludeLayer*/))
        //{
        //    Debug.Log("Target occupied");
        //    return true;
        //}

        //return false;
        Collider[] colliders = Physics.OverlapSphere(spawnPosition.position, radius, ~excludeLayer);

        foreach (Collider collider in colliders)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnPosition.position + Vector3.up , 5f);
    }
}