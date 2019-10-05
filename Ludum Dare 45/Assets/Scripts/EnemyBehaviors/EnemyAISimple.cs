using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAISimple : AbstractEnemyBehavior
{
    public Vector2 StartingVelocity = Vector2.down;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetComponent<AbstractShipDescriptor>().VelocityVector = StartingVelocity;
    }

    public override void DrawGizmos(Vector3 position)
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(position, 0.5f);
        Gizmos.DrawRay(position, new Vector3(StartingVelocity.x, StartingVelocity.y, 0) * GetComponent<AbstractShipDescriptor>().MaxShipSpeed);
    }
}
