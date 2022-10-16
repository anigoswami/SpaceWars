using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triple_Shot_Destroy_OOB : MonoBehaviour
{
    [SerializeField]
    private float speed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (this.transform.position.y > 6.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
