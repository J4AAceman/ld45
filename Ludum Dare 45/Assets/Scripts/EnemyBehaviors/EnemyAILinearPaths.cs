using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAILinearPaths : AbstractEnemyBehavior
{
    public Vector2 StartingVelocity = Vector2.down;
    public List<MovementSteps> Steps = new List<MovementSteps>();
    private int index = 0;

    private AbstractShipDescriptor ship;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ship = GetComponent<AbstractShipDescriptor>();
        ship.VelocityVector = StartingVelocity;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(index < Steps.Count)
        {
            Steps[index].TimeDelay -= Time.deltaTime;
            if(Steps[index].TimeDelay <= 0)
            {
                ship.VelocityVector = Steps[index].Direction;
                index++;
            }
        }
    }

    public override void DrawGizmos(Vector3 position)
    {
        // TODO
        ship = GetComponent<AbstractShipDescriptor>();
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(position, 0.5f);
        //Gizmos.DrawRay(position, new Vector3(StartingVelocity.x, StartingVelocity.y, 0) * GetComponent<AbstractShipDescriptor>().MaxShipSpeed);
        Vector3 currentPos = position;
        Vector3 newVelocity = StartingVelocity;
        foreach (var step in Steps)
        {
            Vector3 nextPos = currentPos + new Vector3(newVelocity.x, newVelocity.y) * step.TimeDelay * ship.MaxShipSpeed;
            Gizmos.DrawLine(currentPos, nextPos);
            currentPos = nextPos;
            newVelocity = step.Direction;
            //nextPos = currentPos + ;
        }
        Gizmos.DrawRay(currentPos, new Vector3(newVelocity.x, newVelocity.y) * ship.MaxShipSpeed);
    }


}
