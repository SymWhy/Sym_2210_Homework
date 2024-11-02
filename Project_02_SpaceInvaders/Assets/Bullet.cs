using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {        
        GetComponent<Rigidbody2D>().velocity = transform.up * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Wall") {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Cover") {
            Destroy(this.gameObject);
        }
    }
}
