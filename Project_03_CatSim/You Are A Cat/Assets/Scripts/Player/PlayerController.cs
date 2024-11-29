using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CatSim
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector]
        private Rigidbody2D PlayerBody;
        private Camera MainCamera;
        private Animator PlayerAnimator;

        private Tilemap InfoMap;

        private Vector2 PlayerPos;
        private Vector2 Target;
        private bool IsMoving = false;

        private Vector3Int CellTarget;

        public float Speed = 1f;


        void Awake()
        {
            PlayerBody = GetComponent<Rigidbody2D>();
            PlayerAnimator = GetComponent<Animator>();

            InfoMap = GameObject.Find("Info").GetComponent<Tilemap>();

            Target = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 startPos = transform.position;
                
                // Vector3 mousePos = GameManager.GetGameManager().MouseToWorld();

                // Target = new Vector2(mousePos.x, mousePos.y);


                CellTarget = GameObject.Find("Info").GetComponent<GetCellClicked>().LastCellClicked;
                Target = InfoMap.GetCellCenterWorld(CellTarget);

                
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