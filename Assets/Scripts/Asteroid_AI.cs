using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_AI : MonoBehaviour
{
    [SerializeField]
    private GameObject ExplosionPrefab;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private AudioClip _expl;
    private Game_Manager _gm;
    private UI_Manager _ui;
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        _ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        if (_ui != null)
        {
            Debug.Log("UI_Manager ACCESSED SUCCESSFULLY");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, 0.1f);
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (this.transform.position.y < -7.0f)
        {
            Destroy(this.gameObject);
        }
        if (_gm._isGameOver)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser")
        {
            _ui.updateScore(15);
            if (collision.transform.parent != null)
            {
                Destroy(collision.transform.parent.gameObject);
            }
            Destroy(collision.gameObject);
            DestroyObject();
        }
        else if (collision.tag == "Player")
        {
            _ui.updateScore(-5);
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            DestroyObject();
        }
        else if (collision.tag == "Shield")
        {
            Player player = collision.transform.parent.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        AudioSource.PlayClipAtPoint(_expl, Camera.main.transform.position, 0.05f);
        Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
