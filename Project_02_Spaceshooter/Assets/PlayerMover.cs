using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    private Rigidbody2D MyRigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float Rotation = Input.GetAxis("Horizontal");
        float FullRotation = Rotation * RotationSpeed;
        MyRigidbody2D.MoveRotation(MyRigidbody2D.rotation + (FullRotation * Time.deltaTime));

        if(Input.GetAxis("Vertical") > 0f)
        {
            Debug.Log("Vector is : " + (transform.forward * (Speed * Input.GetAxis("Vertical"))));
            MyRigidbody2D.AddForce(transform.up * (Speed * Input.GetAxis("Vertical")));
        }
        
    }
}
