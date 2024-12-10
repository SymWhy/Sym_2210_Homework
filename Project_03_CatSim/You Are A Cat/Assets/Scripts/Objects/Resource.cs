using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

namespace CatSim
{
    //resources have 3 states: Fresh, Active, and Used
    //they transition as such: Fresh > Active > Used
    //clicking on a resource makes it Active
    //Active resources play audio and sometimes animations, then transition to Used
    //A Used resource needs to be "refreshed" somehow, before you can use it again
    //Note: Certain resources return to Fresh for the first few uses, such as the potty
    //so we want to:
    //check if clicked on
    //(and cancel if we click anywhere else)
    //transition to active to play audio + animations
    //transition to used to lose interactibility


    //idea: selection script that returns whichever gameobject was just clicked

    public class Resource : MonoBehaviour, IPointerDownHandler
    {
        //mostly just for debug
        private readonly String Name = "RESOURCE NOT SET";

        //bools for detecting resource interaction
        private bool GoingToResource = false;
        private bool ArrivedAtResource = false;

        //gather necessary game resources
        private Rigidbody2D PlayerBody;
        public AudioSource EmptyAudioSource;
        public AudioSource RefillAudioSource;
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Sprite SpriteFresh;
        [SerializeField]
        private Sprite SpriteUsed;
        private Sprite CurrentSprite;

        private Vector3Int MyCell;

        //records active state
        public ResourceState state { get; private set; }

        [SerializeField]
        protected ResourceType type;

        // Start is called before the first frame update
        void Awake()
        {

            PlayerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();

            //record cell containing resource
            MyCell = GameObject.Find("Grid").GetComponent<Grid>().WorldToCell(transform.position);

            //grab the child sound sources attached to the resource
            EmptyAudioSource = gameObject.transform.GetChild(0).GetComponent<AudioSource>();
            RefillAudioSource = gameObject.transform.GetChild(1).GetComponent<AudioSource>();

            //get the sprite renderer for rendering sprites
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            //starting with fresh resources
            state = ResourceState.Fresh;

            CurrentSprite = SpriteFresh;
            spriteRenderer.sprite = CurrentSprite;

            // Debug.Log("Current sprite is: " + CurrentSprite);
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            //cancels activation if you move elsewhere
            if (Input.GetMouseButtonDown(0) && GameManager.GetGameManager().LastCellClicked != MyCell)
            {
                if (GoingToResource == true)
                {
                    GoingToResource = false;
                }
            }

            //Do stuff on activation and change states
            if (state == ResourceState.Active)
            {
                StartCoroutine(WaitForActivation());
            }

        }

        //Going and Arrived both should start == false > OnPointerDown sets Going == true > OnTrigger sets Going == false and Arrived == true > WaitForActivation 
        //sets Arrived == false;

        //if player clicks on resource, move to it and prepare to change states
        void IPointerDownHandler.OnPointerDown(PointerEventData pointerEventData)
        {
            if (state == ResourceState.Fresh)
            {
                GoingToResource = true;
                // Returning Going == true and Arrived == false
                state = ResourceState.Active;
                // Debug.Log(Name + " switching to state: " + State);
            }
            else
            {
                Debug.Log("It's empty!");
            }
        }

        //check if the player is moving to a resource and when they arrive
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<Rigidbody2D>() == PlayerBody)
            {
                // Debug.Log("Detecting a collision with: " + col);
                if (GoingToResource == true)
                {
                    GoingToResource = false;
                    ArrivedAtResource = true;
                }
            }
        }

        //wait until player gets to the resource before changing states
        IEnumerator WaitForActivation()
        {
            yield return new WaitUntil(() => ArrivedAtResource == true);

            //do all this and change states
            ArrivedAtResource = false;

                SwapSpriteToUsed();
                EmptyAudioSource.Play();
                state = ResourceState.Used;

            //tell StatusSystem that the resource was activated
            StatusSystem.OnResourceActivate(type);
        }

        public void ResetResource()
        {
            SwapSpriteToFresh();
            state = ResourceState.Fresh;
        }

        private void SwapSpriteToFresh()
        {
            CurrentSprite = SpriteFresh;
            spriteRenderer.sprite = CurrentSprite;
        }

        private void SwapSpriteToUsed()
        {
            CurrentSprite = SpriteUsed;
            spriteRenderer.sprite = CurrentSprite;
        }

        public Vector3Int GetMyTile()
        {
            Vector3 myPos = transform.position;
            Vector3Int myTile = GameManager.WorldGrid.WorldToCell(myPos);
            return myTile;
        }
    }
}