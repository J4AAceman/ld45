using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyBehavior : MonoBehaviour
{
    protected const float KillLinePY = 10.0f;
    protected const float KillLineNY = -7.0f;
    protected const float KillLinePX = 12.0f;
    protected const float KillLineNX = -12.0f;

    private List<AbstractWeapon> shipWeaponList;

    private void Awake()
    {
        shipWeaponList = gameObject.GetComponent<AbstractShipDescriptor>().ShipWeaponList;
    }

    protected virtual void FixedUpdate()
    {
        // If the ship has left the playable area (or somehow passed it), kill it
        if (transform.position.y < KillLineNY)
        {
            Destroy(gameObject);
        }

        foreach (var w in shipWeaponList)
        {
            w.ShouldFire = true;
        }
    }

    public virtual void DrawGizmos(Vector3 position)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(position, 0.125f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(KillLinePX, KillLinePY), new Vector3(KillLinePX, KillLineNY));
        Gizmos.DrawLine(new Vector3(KillLineNX, KillLinePY), new Vector3(KillLineNX, KillLineNY));
        Gizmos.DrawLine(new Vector3(KillLinePX, KillLinePY), new Vector3(KillLineNX, KillLinePY));
        Gizmos.DrawLine(new Vector3(KillLinePX, KillLineNY), new Vector3(KillLineNX, KillLineNY));
    }
}
