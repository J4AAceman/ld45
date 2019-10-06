using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAITurnOverTime : AbstractEnemyBehavior
{
    public Vector2 StartingVelocity = Vector2.down;
    public Vector2 FinalVelocity = Vector2.right;
    public float MaxInterpolationTime = 2.0f;
    public float TurnDelay = 1.0f;

    private float InterpolationTime;
    private AbstractShipDescriptor ship;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        ship = GetComponent<AbstractShipDescriptor>();
        ship.VelocityVector = StartingVelocity;
        InterpolationTime = MaxInterpolationTime;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        TurnDelay = Mathf.Max(0, TurnDelay - Time.deltaTime);

        if (TurnDelay == 0)
        {
            ship.VelocityVector = Vector2.Lerp(FinalVelocity, StartingVelocity, InterpolationTime / MaxInterpolationTime);
            InterpolationTime = Mathf.Max(0, InterpolationTime - Time.deltaTime);
        }
    }

    public override void DrawGizmos(Vector3 position)
    {
        Vector3 turnStartPosition = position + new Vector3(StartingVelocity.x, StartingVelocity.y, 0) * GetComponent<AbstractShipDescriptor>().MaxShipSpeed * (TurnDelay + 0.5f);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(position, 0.5f);
        Gizmos.DrawRay(position, new Vector3(StartingVelocity.x, StartingVelocity.y, 0) * GetComponent<AbstractShipDescriptor>().MaxShipSpeed * (TurnDelay + 0.5f)); // SUPER approximated - don't rely on this one
        Gizmos.DrawRay(turnStartPosition, new Vector3(FinalVelocity.x, FinalVelocity.y, 0) * GetComponent<AbstractShipDescriptor>().MaxShipSpeed * InterpolationTime);
    }
}
