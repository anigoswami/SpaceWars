using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private GameObject[] PowerUpPrefab;
    [SerializeField]
    private GameObject asteroid;
    
    public float difficulty;

    private Splash_Screen splash;
    private void Start()
    {
        difficulty = 7.0f;
        splash = GameObject.Find("Splash_Screen").GetComponent<Splash_Screen>();
        //player.SetActive(true);
    }
    IEnumerator EnemySpawnerCoRoutine()
    {
        while (!splash.isSplash)
        {
            Instantiate(EnemyPrefab, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(difficulty);
        }
    }

    IEnumerator powerUpSpawnerCoRoutine()
    {
        while (!splash.isSplash)
        {
            int PowerUp = Random.Range(0, 3);
            Vector3 randomPosition = new Vector3(Random.Range(-7f, 7f), 7, 0);
            Instantiate(PowerUpPrefab[PowerUp], randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(1.5f*difficulty);
        }
    }

    IEnumerator asteroidSpawnerCoRoutine()
    {
        while (!splash.isSplash)
        {
            Instantiate(asteroid, new Vector3(Random.Range(-7f, 7f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(2.0f*difficulty);
        }
    }

    public void Update()
    {
        if (splash.isSplash)
        {
            EnemyPrefab.SetActive(false);
            asteroid.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                PowerUpPrefab[i].SetActive(false);
            }
        }
        else
        {
            EnemyPrefab.SetActive(true);
            asteroid.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                PowerUpPrefab[i].SetActive(true);
            }
        }
    }

    public void startSpawner()
    {
        StartCoroutine(EnemySpawnerCoRoutine());
        StartCoroutine(powerUpSpawnerCoRoutine());
        StartCoroutine(asteroidSpawnerCoRoutine());
    }
    public void stopSpawner()
    {
        StopCoroutine(asteroidSpawnerCoRoutine());
        StopCoroutine(powerUpSpawnerCoRoutine());
        StopCoroutine(EnemySpawnerCoRoutine());
    }
}
