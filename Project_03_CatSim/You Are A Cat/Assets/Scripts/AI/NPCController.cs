using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CatSim
{
    public class NPCController : MonoBehaviour, IPointerDownHandler
    {
        //when activated, npc walks to dish and refills it, then walks back

        //components
        private Rigidbody2D NPCBody;
        private Animator NPCAnimator;

        private Rigidbody2D PlayerBody;

        //presets
        public float Speed = 1f;

        //data
        private Vector2 NPCPos;
        private Vector2 Target;
        private bool IsMoving;
        public Resource resource; // grab the last active resource

        private bool GoingToResource;
        private bool ArrivedAtResource;

        private Vector3Int CellTarget;

        // Start is called before the first frame update
        void Start()
        {
            //gather components
            NPCBody = GetComponent<Rigidbody2D>();
            NPCAnimator = GetComponent<Animator>();

            PlayerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();


            //initialize variables
            Target = transform.position;
            IsMoving = false;
            GoingToResource = false;
            ArrivedAtResource = false;


            //play default animations
            NPCAnimator.SetFloat("XInput", 1);
            NPCAnimator.SetFloat("YInput", -1);

        }

        // Update is called once per frame
        void Update()
        {
            //move the player to target coords (start position, target position, speed)
            transform.position = Vector2.MoveTowards(transform.position, Target, Time.deltaTime * Speed);

            //get the player's current x and y
            NPCPos = new Vector2(transform.position.x, transform.position.y);

            //when the player arrives at the target cell
            if (NPCPos == Target)
            {
                //the player is no longer moving
                IsMoving = false;
            }
            //play the appropriate animations
            NPCAnimator.SetBool("IsMoving", IsMoving);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData pointerEventData)
        {
            //when the player clicks the npc
            //find an empty resource
            resource = GameObject.Find("resource_food").GetComponent<Resource>();

            //move to resource if asked
            GoingToResource = true;
            StartCoroutine(WaitForActivation(resource));

        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<Rigidbody2D>() == PlayerBody && GoingToResource == true)
            {
                if (resource.state == ResourceState.Used && StatusSystem.hungerState == HungerState.Hungry)
                {
                    GoingToResource = false;
                    ArrivedAtResource = true;
                }

                else
                {
                    GoingToResource = false;
                    Debug.Log("You're fine!");
                }
            }
        }

        void RefillResource(Resource resource)
        {
            //record current position
            Vector2 startPos = transform.position;

            //get the highest priority used resource (just food for now)
            CellTarget = GameObject.Find("resource_food").GetComponent<Resource>().GetMyTile();

            //update the target to the new cell (in world coords)
            Target = GameManager.Infomap.GetCellCenterWorld(CellTarget);

            //play the appropriate animations
            NPCAnimator.SetFloat("XInput", Target.x - startPos.x);
            NPCAnimator.SetFloat("YInput", Target.y - startPos.y);

            //let the game know the player is moving
            IsMoving = true;
            StartCoroutine(WaitForRefill(resource));
        }

        IEnumerator WaitForActivation(Resource myResource)
        {
            yield return new WaitUntil(() => ArrivedAtResource == true);

            yield return new WaitForSeconds(0.2f);

            if (myResource.state == ResourceState.Used)
            {
                ArrivedAtResource = false;
                RefillResource(myResource);
            }

            else
            {
                Debug.Log("It's full!");
            }
        }

        //when I activate a resource, do this
        IEnumerator WaitForRefill(Resource myResource)
        {
            yield return new WaitUntil(() => IsMoving == false);

            yield return new WaitForSeconds(0.2f);

            //change resource state to fresh
            myResource.ResetResource();

            //play audio
            myResource.RefillAudioSource.Play();

            yield return new WaitForSeconds(myResource.RefillAudioSource.clip.length);

            //walk to a random tile
            Vector3Int myCell = GameManager.GetGameManager().GetRandomCell(myResource.GetMyTile());
            Target = GameManager.Infomap.GetCellCenterWorld(myCell);
            IsMoving = true;

        }
    }
}