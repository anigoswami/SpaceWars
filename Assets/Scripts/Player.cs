using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //variables to determine player's behaviour
    private float speed;
    private float fireRate = 0.25f;
    private float canFire = 0.0f;
    private int hit;
    public int life;
    

    //variables to determine player enhancements
    [SerializeField]
    private bool canTripleShot = false;
    [SerializeField]
    private bool canBoostSpeed = false;
    [SerializeField]
    private bool shield = false;
    

    //visual elements associated with player
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject tripleShotPrefab;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private GameObject boost;
    [SerializeField]
    private GameObject[] _enginefail;

    //Audio elements associated with player
    private AudioSource _laser;

    //variables required to communicate with other scripts
    private UI_Manager _ui;
    private Game_Manager _gm;

    private void Start()
    {
        hit = Random.Range(0,2);
        life = 3;
        //this.gameObject.SetActive(false);
        _ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        _gm = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
        if(_gm != null)
        {
            Debug.Log("Found Game_Manager from player");
        }
        _laser = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            LaserShoot();
        }
        ShieldActive();
        //_gm.checkGameOver();
    }

    private void movePlayer()
    {
        if (canBoostSpeed)
        {
            boost.SetActive(true);
            speed = 28.0f;
        }
        else
        {
            boost.SetActive(false);
            speed = 10.0f;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

        if (transform.position.y < -5.62f)
        {
            transform.position = new Vector3(transform.position.x, 5.62f, 0);
        }
        else if (transform.position.y > 5.63f)
        {
            transform.position = new Vector3(transform.position.x, -5.63f, 0);
        }
        else if (transform.position.x > 9.45f)
        {
            transform.position = new Vector3(-9.45f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.45f)
        {
            transform.position = new Vector3(9.45f, transform.position.y, 0);
        }
    }
    private void LaserShoot()
    {

        if (Time.time > canFire)
        {
            _laser.Play();
            if (canTripleShot)
            {
                Instantiate(tripleShotPrefab, this.transform.position+ new Vector3(0, 0.88f, 0), Quaternion.identity);

            }
            else
            {
                Instantiate(laserPrefab, this.transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            canFire = Time.time + fireRate;
        }
    }
    private void ShieldActive()
    {
        if (shield)
        {
            shieldPrefab.SetActive(true);
        }
        else
        {
            shieldPrefab.SetActive(false);
        }
    }
    public void Damage()
    {
        if (shield)
        {
            shield = false;
            shieldPrefab.SetActive(false);
        }
        else
        {
            if(hit == 0)
            {
                hit++;
                _enginefail[0].SetActive(true);
            }
            else if(hit == 1)
            {
                hit--;
                _enginefail[1].SetActive(true);
            }
            life--;
            _ui.updateLives(life);
            if (life < 1)
            {
                Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                
            }
        }
    }
    
    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canTripleShot = false;
    }

    private IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canBoostSpeed = false;
    }
    private IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(25.0f);

        shield = false;

    }
    public void EnableTripleShot()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void EnableSpeed()
    {
        canBoostSpeed = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    public void EnableShield()
    {
        shield = true;
        StartCoroutine(ShieldPowerDownRoutine());

    }
}
