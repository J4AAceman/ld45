using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelController : MonoBehaviour
{
    public List<EnemySpawnElement> SpawnList;
    [Range(0, 10000)]
    public float LevelEndTime = 0;

    public float ScrollRate = 1.0f;

    private int index = 0;

    private void Awake()
    {
        // Check all spawn elements
        for(var i = 0; i < SpawnList.Count; i++)
        {
            if(!SpawnList[i].IsValid)
            {
                Debug.LogError("Invalid SpawnElement: i = " + i.ToString(), this);
            }
        }

        SortSpawnList();

        if(LevelEndTime < SpawnList[SpawnList.Count-1].SpawnTime)
        {
            Debug.LogWarning("LevelEndTime less than SpawnTime of last ship: Level will end before all enemies spawn");
        }
    }

    public void SortSpawnList()
    {
        SpawnList.Sort((a, b) => a.SpawnTime < b.SpawnTime ? -1 : a.SpawnTime == b.SpawnTime ? 0 : 1);
    }

    private void FixedUpdate()
    {
        for(; index < SpawnList.Count; index++)
        {
            if(SpawnList[index].SpawnTime < Time.timeSinceLevelLoad)
            {
                GameObject go = Instantiate(SpawnList[index].ShipPrefab, transform.position + new Vector3(SpawnList[index].SpawnPosition.x, SpawnList[index].SpawnPosition.y), Quaternion.Euler(0, 0, 180));
                // Set layer for all weapons' bullets
                foreach(var weapon in go.GetComponent<AbstractShipDescriptor>().ShipWeaponList)
                {
                    weapon.BulletLayer = LayerMask.NameToLayer("EnemyBullets");
                }
            }
            else
            {
                break;
            }
        }

        if(LevelEndTime < Time.timeSinceLevelLoad)
        {
            // TODO: end level
        }
    }

    public void DrawAllGizmos()
    {
        foreach (var se in SpawnList)
        {
            se.DrawGizmo = true;
        }
    }

    public void HideAllGizmos()
    {
        foreach (var se in SpawnList)
        {
            se.DrawGizmo = false;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw Gizmos for all spawn elements
        foreach(var se in SpawnList)
        {
            if (se.DrawGizmo)
            {
                Vector3 position = transform.position + se.SpawnPosition + (Vector3.up * ScrollRate * se.SpawnTime);
                if (se.ShipPrefab)
                {
                    var behavior = se.ShipPrefab.GetComponent<AbstractEnemyBehavior>();
                    if(behavior)
                    {
                        behavior.DrawGizmos(position);
                        continue;
                    }
                }
                Gizmos.color = Color.red;
                Gizmos.DrawLine(position + new Vector3(1, 1, 0), position + new Vector3(-1, -1, 0));
                Gizmos.DrawLine(position + new Vector3(1, -1, 0), position + new Vector3(-1, 1, 0));
            }
        }

        // Draw level borders, for convenience
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-9, 5), new Vector3(-9, 5 + ScrollRate * LevelEndTime));
        Gizmos.DrawLine(new Vector3(9, 5), new Vector3(9, 5 + ScrollRate * LevelEndTime));
    }
}
