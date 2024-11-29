using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

namespace CatSim
{
    public class GetCellClicked : MonoBehaviour
    {
        //whenever I click on the map, return cell id

        //maybe I want a dictionary of tiles, tiletypes?

        public Grid MyGrid { get; private set; }
        public Tilemap MyTilemap { get; private set; }

        private Vector3 MousePos;

        //return the coords of the last cell clicked
        public Vector3Int LastCellClicked { get; private set; }

        void Awake()
        {
            MyGrid = GameObject.Find("Grid").GetComponent<Grid>();
            MyTilemap = GetComponent<Tilemap>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //get mouse position
                MousePos = GameManager.GetGameManager().MouseToWorld();
                //convert mouse position to cell position
                LastCellClicked = MyGrid.WorldToCell(MousePos);
                //gets the tile type
                // Tile tile = MyTilemap.GetTile<Tile>(CellPos);
                // Debug.Log(tile);

                // if (tile != null)
                // {
                //     LastTileClicked = 1;
                // }

                // else 
                // {
                //     LastTileClicked = 0;
                // }
            }
        }
    }
}