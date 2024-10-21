using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed;
    public GameObject CoverPrefab;
    public GameObject EnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Buffer" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Cover") {
            Destroy(this.gameObject);
        }
    }
}
