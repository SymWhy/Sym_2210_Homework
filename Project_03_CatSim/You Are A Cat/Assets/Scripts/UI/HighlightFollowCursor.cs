using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatSim
{
    public class HighlightFollowCursor : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Sprite SpriteTrue;
        [SerializeField]
        private Sprite SpriteFalse;
        [SerializeField]
        private Sprite SpriteClicked;

        private Sprite CurrentSprite;

        private bool CellValid;

        // Start is called before the first frame update
        void Start()
        {
            //gather components
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            //set up variables
            CurrentSprite = SpriteTrue;
        }

        // Update is called once per frame
        void Update()
        {
            //check if the cell is valid
            CellValid = GameManager.GetGameManager().GetCellValid();

            if (Input.GetMouseButtonDown(0))
            {
                if (CellValid)
                {
                    StartCoroutine(ChangeSpriteAndHold());
                }
            }

            MoveHighlight();
            UpdateSprite();
        }

        private void MoveHighlight()
        {
            //convert mouse pos to cell
            Vector3 currentPos = GameManager.GetGameManager().MouseToWorld();
            Vector3Int cellPos = GameManager.WorldGrid.WorldToCell(currentPos);
            Vector3 newPos = GameManager.Infomap.GetCellCenterWorld(cellPos);

            //move cursor to cell
            gameObject.transform.SetPositionAndRotation(newPos, Quaternion.identity);
        }

        private void UpdateSprite()
        {
            //change the sprite to green if it isnt green or yellow
            if (CellValid == true && CurrentSprite == SpriteFalse)
            {
                CurrentSprite = SpriteTrue;
            }

            //change the sprite to red if it isnt red or yellow
            else if (CellValid == false && CurrentSprite == SpriteTrue)
            {
                CurrentSprite = SpriteFalse;
            }

            spriteRenderer.sprite = CurrentSprite;
        }

        IEnumerator ChangeSpriteAndHold()
        {
            CurrentSprite = SpriteClicked;
            yield return new WaitForSecondsRealtime(0.2f);
            CurrentSprite = SpriteTrue;
        }
    }
}