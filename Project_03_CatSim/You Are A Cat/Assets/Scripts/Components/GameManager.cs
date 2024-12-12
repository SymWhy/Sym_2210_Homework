using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CatSim
{
    public class GameManager : MonoBehaviour
    {
        static GameManager _Instance;
        public static Grid WorldGrid { get; private set; }
        public static Tilemap Infomap { get; private set; }
        public static Camera MainCamera { get; private set; }

        //global variables
        [SerializeField]
        public static float TickDuration = 0.3f;

        //GameManager-specific variables
        public Vector3Int LastCellClicked { get; private set; }

        //global lists and dictionaries
        public static List<Vector2Int> ValidTileList;
        void Awake()
        {
            //gather components
            MainCamera = Camera.main;
            WorldGrid = GameObject.Find("Grid").GetComponent<Grid>();
            Infomap = GameObject.Find("Info").GetComponent<Tilemap>();
            ValidTileList = new List<Vector2Int>();

            //populate lists and dictionaries
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    Vector2Int myCoord = new Vector2Int(x, y);
                    ValidTileList.Add(myCoord);
                    //should be a list of (0, 0), (0, 1), (0, 2), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1), (2, 2)
                }
            }
        }

        void Start()
        {
            //subscribe GameManager to TickSystem, do the following on tick update
            TickSystem.OnTick += delegate (object sender, TickSystem.OnTickEventArgs e)
            {
                //do whatever here
            };
        }

        //game manager singleton
        public static GameManager GetGameManager()
        {
            if (_Instance == null)
            {
                _Instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _Instance;
        }

        public bool GetCellValid() 
        {
            Vector3 mousePos = new Vector3();
            //get mouse position
            mousePos = MouseToWorld();
            //convert mouse position to a Vector3Int
            Vector3Int cellPos = new Vector3Int();
            cellPos = WorldGrid.WorldToCell(mousePos);
            //convert Vector3Int to Vector2Int
            Vector2Int myCell = new Vector2Int(cellPos.x, cellPos.y);
            //check if cell is valid
            if (ValidTileList.Contains(myCell)) 
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        //translates mouse location to world coords
        public Vector3 MouseToWorld()
        {
            //get coords of mouse pointer
            Vector2 mousePosRaw = Input.mousePosition;
            //REMEMBER TO MARK YOUR CAMERA AS MAIN CAMERA OR YOU WILL GET AN ERROR
            Vector2 LastMousePos = MainCamera.ScreenToWorldPoint(mousePosRaw);
            return LastMousePos;
        }

        public Vector3Int GetCellClicked()
        {
            //get mouse position
            Vector3 mousePos = MouseToWorld();

            //convert mouse position to cell position
            Vector3Int myCell = new Vector3Int();
            myCell = WorldGrid.WorldToCell(mousePos);
            return myCell;
        }

        public Vector3Int GetRandomCell(Vector3Int tile)
        {
            int randomTile = Random.Range(0, ValidTileList.Count);
            Vector3Int randomCell = new Vector3Int(ValidTileList[randomTile].x, ValidTileList[randomTile].y, 0);
            if (randomCell == tile)
            {
                return GetRandomCell(tile);
            }
            else
            {
                return randomCell;
            }

        }
    }
}