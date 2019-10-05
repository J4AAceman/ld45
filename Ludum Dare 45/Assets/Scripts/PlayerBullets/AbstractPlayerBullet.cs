using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayerBullet : MonoBehaviour
{
    protected const float BulletLifetime = 2.0f;
    protected Vector2 StartingVelocity = Vector2.up * 10;
    protected const float BulletDamage = 1.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, BulletLifetime);
        this.GetComponent<Rigidbody2D>().velocity = StartingVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Target hit, destroy self. This will be overridden for piercing projectiles
        Destroy(gameObject);
    }

    public virtual float GetDamage()
    {
        return BulletDamage;
    }
}
