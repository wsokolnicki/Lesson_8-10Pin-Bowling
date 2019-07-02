using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour
{
    [SerializeField] Text text;
    _GameManager gameManager;
    bool ballEnteredBox = false;
    public int lastStandingCount = -1;
    float lastChangeTime;
    int lastSettledCount = 10;
    //int pinFall = 0;

    private void Start()
    {
        gameManager = FindObjectOfType<_GameManager>();
    }

    private void Update()
    {
        if (ballEnteredBox)
        {
            text.text = CountStanding().ToString();
            CheckStanding();
        }
    }

    public void Reset()
    {
        lastSettledCount = 10;
    }

    public void CheckStanding()
    {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }
        float settleTime = 3f;
        if ((Time.time - lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled()
    {
        int pinFall = lastSettledCount - CountStanding();
        lastSettledCount = CountStanding();

        lastStandingCount = -1;
        ballEnteredBox = false;
        text.color = Color.green;

        gameManager.Bowl(pinFall);
    }

    public int CountStanding()
    {
        int noOfStandingPins = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.transform.Translate(new Vector3(0, 0, 0.000001f));

            if (pin.IsStanding())
            {
                noOfStandingPins++;
            }
        }

        return noOfStandingPins;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BowlingBall>())
        {
            ballEnteredBox = true;
            text.color = Color.red;
        }
    }

}
