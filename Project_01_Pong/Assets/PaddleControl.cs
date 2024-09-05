using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public string myAxis;
    public float speed = 2.0f;
    public Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis(myAxis);

        Vector2 vel = new Vector2(0.0f, direction);
        vel = vel * speed * 10;

        //velocity wants a vector
        r2d.velocity = vel;
    }
}
