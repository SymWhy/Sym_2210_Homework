using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI LeftText;
    public TextMeshProUGUI RightText;
    public Button RestartButton;
    public int MaxScore;
    public int LeftScore;
    public int RightScore;

    public bool BallBOut = false;
    public bool BallCOut = false;
    public float StartTime;
    public float CurrentTime;
    public Ball BallA;
    public Ball BallB;
    public Ball BallC;
    public float SecsForBallB = 5;
    public float SecsForBallC = 10;

    public bool Restarting = false;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        CurrentTime = StartTime;
        // Debug.Log(CurrentTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        CurrentTime = Time.time;

//check for win condition, leave if
        if (StartTime + CurrentTime > SecsForBallB && !BallBOut) {
            BallBOut = true;
            BallB.ResetBall();
        }

        if (StartTime + CurrentTime > SecsForBallC && !BallCOut) {
            BallCOut = true;
            BallC.ResetBall();
        }
    }

/// ball not deactivating
    public int IncrementScore(bool IsLeft) {
        if (IsLeft) {
            LeftScore = LeftScore + 1;
            string Score = "";
            Score = Score + LeftScore;
            Debug.Log("Left: " + Score);
            LeftText.SetText(Score);
            if (LeftScore >= MaxScore) {
                RestartButton.gameObject.SetActive(true);
                Ball[] BallArray = GameObject.FindObjectsOfType<Ball>();
                foreach (Ball myBall in BallArray) {
                    myBall.ResetAllBalls();
                }
                Debug.Log("Game over!");
                Restarting = true;
                BallBOut = true;
                BallCOut = true;
            }
        }

        else {
            RightScore = RightScore + 1;
            string Score = "";
            Score = Score + RightScore;
            Debug.Log("Right: " + Score);
            RightText.SetText(Score);
            if (RightScore >= MaxScore) {
                RestartButton.gameObject.SetActive(true);
                Ball[] BallArray = GameObject.FindObjectsOfType<Ball>();
                foreach (Ball myBall in BallArray) {
                    myBall.ResetAllBalls();
                }
                Debug.Log("Game over!");
                Restarting = true;
                BallBOut = true;
                BallCOut = true;
            }
        }

        return 0;
    }

        //game not restarting
    public void RestartGame() {
        RestartButton.gameObject.SetActive(false);
        LeftScore = 0;
        RightScore = 0;
        LeftText.SetText("0");
        RightText.SetText("0");
        PaddleControl[] PaddleArray = GameObject.FindObjectsOfType<PaddleControl>();
        foreach (PaddleControl myPaddle in PaddleArray) {
            myPaddle.ResetPaddle();
        }
        //reset the timer
        StartTime = Time.time;
        BallBOut = false;
        BallCOut = false;

        //Restart BallA
        GameObject.Find("BallA").GetComponent<Ball>().ResetBall();

        Restarting = false;
    }
}
