using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float yRange;
    public float xRange;
    public GameObject AsteroidPrefab;
    private List<Asteroid> Asteroids = new List<Asteroid>();
    
    // Start is called before the first frame update
    void Start()
    {        
        Setup();
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
    }

    private void Setup() {
        //calculate the size of the game
        yRange = Camera.main.orthographicSize;
        xRange = yRange * Camera.main.aspect;
        yRange /= 2;
        xRange /= 2;
    }
}
