using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatSim
{
    public class GameManager : MonoBehaviour
    {
        static GameManager _Instance;
        public Camera MainCamera { get; private set; }
        void Awake()
        {
            MainCamera = Camera.main;
        }

        public static GameManager GetGameManager()
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _Instance;
        }

        public Vector3 MouseToWorld()
        {
            //get coords of mouse pointer
            Vector2 mousePosRaw = Input.mousePosition;
            //REMEMBER TO MARK YOUR CAMERA AS MAIN CAMERA OR YOU WILL GET AN ERROR
            Vector2 mousePos = MainCamera.ScreenToWorldPoint(mousePosRaw);
            return mousePos;
        }
    }
}