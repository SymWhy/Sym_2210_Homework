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

    public AudioSource ExplosionSoundSource;
    // Start is called before the first frame update
    void Start()
    {
        ExplosionSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            Alien[] listOfAliens = FindObjectsOfType<Alien>();
            Debug.Log(listOfAliens[0]);
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
}
