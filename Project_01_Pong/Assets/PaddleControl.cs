using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public string myAxis;
    public float speed = 2.0f;
    public Rigidbody2D r2d;

    public string FireBtn;
    public int FireDir;
    public float BumperDist = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float MoveDir = Input.GetAxis(myAxis);

        Vector2 vel = new Vector2(0.0f, MoveDir);
        vel = vel * speed * 10;

        //velocity wants a vector
        r2d.velocity = vel;

        // if (Input.GetButtonDown(FireBtn)) {
        //     // Debug.Log("Fire!");
        //     r2d.Transform.translate(0, BumperDist * FireDir, 0);
        //     r2d.Transform.translate(0, BumperDist * FireDir * -1, 0);
        // }        
    }
}


//input.getbutton (only when button is pressed)
