using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Splash_Screen splashScreen;

    private UI_Manager _ui;

    public bool _isGameOver;

    private bool _gameHasStarted;

    private Spawn_Manager _sm;
    int l;

    void Start()
    {
        _isGameOver = false;
        _gameHasStarted = false;
        _ui = GameObject.Find("UI_Manager").GetComponent<UI_Manager>();
        splashScreen = GameObject.Find("Splash_Screen").GetComponent<Splash_Screen>();
        _sm = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        l = player.GetComponent<Player>().life;
        if(_ui != null)
        {
            Debug.Log("Found UI Manager");
        }
        if(splashScreen != null)
        {
            Debug.Log("Found Splash_Screen Game Object");
        }
        if(_sm != null)
        {
            Debug.Log("Found Spawn Manager Successfully");
        }
        Debug.Log("Life is " + l);
    }

    // Update is called once per frame
    void Update()
    {
        checkEsc();
        checkGameOver();
    }

    //Function to check if Esc Key has been pressed by player
    //If it has, pause the game play
    //And Display Splash Screen
    private void checkEsc()
    {
        if (splashScreen != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                splashScreen.toggleSplash();
                if(splashScreen.isSplash)
                {
                    Time.timeScale = 0;
                    _ui.hideEverything();
                    _sm.stopSpawner();
                    GameObject.Find("Play_Button").GetComponent<Button>().interactable = false;
                }
                else
                {
                    Time.timeScale = 1;
                    _ui.showEverything();
                    _sm.startSpawner();
                }

            }
        }
    }  

    //Function to check if Game is Over.
    //If it is, Display Splash Screen
    //Pause all Gameplay
    public void checkGameOver()
    {
        if ((_isGameOver) && (! splashScreen.isSplash))
        {
            _sm.stopSpawner();
            splashScreen.toggleSplash();
            _ui.hideEverything();
            _gameHasStarted = true;
            //Time.timeScale = 0;
            GameObject.Find("Play_Button").GetComponent<Button>().interactable = true;
        }
    }

    //Function which is Called every Frame
    //Checks to see if Game is Started from Splash Screen
    public void startGame()
    {
        if(!_gameHasStarted)
        {
            Debug.Log("Button WORKS");
            Time.timeScale = 1;
            _isGameOver = false;
            _gameHasStarted = true;
            splashScreen.toggleSplash();
            GameObject p=Instantiate(player);
            p.SetActive(true);
            _ui.initializeGame();
            _sm.startSpawner();
        }
        else
        {
            if (_isGameOver && (splashScreen.isSplash))
            {
                Debug.Log("Restart Successfull");
                GameObject p = (GameObject)Instantiate(player);
                p.SetActive(true);
                _ui.showEverything();
                Time.timeScale = 1;
                _isGameOver = false;
                _gameHasStarted = true;
                _ui.initializeGame();
                splashScreen.toggleSplash();
                _sm.startSpawner();

            }
        }
    }

    //Function to exit game using button
    public void exitGame()
    {
        Application.Quit();
    }
}
