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
    private const float MaxControlDisableTime = 1.0f;
    private float FireDisabledTime = 0.0f;

    private const float MaxInvincibilityTime = 2.0f;
    private float InvincibilityTime = 0.0f;

    private Quaternion from_rot = Quaternion.identity;
    private Quaternion to_rot = Quaternion.identity;

    private void Awake()
    {
        if(!HackFX || !HackFX.GetComponent<LineRenderer>())
        {
            Debug.LogError("Must have a HackFX, with a LineRenderer attached");
        }
        currentHackCooldown = 0;
        to_rot = playerShip.transform.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (FireDisabledTime > 0)
        {
            // Update timers
            FireDisabledTime = Mathf.Max(0, FireDisabledTime - Time.deltaTime);

            // Slerp to new rotation
            playerShip.transform.rotation = Quaternion.Slerp( to_rot, from_rot, FireDisabledTime / MaxControlDisableTime);
        }

        DoControls();

        InvincibilityTime = Mathf.Max(0, InvincibilityTime - Time.deltaTime);
        if (InvincibilityTime == 0)
        {
            playerShip.IsInvincible = false;
        }
    }

    private void DoControls()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool shouldFire = Input.GetAxis("Fire Weapons") > 0.0f && FireDisabledTime == 0.0f;
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
            // Try to steal enemy ship
            currentHackCooldown = MaxHackCooldown;

            // Raycast forward, hitting closest of either an enemy ship, or the level collider
            RaycastHit2D ray = Physics2D.Raycast(playerShip.transform.position, Vector2.up, 20, LayerMask.GetMask("LevelBoundary", "Enemies"));
            try
            {
                if (ray.collider)
                {
                    GameObject otherObject = ray.collider.gameObject;
                    AbstractShipDescriptor newShip = otherObject.GetComponent<AbstractShipDescriptor>();
                    if (newShip)
                    {
                        // Steal ship

                        // Make new ship temporarily invincible
                        newShip.IsInvincible = true;
                        InvincibilityTime = MaxInvincibilityTime;

                        // Disable controls
                        FireDisabledTime = MaxControlDisableTime;

                        // Delete enemy behavior, but NOT THE OBJECT
                        var behavior = otherObject.GetComponent<AbstractEnemyBehavior>();
                        if (behavior)
                        {
                            Destroy(behavior);
                        }

                        // Safe swap of "playerShip". Possibly not necessary, but just in case
                        var oldPlayerShip = playerShip;
                        playerShip = newShip;

                        // Switch ship's Rigidbody2D to Dynamic
                        playerShip.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                        // Grab current rotation to Slerp from
                        from_rot = playerShip.transform.rotation;

                        // Switch layer of new ship
                        playerShip.gameObject.layer = LayerMask.NameToLayer("Player");

                        // Reset health of new ship
                        playerShip.InitializeShipStats();

                        // Preserve ship through level-load
                        DontDestroyOnLoad(playerShip.gameObject);

                        // Set layer for all weapons' bullets
                        foreach (var weapon in playerShip.ShipWeaponList)
                        {
                            weapon.BulletLayer = LayerMask.NameToLayer("PlayerBullets");
                        }

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
