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
        if (collision.gameObject.tag == "Bullet") {
            //Debug.Log("Destroying asteroid...");
            AsteroidSoundSource.Play();
            //Destroy(collision.gameObject);
            //Destroy(gameObject);
            StartCoroutine(waitToSplode(0.2f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "Ship") {
            //GetComponent<Script Containing The Function You Want To Call>.Function You Want To Call();
            //GetComponent gets a script you want to use
            collision.gameObject.GetComponent<FlashingSprite>().StartFlash();
            AsteroidSoundSource.Play();
            //Destroy(collision.gameObject);
        }
    }

    //coroutine
    IEnumerator waitToSplode(float TimeToShrink) {

        gameObject.GetComponent<Collider2D>().enabled = false;
        
        float currentTime = Time.timeSinceLevelLoad;
        float targetTime = currentTime + TimeToShrink;
        float loopTime = currentTime;

        while(loopTime < targetTime) {
            float t = Mathf.InverseLerp(currentTime, targetTime, loopTime);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, GameManager.tween(t));
            loopTime += Time.deltaTime;
            yield return null;
            
        }

        yield return new WaitForSecondsRealtime(1);
        Destroy(gameObject);
        GameManager.GetGameManager().IncrementScore();
    }

}
