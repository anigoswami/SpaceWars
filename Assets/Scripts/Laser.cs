using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed = 15.0f;

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > 6.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
