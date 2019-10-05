using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannon : AbstractPlayerWeapon
{
    public AbstractPlayerBullet playerBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    protected override void Fire()
    {
        var bullet = Instantiate(playerBullet, this.transform.position, this.transform.rotation);
    }

}
