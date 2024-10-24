using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingSprite : MonoBehaviour
{    
    //length of each individual flash
    public float FlashTime = 0.25f;
    //length of time the flashing status is active
    public float TimeActive = 1.0f;
    private float StartTime;
    private Color StartColor;
    public Color AltColor;
    // Start is called before the first frame update
    void Start()
    {
        StartColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFlash() {
        StartCoroutine(FlashingCoRoutine());
    }

    IEnumerator FlashingCoRoutine() {
        StartTime = Time.time;
        while ((StartTime + TimeActive) > Time.time) {
            yield return new WaitForSeconds(FlashTime);
            GetComponent<SpriteRenderer>().color = AltColor;
            yield return new WaitForSeconds(FlashTime);
            GetComponent<SpriteRenderer>().color = StartColor;
        }
    }
}
