using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    List<int> pins = new List<int> ();
    BowlingBall ball;
    PinSetter pinSetter;
    ScoreDisplay scores;

    private void Start()
    {
        ball = FindObjectOfType<BowlingBall>();
        pinSetter = FindObjectOfType<PinSetter>();
        scores = FindObjectOfType<ScoreDisplay>();
    }

    public void Bowl (int pinFall)
    {
        try
        {
            pins.Add(pinFall);
            ball.Reset();
            pinSetter.PerformAction(ActionMaster.NextAction(pins));
        }
        catch
        {
            Debug.LogWarning("Something went wrong with Bowl()");
        }

        try
        {
            scores.FillRollCard(pins);
            scores.FillFrames(ScoreMaster.ScoreCumulative(pins));
        }
        catch { Debug.LogWarning("Something went wrong with FillRollCard()");  }
    }
}
