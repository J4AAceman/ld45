using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockDescriptor : MonoBehaviour
{
    public float MaxHealth = 5.0f;
    public float CurrentHealth;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    private void FixedUpdate()
    {
        if (CurrentHealth <= 0)
        {
            // TODO: Handle death better
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObject = collision.gameObject;
        DamageDealer dd = otherObject.GetComponent<DamageDealer>();

        if (dd)
        {
            CurrentHealth -= dd.GetDamage();
        }
    }

}
