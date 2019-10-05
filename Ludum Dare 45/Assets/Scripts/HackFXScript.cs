using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackFXScript : MonoBehaviour
{
    public float Lifetime = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, Lifetime);
    }

}
