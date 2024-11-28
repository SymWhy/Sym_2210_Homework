using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatSim
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D PlayerBody;
        private Camera MainCamera;
        private Animator PlayerAnimator;

        private Vector2 PlayerPos;
        private Vector2 Target;
        private bool IsMoving = false;

        public float Speed = 1f;


        void Awake()
        {
            PlayerBody = GetComponent<Rigidbody2D>();
            PlayerAnimator = GetComponent<Animator>();
            MainCamera = Camera.main;

            Target = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 startPos = transform.position;
                //get coords of mouse pointer
                Vector2 mousePosRaw = Input.mousePosition;
                //REMEMBER TO MARK YOUR CAMERA AS MAIN CAMERA OR YOU WILL GET AN ERROR
                Vector2 mousePos = MainCamera.ScreenToWorldPoint(mousePosRaw);

                Target = new Vector2(mousePos.x, mousePos.y);

                PlayerAnimator.SetFloat("XInput", Target.x - startPos.x);
                PlayerAnimator.SetFloat("YInput", Target.y - startPos.y);

                IsMoving = true;
            }

            //move the player to target coords (start position, target position, speed)
            transform.position = Vector2.MoveTowards(transform.position, Target, Time.deltaTime * Speed);
            PlayerPos = new Vector2(transform.position.x, transform.position.y);
            if (PlayerPos == Target) {
                IsMoving = false;
            }
            PlayerAnimator.SetBool("IsMoving", IsMoving);
        }
    }
}