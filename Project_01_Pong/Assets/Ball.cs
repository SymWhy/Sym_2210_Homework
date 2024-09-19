using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Timers;

public class Ball : MonoBehaviour
{
    public Rigidbody2D r2d;
    public Vector2 LocalVelocity;
    public Vector3 StartPos;
    public float Speed = 5.0f; 

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "BallA") {
            ResetBall();
        }        
    }

    // Update is called once per frame
    void Update()
    {
        r2d.velocity = LocalVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision!");

        //Vector2.reflect() returns reflected velocity, LocalVelocity still needs
        //to be updated
        LocalVelocity = Vector2.Reflect(

            LocalVelocity,

            //get info from collision ie normal, point of contact
            collision.GetContact(0).normal);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Hit Trigger");
        bool IsLeft = collision.gameObject.name == "LeftGoal";
        GameObject.FindObjectOfType<GameManager>().IncrementScore(IsLeft);
        ResetBall();
    }

    public void ResetBall()
    {
        gameObject.SetActive(true);
        transform.position = StartPos;
        r2d.GetComponent<Rigidbody2D>();
        r2d.velocity = new Vector2(Random.Range(-1f, 1f), (Random.Range(-1f, 1f)));
        r2d.velocity.Normalize();
        LocalVelocity = r2d.velocity * Speed;
    }
}

//---multiball---
//on game start(set timer)
//if timer > 30 seconds {BallB.ResetBall()}
//if timer > 60 seconds {BallC.ResetBall()}

//grab current time in start
//on update compare current time to 10sec later

//---ball smack---
//when fire pressed
//paddle.transform forward x (this prevents spam by providing an opportunity to get past the paddle)
//(if this doesnt accelerate the ball) multiply ball acceleration by 5