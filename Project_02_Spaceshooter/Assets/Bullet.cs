using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 5.0f;
    //time is generally done as seconds as a float
    public float LifeTime = 1.0f;
    private bool BulletLifetimeCheck = false;
    private float StartTime;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * Speed;
        StartTime = Time.time;
        BulletLifetimeCheck = true;
        //Invoke("function to invoke", relative time to invoke in seconds)
        Invoke("DestroyMe", LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        // if(BulletLifetimeCheck && (StartTime + LifeTime < Time.time)) {
        //     BulletLifetimeCheck = false;
        //     DestroyMe(); 
        // }
    }

    void DestroyMe() {
        Destroy(this.gameObject);
        BulletLifetimeCheck = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "WallL" || collision.gameObject.name == "WallR" || collision.gameObject.name == "WallT" || collision.gameObject.name == "WallB") {
            Destroy(this.gameObject);
        }
    }
}
