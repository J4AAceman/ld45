using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MaxPlayerSpeed = 1f;
    public GameObject PlayerShip;
    public List<AbstractPlayerWeapon> PlayerWeaponList;

    private Transform playerShipTransform;
    //private Vector2 velocity;
    private Rigidbody2D playerShipRigidBody;

    private void Awake()
    {
        //velocity = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerShip)
        {
            playerShipTransform = PlayerShip.transform;
            playerShipRigidBody = PlayerShip.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogError("Missing PlayerShip");
        }

        if (!playerShipTransform)
        {
            Debug.LogError("Missing PlayerShip Transform");
        }
        if(!playerShipRigidBody)
        {
            Debug.LogError("Missing PlayerShip Rigidbody2D");
        }
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

        playerShipRigidBody.velocity = velocity * MaxPlayerSpeed;

        foreach(var w in PlayerWeaponList)
        {
            w.ShouldFire = shouldFire;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
