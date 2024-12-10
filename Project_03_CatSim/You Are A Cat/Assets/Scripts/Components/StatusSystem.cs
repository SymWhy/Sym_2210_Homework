using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace CatSim
{
    //StatusSystem event flow:
    // - Resource componet sucessfully activates
    // - StatusSystem observes this and changes the status based on resource type
    // - 
    public class StatusSystem : MonoBehaviour
    {
        public static StatusSystem statusSystem;
        public static StatusBar hungerBar;

        [SerializeField]
        private int Hungry = 50;

        public static int MaxStat { get; private set; }

        //start with maxed out hunger
        public static int Hunger;

        [HideInInspector]
        public static HungerState hungerState;


        // Start is called before the first frame update
        void Start()
        {
            //initialize variables
            statusSystem = this;
            MaxStat = 100;
            Hunger = MaxStat;
            hungerState = HungerState.Content;

            hungerBar = GameObject.Find("HungerBar").GetComponent<StatusBar>();

            //on tick do this
            TickSystem.OnTick += delegate (object sender, TickSystem.OnTickEventArgs e)
            {
                GetHungry();
            };
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void OnResourceActivate(ResourceType type)
        {
            if (type == ResourceType.Food)
            {
                    Hunger = MaxStat;
                    hungerState = HungerState.Content;
                    StatusSystem.hungerBar.UpdateFill();
                    Debug.Log("I just ate!");
            }
        }


        private void GetHungry()
        {
            //every tick, decrease hunger. when hunger reaches "Hungry" amt, set state to Hungry
                if (Hunger > 0)
                {
                    Hunger -= 1;

                    //decrease status bar
                    hungerBar.UpdateFill();
                    
                    if (Hunger == Hungry)
                    {
                        hungerState = HungerState.Hungry;
                        Debug.Log("Switching to state: " + hungerState);
                    }
                }
        }
    }
}