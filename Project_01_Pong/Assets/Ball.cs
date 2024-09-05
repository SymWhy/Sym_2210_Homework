using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D r2d;
    public Vector2 LocalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        r2d.GetComponent<Rigidbody2D>();
        r2d.velocity = new Vector2(-3.0f, 1f);
        LocalVelocity = r2d.velocity;
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
}
