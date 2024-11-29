using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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
        private AudioSource ResourceAudioSource;

        //records active state
        protected ResourceState State;

        // Start is called before the first frame update
        void Awake()
        {
            PlayerBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
            //most interactibles will have a sound source
            ResourceAudioSource = GetComponent<AudioSource>();

            //starting with fresh resources
            State = ResourceState.Fresh;
        }

        // Update is called once per frame
        void Update()
        {
            //cancels activation if you move elsewhere
            if (Input.GetMouseButtonDown(0))
            {
                if (GoingToResource == true)
                {
                    GoingToResource = false;
                }
            }

            //Do stuff on activation and change states
            if (State == ResourceState.Active)
            {
                StartCoroutine(WaitForActivation());
            }
        }

        //Going and Arrived both start == false > OnPointerDown sets Going == true > OnTrigger sets Going == false and Arrived == true > WaitForActivation sets Arrived == false;

        //if player clicks on resource, move to it and prepare to change states
        void IPointerDownHandler.OnPointerDown(PointerEventData pointerEventData)
        {
            if (State == ResourceState.Fresh)
            {
                GoingToResource = true;
                // Returning Going == true and Arrived == false
                // Debug.Log("I am now going to the resource.");
                // Debug.Log("GoingToResource = " + GoingToResource);
                // Debug.Log("ArrivedAtResource = " + ArrivedAtResource);
                State = ResourceState.Active;
                Debug.Log(Name + " switching to state: " + State);
            }
        }


        //check if the player is moving to a resource and when they arrive
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col == PlayerBody)
            {
                Debug.Log("Detecting a collision with: " + col);
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
            ResourceAudioSource.Play();
            State = ResourceState.Used;
            Debug.Log(Name + " switching to state: " + State);
        }
    }
}