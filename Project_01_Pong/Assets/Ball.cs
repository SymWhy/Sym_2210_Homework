using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Ball : MonoBehaviour
{
    public Rigidbody2D r2d;
    public Vector2 LocalVelocity;
    public Vector3 StartPos;
    public float Speed = 5.0f; 
    public int BallCount = 1;
    public System.Timers.Timer GameTimer;

    // Start is called before the first frame update
    void Start()
    {
        ResetBall();
        GameTimer = new System.Timers.Timer();
        //directing to the function "WhenElapsed", no parentheses
        GameTimer.Elapsed += OnTimedEvent;
        GameTimer.Interval = 30000;
        GameTimer.AutoReset = true;
        GameTimer.Enabled = true;
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

    public void ResetBall()
    {
        transform.position = StartPos;
        r2d.GetComponent<Rigidbody2D>();
        r2d.velocity = new Vector2(Random.Range(-1f, 1f), (Random.Range(-1f, 1f)));
        r2d.velocity.Normalize();
        LocalVelocity = r2d.velocity * Speed;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e) {
        // if (BallCount == 1) {
        //     BallCount += 1;
        //     Debug.Log("Ball count at " + BallCount);
        //     GameTimer.Dispose();
        // }
    }
}

//---multiball---
//on game start(set timer)
//if timer > 30 seconds {BallB.ResetBall()}
//if timer > 60 seconds {BallC.ResetBall()}

//---ball smack---
//when fire pressed
//paddle.transform forward x (this prevents spam by providing an opportunity to get past the paddle)
//(if this doesnt accelerate the ball) multiply ball acceleration by 5