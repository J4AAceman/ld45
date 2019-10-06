using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractShipDescriptor : MonoBehaviour
{
    private Vector2 velocityVector;
    public float MaxShipSpeed = 12.0f;
    public List<AbstractWeapon> ShipWeaponList;
    public GameObject Explosion;

    public float MaxHealth = 100.0f;
    public float CurrentHealth;

    public bool IsInvincible = false;

    private Rigidbody2D ShipRigidBody;

    public Vector2 VelocityVector { get => velocityVector; set => velocityVector = (value.magnitude > 1 ? value.normalized : value) * MaxShipSpeed; }

    protected virtual void Awake()
    {
        ShipRigidBody = GetComponent<Rigidbody2D>();
        InitializeShipStats();
    }

    private void FixedUpdate()
    {
        ShipRigidBody.velocity = VelocityVector;

        if (CurrentHealth <= 0)
        {
            // TODO: Handle death better
            if(Explosion)
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObject = collision.gameObject;
        DamageDealer dd = otherObject.GetComponent<DamageDealer>();

        if (dd && !IsInvincible)
        {
            CurrentHealth -= dd.GetDamage();
        }
    }

    public void InitializeShipStats()
    {
        CurrentHealth = MaxHealth;
    }

}
