using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Rigidbody2D MyRigidbody2D;
    public float Speed;
    public GameObject BulletPrefab;
    public AudioSource BulletSoundSource;

    
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float rawHorizontalAxis = Input.GetAxisRaw("Horizontal");
        Vector3 direction = Vector3.zero;
        direction.x = rawHorizontalAxis;
        float timeSinceLastFrame = Time.deltaTime;
        Vector3 translation = direction * timeSinceLastFrame;
        translation = translation * Speed;
        transform.Translate(translation);

        if(Input.GetButtonDown("Fire1")) {

            //Instantiate<object type>(Object to build, potition, rotation)
            GameObject myBullet = GameObject.Instantiate(BulletPrefab, transform.position, transform.rotation);
            BulletSoundSource.Play();
        }
    }
}
