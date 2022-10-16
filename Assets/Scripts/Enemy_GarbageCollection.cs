using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GarbageCollection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 2.5f);
    }

    
}
