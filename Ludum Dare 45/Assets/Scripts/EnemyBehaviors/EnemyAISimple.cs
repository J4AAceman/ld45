using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAISimple : MonoBehaviour
{
    public Vector2 StartingVelocity = Vector2.down;
    protected const float KillLine = -10.0f;
    //protected const float MaxHeath = 3.0f; // TODO: move this to a different script - doesn't actually belong here

    //private float currentHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetComponent<AbstractShipDescriptor>().VelocityVector = StartingVelocity;
        //currentHealth = MaxHeath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // If the ship has left the playable area (or somehow passed it), kill it
        if(transform.position.y < KillLine)
        {
            Destroy(gameObject);
        }

        // If reduced to 0 health, give the player credit (credits?) for the kill, and destroy this ship
        //if(currentHealth <= 0)
        //{
        //    KilledByPlayer();
        //}
    }

    //protected void KilledByPlayer()
    //{
    //    // Todo: Give the player some credits or something??

    //    // Now destroy this object
    //    Destroy(gameObject);
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    GameObject otherObject = collision.gameObject;
    //    if(otherObject.layer == LayerMask.NameToLayer("PlayerBullets"))
    //    {
    //        AbstractPlayerBullet bullet = otherObject.GetComponent<AbstractPlayerBullet>();
    //        currentHealth -= bullet.GetDamage();
    //    }
    //}
}
