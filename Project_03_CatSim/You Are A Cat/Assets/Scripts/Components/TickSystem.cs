using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatSim
{
    public class TickSystem : MonoBehaviour
    {
        public class OnTickEventArgs: EventArgs
        {
            public int tick;
        }

        //sends the current tick
        public static event EventHandler<OnTickEventArgs> OnTick;
    
        public static int tick;
        public static float tickTimer;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //add time passed since last update
            tickTimer += Time.deltaTime;

            //if time passed is greater than the length of one tick
            if (tickTimer >= GameManager.TickDuration)
            {
                //increment the tick count
                tick++;

                //reset tickTimer
                tickTimer -= GameManager.TickDuration;

                //if OnTick has subscribers, fire the relevent event
                if (OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick});
            }
        }
    }
}