using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AbstractShipDescriptor playerShip;

    private void Awake()
    {
        //velocity = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool shouldFire = Input.GetAxis("Fire1") > 0f;
        Vector2 velocity = new Vector2(horizontal, vertical);

        if(velocity.magnitude > 1)
        {
            velocity.Normalize();
        }

        //playerShipRigidBody.velocity = velocity * MaxPlayerSpeed;
        playerShip.VelocityVector = velocity;

        foreach (var w in playerShip.ShipWeaponList)
        {
            w.ShouldFire = shouldFire;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
