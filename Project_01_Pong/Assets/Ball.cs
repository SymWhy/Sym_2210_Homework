using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D r2d;
    public Vector2 LocalVelocity;
    public Vector3 StartPos;
    public float Speed = 5.0f; 

    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
    }

    // Update is called once per frame
    void Update()
    {
        r2d.velocity = LocalVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision!");

        //Vector2.reflect() returns reflected velocity, LocalVelocity still needs
        //to be updated
        LocalVelocity = Vector2.Reflect(

            LocalVelocity,

            //get info from collision ie normal, point of contact
            collision.GetContact(0).normal);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit Trigger");
        
        ResetBall();
    }

    private void OnTriggerStay2D(Collider2D collision){

    }

    public void ResetBall()
    {
        transform.position = StartPos;
        r2d.GetComponent<Rigidbody2D>();
        r2d.velocity = new Vector2(Random.Range(-1f, 1f), (Random.Range(-1f, 1f)));
        r2d.velocity.Normalize();
        LocalVelocity = r2d.velocity * Speed;
    }

}
