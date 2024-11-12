using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    public static GameManager GetGameManager() {
        if(_Instance == null)
        {
        _Instance = FindObjectOfType<GameManager>();
        }
        return _Instance;
    }

    private int PlayerHitCounter = 0;
    private int EnemyHitCounter = 0;
    public List<Alien> ListOfAliens;
    public int Aliens = 36;

    public AudioSource ExplosionSoundSource;
    // Start is called before the first frame update
    void Start()
    {
        ExplosionSoundSource = GetComponent<AudioSource>();

        Alien[] arrayOfAliens = FindObjectsOfType<Alien>();
        
        ListOfAliens.AddRange(arrayOfAliens);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            Debug.Log(Aliens);
        }
    }

    public void PlayExplosion() {
        ExplosionSoundSource.Play();
    }

    public void PlayerHit() {
        PlayerHitCounter++;
        Debug.Log("Player hit count: " + PlayerHitCounter);
    }

    public void EnemyHit() {
        EnemyHitCounter++;
        Debug.Log("Enemy hit count: " + EnemyHitCounter);
    }

    public void RemoveAlien(Alien alienToRemove) {
        ListOfAliens.Remove(alienToRemove);
        Aliens--;
    }


}


//create a list of all the aliens
//when rmb pressed, cycle through the list and pick an alien at random
//every time an alien is killed, remove that alien from the list