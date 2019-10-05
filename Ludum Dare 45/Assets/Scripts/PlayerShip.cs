using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private Vector2 velocityVector;
    public float MaxShipSpeed = 12f;
    public List<AbstractPlayerWeapon> ShipWeaponList;

    public float MaxHealth = 100.0f;
    public float CurrentHealth;

    private Rigidbody2D ShipRigidBody;

    public Vector2 VelocityVector { get => velocityVector; set => velocityVector = value.normalized * MaxShipSpeed; }

    private void Awake()
    {
        ShipRigidBody = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ShipRigidBody.velocity = VelocityVector;

        if(CurrentHealth <= 0)
        {
            // TODO: Handle death better
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            // TODO
        }
        else if (otherObject.layer == LayerMask.NameToLayer("EnemyBullets"))
        {
            AbstractEnemyBullet bullet = otherObject.GetComponent<AbstractEnemyBullet>();
            CurrentHealth -= bullet.GetDamage();
        }
    }
}
