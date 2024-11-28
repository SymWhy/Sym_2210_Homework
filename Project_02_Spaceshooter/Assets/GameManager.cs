using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum lets you pick one from multiple options
public enum GameState {
    GAME_BEGIN,
    GAME_PLAY,
    GAME_LOSE,
    GAME_WIN
}

public class GameManager : MonoBehaviour
{
    public float yRange;
    public float xRange;
    public GameObject AsteroidPrefab;
    private List<Asteroid> Asteroids = new List<Asteroid>();
    static GameManager _Instance;
    public GameObject WinStuff;
    public GameObject LoseStuff;
    public GameObject BeginStuff;
    private GameState CurrentState;
    public GameObject ShipPrefab;
    public bool ShipInGame;
    public GameState GetGameState() {
        return CurrentState;
    }
    public static GameManager GetGameManager() {
        if (_Instance == null){
            _Instance = GameObject.FindObjectOfType<GameManager>();
        }
        return _Instance;
    }
    private int Score = 0;
    
    // Start is called before the first frame update
    void Start()
    {        
        Setup();
        ChangeState(GameState.GAME_BEGIN);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) {
            Vector3 cameraBoundCalc = new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0f);
            GameObject myAsteroid = GameObject.Instantiate(AsteroidPrefab, cameraBoundCalc, Quaternion.Euler(0f, 0f, 90f));
            Asteroids.Add(myAsteroid.GetComponent<Asteroid>());
            //Debug.Log("Adding asteroid...");
        }
        switch(CurrentState) {
            case GameState.GAME_BEGIN: 
                Debug.Log("In BEGIN state.");
                break;
            case GameState.GAME_PLAY: 
                if (ShipInGame = null) {
                    ChangeState(GameState.GAME_LOSE);
                }
                break;
            case GameState.GAME_LOSE: 
                Debug.Log("In LOSE state.");
                break;
            case GameState.GAME_WIN: 
                Debug.Log("In WIN state.");
                break;
            
        }
    }
    public void StartGame() {
        ChangeState(GameState.GAME_PLAY);
    }

//END first then BEGIN
    public void ChangeState(GameState NewState) {
        //Do something as the state ends
        switch(CurrentState) {
            case GameState.GAME_BEGIN: 
                break;
            case GameState.GAME_PLAY: 
                break;
            case GameState.GAME_LOSE: 
                break;
            case GameState.GAME_WIN: 
                break;
        }
        CurrentState = NewState;

        //Do something as the state begins
        switch(CurrentState) {
            case GameState.GAME_BEGIN: 
                BeginStuff.SetActive(true);
                WinStuff.SetActive(false);
                LoseStuff.SetActive(false);
                break;
            case GameState.GAME_PLAY: 
                BeginStuff.SetActive(false);
                WinStuff.SetActive(false);
                LoseStuff.SetActive(false);

                ShipInGame = GameObject.Instantiate(ShipPrefab);
                GameObject.Instantiate(AsteroidPrefab, new Vector3(-4f, -4f, 0f), Quaternion.identity); 
                break;
            case GameState.GAME_LOSE: 
                LoseStuff.SetActive(true);
                break;
            case GameState.GAME_WIN: 
                WinStuff.SetActive(true);
                break;
        }
        CurrentState = NewState;
    }

    private void Setup() {
        //calculate the size of the game
        yRange = Camera.main.orthographicSize;
        xRange = yRange * Camera.main.aspect;
        yRange /= 2;
        xRange /= 2;
    }

    public void IncrementScore() {
        Score++;
        Debug.Log(Score);
    }

//static makes this available in the GameManager library
    public static float tween(float x) {
        return x * x; //check the github for different easing functions
    }
}
