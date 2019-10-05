﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyWeapon : MonoBehaviour
{
    protected const float CooldownTime = 0.2f;

    private float cooldown = 0;

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
        cooldown = Mathf.Max(0, cooldown - Time.deltaTime);

        if (cooldown <= 0)
        {
            Fire();
            cooldown = CooldownTime;
        }
    }

    abstract protected void Fire();
}
