using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    private Rigidbody2D MyRigidbody2D;
    public float Speed = 1.0f;
    private Vector3[] Directions = {Vector3.zero, Vector3.right, Vector3.down, Vector3.left};
    public int CurrentDirection = 0;
    public float timeToWait = 1.0f;
    public GameObject BulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>(); 
        CurrentDirection = 1;

        
    }

    // Update is called once per frame
    void Update()
    {
        //get time elapsed
        float timeSinceLastFrame = Time.deltaTime;

        //multiply directional vector by elapsed time
        Vector3 translation = Directions[CurrentDirection] * timeSinceLastFrame * Speed;

        //translate the sprite by this vector
        transform.Translate(translation);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("Trigger triggered!");
        if (collision.gameObject.tag == "Buffer") {
            if (CurrentDirection == 1) {
                StartCoroutine(waitThenMoveLeft());
            }
            if (CurrentDirection == 3) {
                StartCoroutine(waitThenMoveRight());
            }
            //Debug.Log("Setting direction to down!");
            CurrentDirection = 2;
        }
    }

    public void Fire() {
        GameObject myBullet = GameObject.Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, 180));
    }

    public void KillMe() {
        GameManager.GetGameManager().PlayExplosion();
        Destroy(this.gameObject);
    }

    IEnumerator waitThenMoveLeft() {
        yield return new WaitForSecondsRealtime(timeToWait);
        CurrentDirection = 3;
    }

    IEnumerator waitThenMoveRight() {
        yield return new WaitForSecondsRealtime(timeToWait);
        CurrentDirection = 1;
    }
}
