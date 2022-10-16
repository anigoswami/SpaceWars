using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] LifeHUD;
    [SerializeField]
    private Image currentLifeHUD;
    [SerializeField]
    private GameObject Score;
    private int currentscore;
    [SerializeField]
    private GameObject player;

    private Game_Manager _gm;
    void Start()
    {
        currentscore = 0; 
        currentLifeHUD.gameObject.SetActive(false);
        Score.SetActive(false);
        _gm = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
    }

    public void initializeGame()
    {
        currentscore = 0;
        Score.GetComponent<TMP_Text>().text = "SCORE: " + currentscore;
        currentLifeHUD.sprite = LifeHUD[player.GetComponent<Player>().life];
        currentLifeHUD.gameObject.SetActive(true);
        Score.SetActive(true);
        player.transform.position = Vector3.zero;
    }
    public void hideEverything()
    {
        currentLifeHUD.gameObject.SetActive(false);
        Score.SetActive(false);
        player.SetActive(false);
    }
    public void showEverything()
    {
        currentLifeHUD.gameObject.SetActive(true);
        Score.SetActive(true);
        player.SetActive(true);
    }
    public void updateScore(int pts)
    {
        currentscore += pts;
        Score.GetComponent<TMP_Text>().text = "SCORE: " + currentscore;
    }
    public void updateLives(int currentLife)
    {
        currentLifeHUD.sprite = LifeHUD[currentLife];
        if (currentLife == 0)
        {
            StartCoroutine(gameover());
        }
    }
    IEnumerator gameover()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        _gm._isGameOver = true;

    }
}
