using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCannonBullet : AbstractBullet
{
    // Start is called before the first frame update
    protected override void Start()
    {
        StartingDirection = transform.rotation * Vector2.up;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
