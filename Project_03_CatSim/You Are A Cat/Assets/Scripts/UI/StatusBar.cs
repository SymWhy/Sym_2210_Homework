using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatSim
{
    public class StatusBar : MonoBehaviour
    {
        private int max;

        [SerializeField]
        public static int current;
        
        [SerializeField]
        private Image bar;

        [SerializeField]
        private Image warning;

        void Start()
        {
            max = StatusSystem.MaxStat;
            current = max;
            warning.enabled = false;
        }

        void Update()
        {
            GetCurrentFill();
        }

        void GetCurrentFill()
        {
            //divide current amt by maximum as floats
            float fillAmount = (float)current / (float)max;

            bar.fillAmount = fillAmount;

            //warn the user when the stat gets low
            if (fillAmount * 100 <= (max / 2))
            {
                warning.enabled = true;
            }

            //remove warning when no longer relevant
            else if (fillAmount * 100 >= (max / 2))
            {
                warning.enabled = false;
            }
        }

        public void UpdateFill()
        {
            current = StatusSystem.Hunger;
        }
    }
}