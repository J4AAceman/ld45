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

}
