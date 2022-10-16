using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy_ExplosionPrefab;
    [SerializeField]
    private float speed = 10.0f;

    private Game_Manager _gm;
    [SerializeField]
    private AudioClip _expl;
    private UI_Manager _ui;
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        _ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        if (_ui != null)
        {
            Debug.Log("UI_Manager ACCESSED SUCCESSFULLY from Enemy AI");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (this.transform.position.y < -7.0f)
        {
            float xValue = Random.Range(-7.0f, 7.0f) * Random.value;
            this.transform.position = new Vector3(xValue, 7.0f, 0);
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
            _ui.updateScore(10);
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
        AudioSource.PlayClipAtPoint(_expl, Camera.main.transform.position,0.05f);
        Instantiate(Enemy_ExplosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
