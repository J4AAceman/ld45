using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICore : AbstractShipDescriptor
{
    protected override void Awake()
    {
        base.Awake();
        MaxShipSpeed = 0;
    }
}
