using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public string myAxis;
    public float speed = 2.0f;
    public Rigidbody2D r2d;

    public string FireBtn;

    public bool MoveFwd = false;
    public bool MoveBk = false;
    public float StartX;
    public float StartY;

    public bool LeftPaddle = false;

    // Start is called before the first frame update
    void Start()
    {
        //record default X position
        StartX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float MoveDir = Input.GetAxis(myAxis);

        Vector2 vel = new Vector2(0.0f, MoveDir);
        vel = vel * speed * 10;

        //velocity wants a vector
        r2d.velocity = vel;

        //will not transform
        if (Input.GetButtonDown(FireBtn)) {
            // Debug.Log("Fire!");
            //transform.Translate(Vector3.right * 1);
            //record starting Y position
            StartY = transform.position.y;
            MoveFwd = true;
        }

        if (LeftPaddle) {
            if (MoveFwd) {
                transform.Translate(Vector3.right * 0.4f);
             if (transform.position.x >= StartX + 1.3f) {
                MoveFwd = false;
                MoveBk = true;
                }
            }

            if (MoveBk) {
                transform.Translate(Vector3.left * 0.1f);
                if (transform.position.x <= StartX) {
                    MoveBk = false;
                }
            }
        }

        else {
            if (MoveFwd) {
                transform.Translate(Vector3.left * 0.3f);
             if (transform.position.x <= StartX - 1.3f) {
                MoveFwd = false;
                MoveBk = true;
                }
            }

            if (MoveBk) {
                transform.Translate(Vector3.right * 0.1f);
                if (transform.position.x >= StartX) {
                    MoveBk = false;
                }
            }
        }

        
    }
}

//I want to:
//Stop paddle movement when fire pressed
//Move paddle forward 
//Return paddle to start location


//input.getbutton (only when button is pressed)


//if x becomes > max distance, invert velocity
//make wall only paddles can hit?