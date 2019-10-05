using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AbstractShipDescriptor playerShip;
    public GameObject HackFX;
    public float MaxHackCooldown = 5.0f;

    private float currentHackCooldown;

    private void Awake()
    {
        if(!HackFX || !HackFX.GetComponent<LineRenderer>())
        {
            Debug.LogError("Must have a HackFX, with a LineRenderer attached");
        }
        currentHackCooldown = 0;
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
        bool shouldFire = Input.GetAxis("Fire Weapons") > 0.0f;
        bool stealShip = Input.GetAxis("Hack") > 0.0f;
        Vector2 velocity = new Vector2(horizontal, vertical);

        if (velocity.magnitude > 1)
        {
            velocity.Normalize();
        }

        playerShip.VelocityVector = velocity;

        foreach (var w in playerShip.ShipWeaponList)
        {
            w.ShouldFire = shouldFire;
        }

        currentHackCooldown = Mathf.Max(0, currentHackCooldown -= Time.deltaTime);

        if (stealShip && currentHackCooldown <= 0)
        {
            currentHackCooldown = MaxHackCooldown;
            // TODO: try to steal enemy ship

            // Raycast forward, hitting closest of either an enemy ship, or the level collider
            RaycastHit2D ray = Physics2D.Raycast(playerShip.transform.position, Vector2.up, 20, LayerMask.GetMask("LevelBoundary", "Enemies"));
            try
            {
                GameObject otherObject = ray.collider.gameObject;
                AbstractShipDescriptor newShip = otherObject.GetComponent<AbstractShipDescriptor>();
                if (newShip)
                {
                    // Steal ship

                    // Delete enemy behavior, but NOT THE OBJECT
                    Destroy(otherObject.GetComponent<AbstractEnemyBehavior>());

                    // Safe swap of "playerShip". Possibly not necessary, but just in case
                    var oldPlayerShip = playerShip;
                    playerShip = newShip;

                    // Switch layer of new ship
                    playerShip.gameObject.layer = LayerMask.NameToLayer("Player");

                    // Reset health of new ship
                    playerShip.InitializeShipStats(); 

                    // Now destroy the old ship
                    // TODO: make destruction animation for all ships
                    Destroy(oldPlayerShip.gameObject);
                }

                List<Vector3> boltPositions = new List<Vector3> { playerShip.transform.position, ray.point };
                // TODO: make bolt more jagged

                GameObject hackFX = Instantiate(HackFX);
                var lr = hackFX.GetComponent<LineRenderer>();
                lr.SetPositions(boltPositions.ToArray());
            }
            catch (Exception e)
            {
                Debug.LogWarning("Need to add safety to this raycast: " + e.Message);
            }
        }
    }

    private void FixedUpdate()
    {

    }
}
