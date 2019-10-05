using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnElement
{
    [Range(0, 10000)]
    public float SpawnTime = 0.0f;
    public Vector3 SpawnPosition = Vector3.zero;
    public GameObject ShipPrefab;
    public bool DrawGizmo = false;

    public bool IsValid { get => ShipPrefab && ShipPrefab.GetComponent<AbstractShipDescriptor>() & SpawnTime >= 0.0f; }

    public EnemySpawnElement(float spawnTime, Vector2 spawnPosition, GameObject shipPrefab)
    {
        SpawnTime = spawnTime;
        SpawnPosition = spawnPosition;
        ShipPrefab = shipPrefab;
    }

}
