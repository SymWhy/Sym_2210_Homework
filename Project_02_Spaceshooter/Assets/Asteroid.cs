using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Asteroid : MonoBehaviour {

    public AudioSource AsteroidSoundSource;
    public AudioClip ExplosionClip;
    public PlayerMover PlayerShip;
    public Vector3 PlayerPos;

    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Spawning asteroid...");
        AsteroidSoundSource = GetComponent<AudioSource>();
        PlayerShip = GameObject.FindObjectOfType<PlayerMover>();
        PlayerPos = PlayerShip.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = PlayerPos * Speed;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Bullet(Clone)") {
            //Debug.Log("Destroying asteroid...");
            AsteroidSoundSource.Play();
            Destroy(collision.gameObject);
            //Destroy(gameObject);
            StartCoroutine(waitToSplode());
        }

        if(collision.gameObject.name == "Ship") {
            //GetComponent<Script Containing The Function You Want To Call>.Function You Want To Call();
            //GetComponent gets a script you want to use
            collision.gameObject.GetComponent<FlashingSprite>().StartFlash();
            AsteroidSoundSource.Play();
            //Destroy(collision.gameObject);
        }
    }

    //coroutine
    IEnumerator waitToSplode() {
        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
    }

}
