using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Ship PlayerShip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            GameManager.GetGameManager().EnemyHit();
            PlayerShip.GetComponent<Ship>().BulletOut = false;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
