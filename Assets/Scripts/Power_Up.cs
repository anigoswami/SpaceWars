using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Up : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;

    [SerializeField]
    //0=triple shot; 1 = speed; 2 = shield;
    private int powerUpID;

    [SerializeField]
    private AudioClip _picked;

    private Game_Manager _gm;

    private void Start()
    {
        _gm = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
        if (_gm._isGameOver)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_picked, Camera.main.transform.position, 0.05f);
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                if (powerUpID == 0)
                {
                    player.EnableTripleShot();
                }
                else if (powerUpID == 1)
                {
                    player.EnableSpeed();
                }
                else if (powerUpID == 2)
                {
                    Debug.Log("Shieldpowerup");
                    player.EnableShield();

                }

            }
            Destroy(this.gameObject);
        }
    }

}
