using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAITurnOverTime : AbstractEnemyBehavior
{
    public Vector2 StartingVelocity = Vector2.down;
    public Vector2 FinalVelocity = Vector2.right;
    public float InterpolationTime = 2.0f;
    public float TurnDelay = 1.0f;

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

        TurnDelay = Mathf.Max(0, TurnDelay - Time.deltaTime);

        if (TurnDelay == 0)
        {
            ship.VelocityVector = Vector2.Lerp(FinalVelocity, StartingVelocity, InterpolationTime);
            InterpolationTime = Mathf.Max(0, InterpolationTime - Time.deltaTime);
        }
    }

    public override void DrawGizmos(Vector3 position)
    {
        Vector3 turnStartPosition = position + new Vector3(StartingVelocity.x, StartingVelocity.y, 0) * GetComponent<AbstractShipDescriptor>().MaxShipSpeed * TurnDelay;

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(position, 0.5f);
        Gizmos.DrawRay(position, new Vector3(StartingVelocity.x, StartingVelocity.y, 0) * GetComponent<AbstractShipDescriptor>().MaxShipSpeed * TurnDelay);
        Gizmos.DrawRay(turnStartPosition, new Vector3(FinalVelocity.x, FinalVelocity.y, 0) * GetComponent<AbstractShipDescriptor>().MaxShipSpeed * InterpolationTime);
    }
}
