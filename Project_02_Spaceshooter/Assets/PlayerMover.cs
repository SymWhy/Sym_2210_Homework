using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    private Rigidbody2D MyRigidbody2D;
    public GameObject BulletPrefab;
    //private List<Bullet> Bullets = new List<Bullet>();
    public AudioSource BulletSoundSource;
    public GameObject ThrusterPoint;
    public GameObject GunPoint;
    public ParticleSystem Exhaust;


    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>(); 
        BulletSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal");
        float fullRotation = rotation * RotationSpeed * 100 * -1;
        MyRigidbody2D.MoveRotation(MyRigidbody2D.rotation + (fullRotation * Time.deltaTime));

        if(Input.GetAxis("Vertical") > 0f)
        {
            //Debug.Log("Vector is : " + (transform.forward * (Speed * Input.GetAxis("Vertical"))));
            MyRigidbody2D.AddForce(transform.up * (Speed * Input.GetAxis("Vertical")));
            Exhaust.Emit(1);

            RaycastHit2D hit = Physics2D.Raycast(ThrusterPoint.transform.position, -transform.up);
            if (hit) {
                Debug.Log("I hit a " + hit.collider.gameObject.name);
                Debug.DrawRay(transform.position, -transform.up * 3.0f);
                hit.rigidbody.AddForce(-transform.up * 3.0f);
            }
        }
        
        if(Input.GetButtonDown("Fire1")) {
            //Debug.Log("Space Pressed!");

            //Instantiate<object type>(Object to build, potition, rotation)
            GameObject myBullet = GameObject.Instantiate(BulletPrefab, transform.position, transform.rotation);
            //Bullets.Add(myBullet.GetComponent<Bullet>());
            BulletSoundSource.Play();
        }

        if(Input.GetButton("Fire2")) {
            RaycastHit2D hitAsteroid = Physics2D.Raycast(GunPoint.transform.position, transform.up * 10.0f);
            if (hitAsteroid) {
                        if (hitAsteroid.collider.gameObject.CompareTag("Asteroid")) {
                            Debug.Log("Raycast hit an asteroid!");
                            GameObject myBullet = GameObject.Instantiate(BulletPrefab, transform.position, transform.rotation);
                        }
            }
        }

        // if(Input.GetKeyDown(KeyCode.X)) {
        //     foreach(Bullet b in Bullets) {
        //         Destroy(b.gameObject);
        //         //you need to purge the array too or you will get an error
        //     }
        //     Bullets.Clear();
        // Debug.Log("Bullet list purged!");
        // }
    }
}
