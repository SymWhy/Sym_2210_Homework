using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CatSim
{
    public class PlayerController : MonoBehaviour
    {
        //components
        [HideInInspector]
        private Rigidbody2D PlayerBody;
        private Camera MainCamera;
        private Animator PlayerAnimator;
        private List<Vector2Int> ValidTiles;

        //presets
        public float Speed = 1f;

        //data
        private Vector2 PlayerPos;
        private Vector2 Target;
        private bool IsMoving = false;

        private Vector3Int CellTarget;



        void Awake()
        {
            //gather components
            PlayerBody = GetComponent<Rigidbody2D>();
            PlayerAnimator = GetComponent<Animator>();

            //initialize target
            Target = transform.position;

            //play default animations
            PlayerAnimator.SetFloat("XInput", 1);
            PlayerAnimator.SetFloat("YInput", -1);
        }

        // Update is called once per frame
        void Update()
        {
            //when the player clicks anywhere
            if (Input.GetMouseButtonDown(0))
            {
                //record current position
                Vector2 startPos = transform.position;
                // Debug.Log("Starting location is: " + startPos);

                //get the last cell that was clicked
                CellTarget = GameManager.GetGameManager().GetCellClicked();
                // Debug.Log("I want to go to " + CellTarget);
                //convert that cell to 2D
                Vector2Int v2iCellTarget = new Vector2Int(CellTarget.x, CellTarget.y);
                // Debug.Log("Which as a Vector2Int, is: " + v2iCellTarget);

                //if that cell is in the list of tiles we can move to
                if (GameManager.ValidTileList.Contains(v2iCellTarget))
                {
                    // Debug.Log("This is a valid tile!");
                    //update the target to the new cell (in world coords)
                    Target = GameManager.Infomap.GetCellCenterWorld(CellTarget);

                    //play the appropriate animations
                    PlayerAnimator.SetFloat("XInput", Target.x - startPos.x);
                    PlayerAnimator.SetFloat("YInput", Target.y - startPos.y);

                    //let the game know the player is moving
                    IsMoving = true;
                }
            }

            //move the player to target coords (start position, target position, speed)
            transform.position = Vector2.MoveTowards(transform.position, Target, Time.deltaTime * Speed);

            //get the player's current x and y
            PlayerPos = new Vector2(transform.position.x, transform.position.y);

            //when the player arrives at the target cell
            if (PlayerPos == Target)
            {
                //the player is no longer moving
                IsMoving = false;
            }
            //play the appropriate animations
            PlayerAnimator.SetBool("IsMoving", IsMoving);
        }
    }
}