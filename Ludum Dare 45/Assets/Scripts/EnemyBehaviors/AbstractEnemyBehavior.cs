using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyBehavior : MonoBehaviour
{
    protected const float KillLine = -10.0f;

    protected virtual void FixedUpdate()
    {
        // If the ship has left the playable area (or somehow passed it), kill it
        if (transform.position.y < KillLine)
        {
            Destroy(gameObject);
        }
    }
}
