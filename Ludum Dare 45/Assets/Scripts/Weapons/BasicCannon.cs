using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCannon : AbstractWeapon
{
    public AbstractBullet Bullet;

    protected override void Fire()
    {
        var bullet = Instantiate(Bullet, this.transform.position, this.transform.rotation);
        bullet.gameObject.layer = BulletLayer;
    }

}
